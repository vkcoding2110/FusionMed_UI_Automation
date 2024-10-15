using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments;
using UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.ProfileSharing
{
    [TestClass]
    [TestCategory("ProfileSharing"), TestCategory("FMP")]
    public class ProfileSharingTests2 : FmpBaseTest
    {
        private static readonly Login ManageMyProfileSharingUser = GetLoginUsersByTypeAndPlatform("ManageMyProfileSharingTests");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteProfileSharingDetails();
        }

        [TestMethod]
        [DoNotParallelize]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void Profile_VerifyThatProfileSharingWithNewRecipientAsATravelerWorksSuccessfully()
        {
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var email = new EmailPo(Driver);
            var emailList = new EmailListingGridPo(Driver);
            var sharedProfileLogInSignUp = new SharedProfileLogInPo(Driver);
            var signUpPage = new SignUpPo(Driver);
            var aboutMe = new AboutMePo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var recipientSharedProfilePage = new RecipientSharedProfilePo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetailPage.NavigateToPage();

            Log.Info("Step 5: Click on 'Manage My Profile Sharing' button, click on 'Add Recipient Via Email' button");
            profileDetailPage.ClickOnEditAboutMeButton();
            var expectedUserData = editProfile.GetEditAboutMeData();
            editProfile.ClickOnCancelButton();
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOAddRecipientViaEmailButton();

            Log.Info("Step 6: Add details in 'Share My Profile' form, click on 'Share My Profile' button & click on 'My Profile Sharing' popup 'close' icon");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            profileSharing.AddShareMyProfileDetails(addAboutMeSignUpData.Email);

            Log.Info("Step 7: Click on user profile & Click on 'Log out' button");
            profileSharing.ClickOnCloseIconAndClickOnLogout();

            Log.Info("Step 8: Open 'YopMail', Open your 'Shared Email', click on 'Get Started' button & verify Login/Signup page gets open");
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 30 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);
            emailList.OpenEmail($"{FusionMarketPlaceLoginCredentials.Name} has shared their Fusion Marketplace profile");
            var actualSenderEmail = emailList.GetSenderEmailText();
            Assert.AreEqual(GlobalConstants.SenderEmailText, actualSenderEmail, "The Sender email doesn't match");

            const string getStartedButton = "Get Started";
            emailList.ClickOnButtonOrLink(getStartedButton);
            const string expectedLogInSignUpPageHeaderText = "Welcome!";
            var actualLogInSignUpPageHeaderText = sharedProfileLogInSignUp.GetSharedProfileLogInSignUpHeaderText();
            Assert.AreEqual(expectedLogInSignUpPageHeaderText, actualLogInSignUpPageHeaderText, "Shared profile Login/SignUp page is not opened.");

            Log.Info("Step 9: Click on 'Create a Traveler Profile' link & verify traveler 'Sign Up' page gets open");
            sharedProfileLogInSignUp.ClickOnCreateTravelerProfileButton();
            const string expectedTitle = "sign up";
            var actualTitle = signUpPage.GetSignUpPageHeader().ToLower();
            Assert.AreEqual(expectedTitle, actualTitle, "Sign up Title is not matched");

            Log.Info("Step 10: Signing up as a new user and login as new user");
            aboutMe.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);
            passwordPage.FillFormAndSubmit(addAboutMeSignUpData);
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 30 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);
            emailList.OpenEmail("Confirm Email");
            const string confirmEmail = "Confirm Email";
            emailList.ClickOnButtonOrLink(confirmEmail);
            confirmPage.ClickOnConfirmationLogInButton();
            marketPlaceLogin.LoginToApplication(addAboutMeSignUpData);
            profileDetailPage.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 11: Verify the shared profile details are displayed correctly on recipient profile");
            var expectedSharedProfileUrl = FusionMarketPlaceUrl + "shared-profile";
            Assert.IsTrue(new WaitHelpers(Driver).WaitUntilUrlContains(expectedSharedProfileUrl), "Shared Profile Url is not correct");

            var expectedFirstAndLastName = expectedUserData.FirstName + " " + expectedUserData.LastName;
            var actualFirstAndLastName = recipientSharedProfilePage.GetFirstAndLastName();
            var expectedDepartmentSpecialty = expectedUserData.Category + " - " + expectedUserData.PrimarySpecialty;
            var actualDepartmentSpecialty = recipientSharedProfilePage.GetDepartmentAndSpecialtyText();
            var actualEmail = recipientSharedProfilePage.GetEmailText();

            Assert.AreEqual(expectedFirstAndLastName, actualFirstAndLastName, "Shared Profile 'User name' is not matched");
            Assert.AreEqual(expectedDepartmentSpecialty, actualDepartmentSpecialty, "Shared Profile 'DepartmentSpecialty' is not matched");
            Assert.AreEqual(FusionMarketPlaceLoginCredentials.Email, actualEmail, "Shared Profile 'Email' is not matched");

            var downloadPath = fileUtil.GetDownloadPath();
            var filename = FusionMarketPlaceLoginCredentials.Name + "_Resume";
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".pdf");
            recipientSharedProfilePage.ClickOnDownloadResumeButton();
            Assert.IsTrue(PlatformName != PlatformName.Web
                ? new MobileFileSelectionPo(Driver).IsFilePresentOnDevice(filename) : fileUtil.DoesFileExistInFolder(downloadPath, filename, ".pdf", 15), $"File - {filename} not found!");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void Profile_VerifyThatProfileSharingWithExistingRecipientWorksSuccessfully()
        {
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var emailList = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var sharedProfileLogInSignUp = new SharedProfileLogInPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();
            profileSharing.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetailPage.NavigateToPage();

            Log.Info("Step 5: Click on 'Manage My Profile Sharing' button, click on 'Add Recipient Via Email' button");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.DeleteAllProfileSharingEmails();
            profileSharing.ClickOAddRecipientViaEmailButton();

            Log.Info("Step 6: Add details in 'Share My Profile' form, click on 'Share My Profile' button");
            profileSharing.AddShareMyProfileDetails(ManageMyProfileSharingUser.Email);

            Log.Info("Step 7: Click on 'close' icon and click on 'Logout'");
            profileSharing.ClickOnCloseIconAndClickOnLogout();

            Log.Info("Step 8: Open 'YopMail', Open your 'Confirm Email' and verify 'sender email'");
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(ManageMyProfileSharingUser.Email);
            emailList.OpenEmail($"{FusionMarketPlaceLoginCredentials.Name} has shared their Fusion Marketplace profile");
            var actualSenderEmail = emailList.GetSenderEmailText();
            const string expectedSenderEmailMessage = "Hello there,Test Testing has created a traveler profile on Fusion Marketplace and would like to share it with you! It looks like you have an account, so you will just need to be logged in and you'll be taken to Test's profile. ";
            var actualSenderMessage = emailList.GetMessageBodyText();
            Assert.AreEqual(GlobalConstants.SenderEmailText, actualSenderEmail, "The Sender email doesn't match");
            Assert.AreEqual(expectedSenderEmailMessage.RemoveWhitespace().ToLowerInvariant(), actualSenderMessage.RemoveWhitespace().ToLowerInvariant(), "The sender email message doesn't match");

            Log.Info("Step 9: Click on 'View Shared Profile' button and verify 'Shared Profile' page gets open");
            const string viewSharedProfile = "View Shared Profile";
            emailList.ClickOnButtonOrLink(viewSharedProfile);
            var expectedSharedProfileUrl = FusionMarketPlaceUrl + "shared-profile";
            Assert.IsTrue(Driver.Url.Contains(expectedSharedProfileUrl), "Shared Profile Url is not matched");

            Log.Info("Step 10: Login with 'Recipient' credentials & verify profile sharing page gets open");
            sharedProfileLogInSignUp.ClickOnMyProfileSharingLoginButton();
            marketPlaceLogin.LoginToApplication(ManageMyProfileSharingUser);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();
            profileSharing.WaitUntilFmpTextLoadingIndicatorInvisible();
            var expectedSharedProfilePageUrl = FusionMarketPlaceUrl + "shared-profile";
            Assert.IsTrue(Driver.Url.Contains(expectedSharedProfilePageUrl), "Shared Profile Url is not matched");
        }

        [TestMethod]
        [DoNotParallelize]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatDownloadResumeAndDownloadDocumentOnSharedProfilePageWorksSuccessfully()
        {
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
            var addDocumentsPopUp = new AddDocumentPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var emailList = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var sharedProfileLogInSignUp = new SharedProfileLogInPo(Driver);
            var recipientSharedProfilePage = new RecipientSharedProfilePo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetailPage.NavigateToPage();

            Log.Info("Step 5: Click on 'My Document' tab, click on 'Add Document' button & upload document file");
            myDocumentsDetails.ClickOnMyDocumentsTabButton();
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            const int droppedDocumentRowNumber = 1;
            addDocumentsPopUp.UploadDocumentFromDevice(uploadDocument.First(), droppedDocumentRowNumber);
            addDocumentsPopUp.ClickOnUploadDocumentsButton();

            Log.Info("Step 6: Click on 'Manage My Profile Sharing' button, click on 'Add Recipient Via Email' button");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.DeleteAllProfileSharingEmails();
            profileSharing.ClickOAddRecipientViaEmailButton();

            Log.Info("Step 7: Add details in 'Share My Profile' form, click on 'Share My Profile' button");
            profileSharing.AddShareMyProfileDetails(ManageMyProfileSharingUser.Email);

            Log.Info("Step 8: Click on 'close' icon and click on 'Logout'");
            profileSharing.ClickOnCloseIconAndClickOnLogout();

            Log.Info("Step 9: Open 'YopMail', Open your 'Confirm Email' and click on 'View Shared Profile' button");
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(ManageMyProfileSharingUser.Email);
            emailList.OpenEmail($"{FusionMarketPlaceLoginCredentials.Name} has shared their Fusion Marketplace profile");
            const string viewSharedProfile = "View Shared Profile";
            emailList.ClickOnButtonOrLink(viewSharedProfile);

            Log.Info("Step 10: Login with 'Recipient' credentials");
            sharedProfileLogInSignUp.ClickOnMyProfileSharingLoginButton();
            marketPlaceLogin.LoginToApplication(ManageMyProfileSharingUser);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 11: Verify the Documents details are correct, click on 'Download' button and verify 'Document' file is downloaded successfully");
            const int documentRowNumber = 1;
            var actualUploadedDocumentDetails = recipientSharedProfilePage.GetDocumentDetailsFromDetailPage(documentRowNumber);
            Assert.AreEqual(uploadDocument.First().FileName.ToLowerInvariant(), actualUploadedDocumentDetails.First().FileName.ToLowerInvariant(), "The 'Document Name' is not matched");
            Assert.AreEqual(uploadDocument.First().DocumentType.ToLowerInvariant(), actualUploadedDocumentDetails.First().DocumentType.ToLowerInvariant(), "The 'Document Type' is not matched");
            Assert.AreEqual(uploadDocument.First().DocumentUploadedDate.ToString("MM/dd/yyyy"), actualUploadedDocumentDetails.First().DocumentUploadedDate.ToString("MM/dd/yyyy"), "The document uploaded 'Date' is not matched");
            Assert.AreEqual(uploadDocument.First().DocumentTypeCode.ToLowerInvariant(), actualUploadedDocumentDetails.First().DocumentTypeCode.ToLowerInvariant(), "The document uploaded 'Date' is not matched");

            var downloadPath = fileUtil.GetDownloadPath();
            const string filename = "Certification";
            fileUtil.DeleteFileInFolder(downloadPath, filename.ToLowerInvariant(), ".pdf");
            recipientSharedProfilePage.ClickOnDownloadDocumentButton();
            Assert.IsTrue(PlatformName != PlatformName.Web
                ? new MobileFileSelectionPo(Driver).IsFilePresentOnDevice(filename) : fileUtil.DoesFileExistInFolder(downloadPath, filename.ToLowerInvariant(), ".pdf", 15), $"File - {filename} not found!");

            Log.Info("Step 12: Click on 'Download Resume' button & verify resume gets downloaded");
            var downloadResumePath = fileUtil.GetDownloadPath();
            var resumeFileName = FusionMarketPlaceLoginCredentials.Name + "_Resume";
            fileUtil.DeleteFileInFolder(downloadResumePath, resumeFileName, ".pdf");
            recipientSharedProfilePage.ClickOnDownloadResumeButton();
            Assert.IsTrue(PlatformName != PlatformName.Web
                ? new MobileFileSelectionPo(Driver).IsFilePresentOnDevice(filename) : fileUtil.DoesFileExistInFolder(downloadResumePath, resumeFileName, ".pdf", 15), $"File - {filename} not found!");
        }

        [TestMethod]
        [DoNotParallelize]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void Profile_VerifyShareMyProfileDetailsWorksWithExistingUserSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var homePage = new HomePagePo(Driver);
            var email = new EmailPo(Driver);
            var emailList = new EmailListingGridPo(Driver);
            var recipientSharedProfilePage = new RecipientSharedProfilePo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to profile section, Get User's data & close the 'edit about me' popup");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnProfileMenuItem();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            profileDetail.ClickOnEditAboutMeButton();
            var expectedUserData = editProfile.GetEditAboutMeData();
            editProfile.ClickOnCancelButton();

            Log.Info("Step 5: Add details in 'Share My Profile' form & click on 'logout' button from profile menu");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOnStopSharing(ManageMyProfileSharingUser.Email);
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.AddShareMyProfileDetails(ManageMyProfileSharingUser.Email);
            profileSharing.ClickOnCloseIconAndClickOnLogout();

            Log.Info($"Step 6: Click on 'Login' button & login with user {ManageMyProfileSharingUser.Email}");
            headerHomePagePo.ClickOnLogInButton();
            fmpLogin.LoginToApplication(ManageMyProfileSharingUser);
            homePage.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 7: Open 'Shared Profile mail' from yopmail & click on 'View Shared Profile'");
            new WaitHelpers(Driver).HardWait(3000); // Waiting for 3 seconds for Shared profile mail
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(ManageMyProfileSharingUser.Email);
            emailList.OpenEmail($"{FusionMarketPlaceLoginCredentials.Name} has shared their Fusion Marketplace profile");
            const string confirmEmail = "View Shared Profile";
            emailList.ClickOnButtonOrLink(confirmEmail);
            recipientSharedProfilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 8: Verify URL & shared profile data is correct");
            var expectedSharedProfileUrl = FusionMarketPlaceUrl + "shared-profile";
            var downloadPath = fileUtil.GetDownloadPath();
            var filename = FusionMarketPlaceLoginCredentials.Name + "_Resume";
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".pdf");
            Assert.IsTrue(new WaitHelpers(Driver).WaitUntilUrlContains(expectedSharedProfileUrl), "Shared Profile Url is not correct");

            var expectedFirstAndLastName = expectedUserData.FirstName + " " + expectedUserData.LastName;
            var actualFirstAndLastName = recipientSharedProfilePage.GetFirstAndLastName();
            Assert.AreEqual(expectedFirstAndLastName, actualFirstAndLastName, "Shared Profile 'User name' is not matched");

            var expectedDepartmentSpecialty = expectedUserData.Category + " - " + expectedUserData.PrimarySpecialty;
            var actualDepartmentSpecialty = recipientSharedProfilePage.GetDepartmentAndSpecialtyText();
            Assert.AreEqual(expectedDepartmentSpecialty, actualDepartmentSpecialty, "Shared Profile 'DepartmentSpecialty' text is not matched");

            var actualEmail = recipientSharedProfilePage.GetEmailText();
            Assert.AreEqual(FusionMarketPlaceLoginCredentials.Email, actualEmail, "Shared Profile 'Email' text is not matched");

            recipientSharedProfilePage.ClickOnDownloadResumeButton();
            new WaitHelpers(Driver).HardWait(3000);
            Assert.IsTrue(PlatformName != PlatformName.Web
                ? new MobileFileSelectionPo(Driver).IsFilePresentOnDevice(filename) : fileUtil.DoesFileExistInFolder(downloadPath, filename, ".pdf", 15), $"File - {filename} not found!");
        }

        [TestMethod]
        [DoNotParallelize]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void Profile_VerifyThatProfileSharingWithNewRecipientAsAGuestUserWorksSuccessfully()
        {
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var email = new EmailPo(Driver);
            var emailList = new EmailListingGridPo(Driver);
            var sharedProfileLogInSignUp = new SharedProfileLogInPo(Driver);
            var signUpPage = new SignUpPo(Driver);
            var aboutMe = new AboutMePo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var recipientSharedProfilePage = new RecipientSharedProfilePo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetailPage.NavigateToPage();

            Log.Info("Step 5: Click on 'Manage My Profile Sharing' button, click on 'Add Recipient Via Email' button");
            profileDetailPage.ClickOnEditAboutMeButton();
            var expectedUserData = editProfile.GetEditAboutMeData();
            editProfile.ClickOnCancelButton();
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOAddRecipientViaEmailButton();

            Log.Info("Step 6: Add details in 'Share My Profile' form, click on 'Share My Profile' button & click on 'My Profile Sharing' popup 'close' icon");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            profileSharing.AddShareMyProfileDetails(addAboutMeSignUpData.Email);

            Log.Info("Step 7: Click on user profile & Click on 'Log out' button");
            profileSharing.ClickOnCloseIconAndClickOnLogout();

            Log.Info("Step 8: Open 'YopMail', Open your 'Shared Email', click on 'Get Started' button & verify Login/Signup page gets open");
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 30 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);
            emailList.OpenEmail($"{FusionMarketPlaceLoginCredentials.Name} has shared their Fusion Marketplace profile");
            var actualSenderEmail = emailList.GetSenderEmailText();
            Assert.AreEqual(GlobalConstants.SenderEmailText, actualSenderEmail, "The Sender email doesn't match");

            const string getStartedButton = "Get Started";
            emailList.ClickOnButtonOrLink(getStartedButton);
            const string expectedLogInSignUpPageHeaderText = "Welcome!";
            var actualLogInSignUpPageHeaderText = sharedProfileLogInSignUp.GetSharedProfileLogInSignUpHeaderText();
            Assert.AreEqual(expectedLogInSignUpPageHeaderText, actualLogInSignUpPageHeaderText, "Shared profile Login/SignUp page is not opened.");

            Log.Info("Step 9: Click on 'Sign up here' link & verify Guest user 'Sign Up' page gets open");
            sharedProfileLogInSignUp.ClickOnSignUpAsAGuestLink();
            const string expectedTitle = "sign up";
            var actualTitle = signUpPage.GetSignUpPageHeader().ToLower();
            Assert.AreEqual(expectedTitle, actualTitle, "Sign up Title is not matched");

            Log.Info("Step 10: Signing up as a new user and Verify user email text box is disabled");
            var expectedEmail = aboutMe.GetEmail();
            Assert.AreEqual(expectedEmail, addAboutMeSignUpData.Email, "Email does not match");

            Log.Info("Step 11: Signing up as a guest user and login as guest user");
            aboutMe.AddGuestUserDataInSignUpForm(addAboutMeSignUpData);
            passwordPage.FillFormAndSubmit(addAboutMeSignUpData);
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 30 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);
            emailList.OpenEmail("Confirm Email");
            const string confirmEmail = "Confirm Email";
            emailList.ClickOnButtonOrLink(confirmEmail);
            confirmPage.ClickOnConfirmationLogInButton();
            marketPlaceLogin.LoginToApplication(addAboutMeSignUpData);
            profileDetailPage.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 12: Verify the shared profile details are displayed correctly on recipient profile");
            var expectedSharedProfileUrl = FusionMarketPlaceUrl + "shared-profile";
            Assert.IsTrue(new WaitHelpers(Driver).WaitUntilUrlContains(expectedSharedProfileUrl), "Shared Profile Url is not correct");

            var expectedFirstAndLastName = expectedUserData.FirstName + " " + expectedUserData.LastName;
            var actualFirstAndLastName = recipientSharedProfilePage.GetFirstAndLastName();
            var expectedDepartmentSpecialty = expectedUserData.Category + " - " + expectedUserData.PrimarySpecialty;
            var actualDepartmentSpecialty = recipientSharedProfilePage.GetDepartmentAndSpecialtyText();
            var actualEmail = recipientSharedProfilePage.GetEmailText();

            Assert.AreEqual(expectedFirstAndLastName, actualFirstAndLastName, "Shared Profile 'User name' is not matched");
            Assert.AreEqual(expectedDepartmentSpecialty, actualDepartmentSpecialty, "Shared Profile 'DepartmentSpecialty' is not matched");
            Assert.AreEqual(FusionMarketPlaceLoginCredentials.Email, actualEmail, "Shared Profile 'Email' is not matched");

            var downloadPath = fileUtil.GetDownloadPath();
            var filename = FusionMarketPlaceLoginCredentials.Name + "_Resume";
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".pdf");
            recipientSharedProfilePage.ClickOnDownloadResumeButton();
            Assert.IsTrue(PlatformName != PlatformName.Web
                ? new MobileFileSelectionPo(Driver).IsFilePresentOnDevice(filename) : fileUtil.DoesFileExistInFolder(downloadPath, filename, ".pdf", 15), $"File - {filename} not found!");
        }
    }
}

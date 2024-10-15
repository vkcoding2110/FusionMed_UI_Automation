using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile
{

    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class EditAboutMeTests2 : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatUserCanUploadImageSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var emailListingGrid = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var profileImage = new EditProfileImagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Signing up as a new user and login as new user");
            headerHomePagePo.ClickOnLogInButton();
            fmpLogin.ClickOnSignUpLink();
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);
            passwordPage.FillFormAndSubmit(addAboutMeSignUpData);
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 10 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);
            emailListingGrid.OpenEmail("Confirm Email");
            const string confirmEmail = "Confirm Email";
            emailListingGrid.ClickOnButtonOrLink(confirmEmail);
            confirmPage.ClickOnConfirmationLogInButton();
            fmpLogin.LoginToApplication(addAboutMeSignUpData);
            editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();
            editProfile.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedProfilePageUrl = FusionMarketPlaceUrl + "profile/";
            new WaitHelpers(Driver).WaitUntilUrlMatched(expectedProfilePageUrl, 120);

            Log.Info("Step 3: Click on profile image 'Pencil' button & Verify upload image popup gets open");
            profileImage.ClickOnProfileImagePencilButton();

            Log.Info("Step 4: Upload image, Click on 'Save' button & verify profile image gets uploaded");
            var uploadImage = AboutMeDataFactory.UploadProfileImage();
            profileImage.UploadPhotoFromDevice(uploadImage);
            var expectedUrl = "blob:" + FusionMarketPlaceUrl;
            Assert.IsTrue(profileImage.GetUploadedProfileImage().StartsWith(expectedUrl), "The uploaded is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatUploadImagePopupCancelButtonAndCloseIconWorksSuccessfully()
        {
            var profileDetails = new ProfileDetailPagePo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var profileImage = new EditProfileImagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on profile image 'Pencil' button");
            profileImage.ClickOnProfileImagePencilButton();

            Log.Info("Step 6: Click on upload image popup 'Cancel' button & verify popup gets close");
            profileImage.ClickOnUploadImageCancelButton();
            Assert.IsFalse(profileImage.IsZoomLabelDisplayed(), "The upload image popup is still opened");

            Log.Info("Step 7: Click on profile image 'Pencil' button");
            profileImage.ClickOnProfileImagePencilButton();

            Log.Info("Step 8: Click on upload image popup 'Close' icon & verify popup gets close");
            if (!PlatformName.Equals(PlatformName.Web)) return;
            profileImage.ClickOnUploadImageCloseIcon();
            Assert.IsFalse(profileImage.IsZoomLabelDisplayed(), "The upload image popup is still opened");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatUserCanEditProfileImageSuccessfully()
        {
            var signUpPage = new AboutMePo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var emailListingGrid = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var profileImage = new EditProfileImagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Signing up as a new user and login as new user");
            headerHomePagePo.ClickOnLogInButton();
            fmpLogin.ClickOnSignUpLink();
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);
            passwordPage.FillFormAndSubmit(addAboutMeSignUpData);
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 10 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);
            emailListingGrid.OpenEmail("Confirm Email");
            const string confirmEmail = "Confirm Email";
            emailListingGrid.ClickOnButtonOrLink(confirmEmail);
            confirmPage.ClickOnConfirmationLogInButton();
            fmpLogin.LoginToApplication(addAboutMeSignUpData);
            editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();
            editProfile.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedProfilePageUrl = FusionMarketPlaceUrl + "profile/";
            new WaitHelpers(Driver).WaitUntilUrlMatched(expectedProfilePageUrl, 120);

            Log.Info("Step 3: Click on profile image 'Pencil' button");
            profileImage.ClickOnProfileImagePencilButton();

            Log.Info("Step 4: Upload image, Click on 'Save' button & Get the profile image style url");
            var uploadImage = AboutMeDataFactory.UploadProfileImage();
            profileImage.UploadPhotoFromDevice(uploadImage);
            var expectedUploadImageStyle = profileImage.GetUploadedProfileImage();

            Log.Info("Step 5: Click on 'Pencil' button, click on 'Choose New Photo' button, edit profile image & verify profile image gets updated");
            var editImage = AboutMeDataFactory.EditProfileDetails();
            profileImage.EditProfileImage(editImage);
            profileImage.ClickOnSaveButton();

            var actualUploadImageStyle = profileImage.GetUploadedProfileImage();
            Assert.AreNotEqual(expectedUploadImageStyle, actualUploadImageStyle, "The image styles are same");
            var expectedUrl = "blob:" + FusionMarketPlaceUrl;
            Assert.IsTrue(profileImage.GetUploadedProfileImage().StartsWith(expectedUrl), "The image is updated successfully");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatImageDragAndDropWorkSuccessfully()
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var profileImage = new EditProfileImagePo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Pencil' button, click on 'Choose New Photo' button, edit profile image, click on image drag and drop scrollbar & verify profile image gets Zoom");
            var editImage = AboutMeDataFactory.EditProfileDetails();
            profileImage.EditProfileImageScrolling(editImage);
            const string expectedProfileImageZoomingSize = "2";
            var actualProfileImageZoomingSize = profileImage.GetImageZoomValue();
            Assert.AreEqual(expectedProfileImageZoomingSize, actualProfileImageZoomingSize, "The profile image doesn't zoom");

        }

        [TestMethod]
        public void VerifyValidationMessageIsDisplayedForFileUploadSizeGreaterThan8Mb()
        {
           
           var profileDetails = new ProfileDetailPagePo(Driver);
           var headerHomePagePo = new HeaderPo(Driver);
           var fmpLogin = new FmpLoginPo(Driver);
           var editProfile = new EditAboutMePo(Driver);
           var profileImage = new EditProfileImagePo(Driver);

           Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
           Driver.NavigateTo(FusionMarketPlaceUrl);

           Log.Info("Step 2: Click on 'Log In' button");
           headerHomePagePo.ClickOnLogInButton();

           Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
           fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
           editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();

           Log.Info("Step 4: Navigate to Profile Details page");
           profileDetails.NavigateToPage();

           Log.Info("Step 5: Upload image which is (>8 MB) and Verify validation message is displayed");
           var uploadImage = AboutMeDataFactory.UploadProfileImage();
           uploadImage.ImageFilePath = new FileUtil().GetBasePath() + "/TestData/FMP/TestDocuments/EditImageSizeGreaterThan8MB.jpg";
           profileImage.EditProfileImage(uploadImage);
           Assert.IsTrue(profileImage.IsValidationMessageDisplayedForFileUpload(), "Validation message is not displayed for image size (>8 MB)");

           if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
               return;

           Log.Info("Step 6: Upload .pdf file and Verify validation message is displayed");
           profileImage.ClickOnUploadImageCloseIcon();
           uploadImage.ImageFilePath = new FileUtil().GetBasePath() + "/TestData/FMP/TestDocuments/Resume.pdf";
           profileImage.EditProfileImage(uploadImage);
           Assert.IsTrue(profileImage.IsValidationMessageDisplayedForFileUpload(), "Validation message is not displayed for .pdf file type");
        }
    }
}

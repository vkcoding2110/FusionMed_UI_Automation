using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.DataFactory.FMP.Jobs.JobDetails;
using UIAutomation.DataFactory.FMP.Traveler.JobPreferences;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Traveler.JobPreferences;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.PageObjects.FMP.Traveler.JobPreferences;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;
using SearchPo = UIAutomation.PageObjects.FMP.Jobs.SearchPo;

namespace UIAutomation.Tests.FMP.Jobs.JobDetails
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMP")]
    public class JobsDetailsTests4 : FmpBaseTest
    {
        private static readonly Profile UserLogin = GetProfileUsersByType("QuickApplicationFormTest");
        private static readonly JobPreference PreferenceDetail = JobPreferenceDataFactory.AddPreferenceDetails();
        private static readonly Login UserLoginTest = GetLoginUsersByTypeAndPlatform("JobsRelatedToMeTest");

        [TestMethod]
        [TestCategory("Smoke")]
        [TestCategory("MobileReady")]
        public void VerifyQuickApplyOnJobWorkSuccessfullyForExistingUser()
        {
            var quickApply = new QuickApplyFormPo(Driver);
            var thankYou = new ThankYouPagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            searchPo.NavigateToPage();
            searchPo.WaitUntilFirstJobCardTitleGetDisplayed();

            Log.Info("Step 2: Click on 'Quick Apply' button & verify 'Collect info' popup is opened");
            var expectedJobTitle = searchPo.GetJobCardJobTitle();
            var expectedAgencyName = searchPo.GetJobCardAgencyName();
            searchPo.ClickOnFirstQuickApplyButton();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();
            if (!quickApply.IsSendNowButtonDisplayed()) return;
            Assert.IsTrue(quickApply.IsCollectInfoPopupOpened(), "Collect info popup is not opened");

            Log.Info("Step 3: Click on 'cancel' button & verify 'Collect info' popup closed");
            quickApply.ClickOnCancelOfInfoPopup();
            quickApply.WaitUntilCollectInfoPopupClosed();
            Assert.IsFalse(quickApply.IsCollectInfoPopupOpened(), "'collect info' popup is opened");

            Log.Info("Step 4: Click on 'Quick Apply' button & Fill up the data");
            searchPo.ClickOnFirstQuickApplyButton();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsFalse(quickApply.IsReferredByCheckboxSelected(), "'Someone referred me' checkbox is selected");

            var quickApplyData = QuickApplyDataFactory.AddQuickApplyInformation();
            quickApplyData.Email = FusionMarketPlaceLoginCredentials.Email;
            quickApply.AddQuickApplyFormData(quickApplyData);
            Assert.IsFalse(quickApply.IsShareProfileRadioButtonEnabled(), "Attach profile checkbox is enabled");
            quickApply.ClickOnSendNow();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5 : Verify Thanks message is matched and Click on 'Go to Login' button");
            var expectedThanksMessage = "Thanks for applying to" + expectedJobTitle + "at" + expectedAgencyName + "!";
            var actualThanksMessage = thankYou.GetThanksMessage();
            Assert.AreEqual(expectedThanksMessage.RemoveWhitespace(), actualThanksMessage.RemoveWhitespace(), "Thanks is not matched");
            thankYou.ClickOnGoToLoginAndCountMeInButton();

            Log.Info($"Step 6: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            quickApply.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 7:Verify that search page opened");
            var expectedUrl = FusionMarketPlaceUrl + "search/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedUrl), $"{expectedUrl} URL is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyQuickApplyOnJobWorkSuccessfullyForNewUser()
        {
            var quickApply = new QuickApplyFormPo(Driver);
            var thankYou = new ThankYouPagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var checkYouEmailPage = new CheckYourEmailPagePo(Driver);
            var emailList = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            searchPo.NavigateToPage();

            Log.Info("Step 2: Click on 'Quick Apply' button & Fill up the data");
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedJobTitle = searchPo.GetJobCardJobTitle();
            var expectedAgencyName = searchPo.GetJobCardAgencyName();
            searchPo.ClickOnFirstQuickApplyButton();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();
            if (!quickApply.IsSendNowButtonDisplayed()) return;
            var quickApplyData = QuickApplyDataFactory.AddQuickApplyInformation();
            quickApply.AddQuickApplyFormData(quickApplyData);
            quickApply.ClickOnSendNow();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3 : Verify Thanks message is matched and Click on 'Count me in' button");
            var expectedThanksMessage = "Thanks for applying to" + expectedJobTitle + "at" + expectedAgencyName + "!";
            var actualThanksMessage = thankYou.GetThanksMessage();
            Assert.AreEqual(expectedThanksMessage.RemoveWhitespace(), actualThanksMessage.RemoveWhitespace(), "Thanks is not matched");
            thankYou.ClickOnGoToLoginAndCountMeInButton();

            Log.Info("Step 4 : Add sign up data");
            if (fmpLogin.GetLoginPageHeader().ToLower().Equals("login")) return; //Defect id : 96
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            addAboutMeSignUpData.Email = quickApplyData.Email;
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);
            Assert.IsTrue(passwordPage.IsFilledProgressBarDisplayed(), "Filled Progressbar is not displayed");

            Log.Info("Step 5: Fill the valid password & click on 'Submit' button and verify 'Check Your Email' page is opened");
            passwordPage.FillFormAndSubmit(addAboutMeSignUpData);
            var actualCheckYourEmailMessage = checkYouEmailPage.GetCheckYourEmailMessageText();
            Assert.AreEqual(FmpConstants.CheckYourEmailMessage, actualCheckYourEmailMessage, "The Check Your Email Message doesn't match");

            Log.Info("Step 6: Open 'YopMail', Open your 'Confirm Email', click on 'Confirm Email' button");
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 30 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);

            emailList.OpenEmail("Confirm Email");
            var actualSenderEmail = emailList.GetSenderEmailText();
            Assert.AreEqual(GlobalConstants.SenderEmailText, actualSenderEmail, "The Sender email doesn't match");

            const string confirmEmail = "Confirm Email";
            emailList.ClickOnButtonOrLink(confirmEmail);

            Log.Info("Step 7: Click on 'Edit My Profile' button ");
            confirmPage.ClickOnConfirmationLogInButton();

            Log.Info("Step 8: Enter 'Login' credentials & verify profile page gets open and Welcome message is displayed.");
            fmpLogin.LoginToApplication(addAboutMeSignUpData);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            new WaitHelpers(Driver).HardWait(2000);
            var expectedProfilePageUrl = FusionMarketPlaceUrl + "profile/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedProfilePageUrl, 180), "Profile page url does not match");
        }
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyCorrectPreFilledDataIsDisplayedAndSubmitQuickApplyForm()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var quickApply = new QuickApplyFormPo(Driver);
            var thankYou = new ThankYouPagePo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();
            var loginDetails = new Login()
            {
                Email = UserLogin.Email,
                Password = UserLogin.Password
            };
            Log.Info($"Step 3: Login to application with credentials - Email:{loginDetails.Email}, password:{loginDetails.Password}");
            fmpLogin.LoginToApplication(loginDetails);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on 'Quick apply' button ,Verify that Correct user details is present on 'Quick apply' pop-up");
            searchPo.NavigateToPage();
            var expectedJobTitle = searchPo.GetJobCardJobTitle();
            var expectedAgencyName = searchPo.GetJobCardAgencyName();
            searchPo.ClickOnFirstQuickApplyButton();
            if (!quickApply.IsSendNowButtonDisplayed()) return;
            var actualData = quickApply.GetQuickApplyData();
            Assert.AreEqual(UserLogin.FirstName, actualData.FirstName, "First name is not matched.");
            Assert.AreEqual(UserLogin.LastName, actualData.LastName, "Last name is not matched.");
            Assert.AreEqual(UserLogin.Email, actualData.Email, "Email is not matched.");
            Assert.AreEqual(UserLogin.PhoneNumber, actualData.PhoneNumber, "Phone number is not matched.");

            Log.Info("Step 5: Add data in 'Quick apply' pop-up");
            var userData = QuickApplyDataFactory.AddQuickApplyInformation();
            quickApply.AddQuickApplyFormData(userData);

            Log.Info("Step 6: Verify 'Send my profile information Radio Button is selected or not and Selected Job title, Agency is displayed'");
            Assert.IsTrue(quickApply.IsShareProfileRadioButtonEnabled(), "Attach profile Radio Button is Not enabled");
            quickApply.ClickOnShareProfileInformationRadioButton();
            Assert.IsTrue(quickApply.IsShareProfileRadioButtonSelected(), "Attach profile Radio Button is not selected");
            quickApply.ClickOnShareProfileNoThanksRadioButton();
            Assert.IsFalse(quickApply.IsShareProfileRadioButtonSelected(), "Attach profile Radio Button is selected");
            quickApply.ClickOnSendNow();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7 : Verify Thanks message is matched, Click on 'Go to Profile' and Verify Profile page is opened");
            var expectedThanksMessage = "Thanks for applying to" + expectedJobTitle + "at" + expectedAgencyName + "!";
            var actualThanksMessage = thankYou.GetThanksMessage();
            Assert.AreEqual(expectedThanksMessage.RemoveWhitespace(), actualThanksMessage.RemoveWhitespace(), "Thanks message is not matched");
            thankYou.ClickOnGoToProfileButton();
            var expectedProfilePageUrl = FusionMarketPlaceUrl + "profile/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedProfilePageUrl, 180), "Profile page url does not match");
        }

        [TestMethod]
        public void VerifyApplyAnywayButtonWorkSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var quickApply = new QuickApplyFormPo(Driver);
            var thankYou = new ThankYouPagePo(Driver);
            var searchPo = new SearchPo(Driver);
            var jobPreferences = new JobPreferencePo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 3: Navigate to job preference page and add Job preference detail");
            jobPreferences.NavigateToPage();
            jobPreferences.EnterPreferenceDetail(PreferenceDetail);

            Log.Info("Step 4: Navigate to job search page ,Select category and Click on 'Show all results' button");
            Driver.Back();
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchPo.ClickOnSortAndFilterButton();
            const string filterOption = "Category";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            const string expectedCategoryOption = "Therapy";
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(expectedCategoryOption);
            sortAndFilter.ClickOnShowAllResultsButton();

            Log.Info("Step 5: Click on Quick apply button and Verify 'Apply anyway' button is displayed");
            searchPo.ClickOnQuickApplyButton();
            Assert.IsTrue(thankYou.IsApplyAnywayButtonDisplayed()," 'Apply anyway' button is not displayed");

            Log.Info("Step 6: Click on 'Apply Anyway' button and Verify Quick apply pop up is displayed");
            thankYou.ClickOnApplyAnywayButton();
            Assert.IsTrue(quickApply.IsCollectInfoPopupOpened(),"'Quick apply' popup is not displayed");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyJobsCardDisplayedAsPerSelectedCategoryFromProfile()
        {
            var headerPo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerPo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLoginTest.Email}, password:{UserLoginTest.Password}");
            fmpLogin.LoginToApplication(UserLoginTest);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Edit About Me' button and edit Category and specialty of user");
            profileDetails.ClickOnEditAboutMeButton();
            var editDetail = AboutMeDataFactory.EditProfileDetails();
            editProfile.SelectCategory(editDetail.Category);
            editProfile.SelectPrimarySpecialty(editDetail.PrimarySpecialty);
            editProfile.ClickOnSubmitButton();
            editProfile.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 6: Navigate to 'Jobs' list page & verify 'Jobs Related to me' text is displayed");
            searchPo.NavigateToPage();
            const string expectedJobsRelatedText = "Jobs related to me";
            var actualJobsRelatedText = searchPo.GetJobsRelatedToMeText();
            Assert.AreEqual(expectedJobsRelatedText, actualJobsRelatedText, "Jobs related to me text is not matched");

            Log.Info("Step 7: Verify the job card is displayed as per specified category from profile");
            var currentUrl = searchPo.GetJobNameFromCards();
            foreach (var href in currentUrl)
            {
                Assert.IsTrue(href.Contains(editDetail.PrimarySpecialty.ToLowerInvariant()), "Job card is not displayed as per selected category");
            }

        }
    }
}

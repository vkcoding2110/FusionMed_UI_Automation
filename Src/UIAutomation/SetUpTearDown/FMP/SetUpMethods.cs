using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Agencies.Recruiters.RateAndReview;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Account;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum.Recruiters;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.BrowseAll.Agencies;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.EducationAndCertification;
using UIAutomation.PageObjects.FMP.Traveler.Profile.Employment;
using UIAutomation.PageObjects.FMP.Traveler.Profile.Licensing;
using UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments;
using UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing;
using UIAutomation.PageObjects.FMP.Traveler.Profile.References;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.SetUpTearDown.FMP
{
    public class SetUpMethods : BaseTest
    {
        public SetUpMethods(TestContext testContext)
        {
            Log = new Logger(testContext);
            var fileUtil = new FileUtil();
            Log = new Logger($"{fileUtil.GetBasePath()}/Resources/Logs/[LOG]_{testContext.FullyQualifiedTestClassName}_{DateTime.Now:yyyy-MM-dd_HH.mm.ss}.log");
            Driver = DriverFactory.InitDriver(AppiumLocalService, Capability);
        }

        private const string ExploreMenuAgenciesButton = "Agencies";
        private const string AgencyName = "Fusion Medical Staffing";
        private const string RecruiterName = "Automation TestGuestUser";
        private static readonly RateAndReviewBase RateAndReviewDetails = RateAndReviewDataFactory.AddRateAndReviewDetail();

        public void DeleteEmploymentDetails(Login login)
        {
            try
            {
                var fmpHeader = new HeaderPo(Driver);
                var fmpLogin = new FmpLoginPo(Driver);
                var profileMenu = new ProfileMenuPo(Driver);
                var profileDetails = new ProfileDetailPagePo(Driver);
                var employmentDetails = new EmploymentDetailsPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                Log.Info("Step 2: Click on 'Log In' button");
                fmpHeader.ClickOnLogInButton();

                Log.Info($"Step 3: Login to application with credentials - Email:{login.Email}, password:{login.Password}");
                fmpLogin.LoginToApplication(login);
                fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

                Log.Info("Step 4: Click on 'Profile' icon & Click on 'Profile' arrow");
                fmpHeader.ClickOnProfileIcon();
                profileMenu.ClickOnProfileArrow();
                profileDetails.WaitUntilFmpPageLoadingIndicatorInvisible();

                Log.Info("Step 5: Delete all Employments");
                employmentDetails.DeleteAllEmployment();
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }

        public void DeleteLicenseDetails()
        {
            try
            {
                var headerHomePagePo = new HeaderPo(Driver);
                var fmpLogin = new FmpLoginPo(Driver);
                var profilePage = new ProfileMenuPo(Driver);
                var licensingDetail = new LicenseDetailsPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                Log.Info("Step 2: Click on 'Log In' button");
                headerHomePagePo.ClickOnLogInButton();

                Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
                fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
                headerHomePagePo.WaitUntilFmpTextLoadingIndicatorInvisible();

                Log.Info("Step 4: Go to Profile and Delete all Licensing details. ");
                headerHomePagePo.ClickOnProfileIcon();
                profilePage.ClickOnProfileMenuItem();
                headerHomePagePo.WaitUntilFmpPageLoadingIndicatorInvisible();
                licensingDetail.DeleteAllLicenseDetails();
            }
            catch
            {
                //Nothing
            }
            finally
            {
                Driver.Quit();
            }
        }

        public void DeleteAllEducationAndCertificationsDetails(Login login)
        {
            try
            {
                var fmpHeader = new HeaderPo(Driver);
                var fmpLogin = new FmpLoginPo(Driver);
                var profileMenu = new ProfileMenuPo(Driver);
                var profileDetails = new ProfileDetailPagePo(Driver);
                var educationDetailPage = new EducationAndCertificationDetailPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                Log.Info("Step 2: Click on 'Log In' button");
                fmpHeader.ClickOnLogInButton();

                Log.Info($"Step 3: Login to application with credentials - Email:{login.Email}, password:{login.Password}");
                fmpLogin.LoginToApplication(login);
                fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

                Log.Info("Step 4: Click on 'Profile' icon & Click on 'Profile' arrow");
                fmpHeader.ClickOnProfileIcon();
                profileMenu.ClickOnProfileArrow();
                profileDetails.WaitUntilFmpPageLoadingIndicatorInvisible();

                Log.Info("Step 5: Delete all Educations & Certifications");
                educationDetailPage.DeleteAllEducationOrCertification();
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }

        public void DeleteAllDocumentsDetails(Login login)
        {
            try
            {
                var fmpLogin = new FmpLoginPo(Driver);
                var fmpHeader = new HeaderPo(Driver);
                var profileDetails = new ProfileDetailPagePo(Driver);
                var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                Log.Info("Step 2: Click on 'Log In' button");
                fmpHeader.ClickOnLogInButton();

                Log.Info($"Step 3: Login to application with credentials - Email:{login.Email}, password:{login.Password}");
                fmpLogin.LoginToApplication(login);
                fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

                Log.Info("Step 4: Navigate to Profile Details page");
                profileDetails.NavigateToPage();

                Log.Info("Step 5: Delete all document details");
                myDocumentsDetails.DeleteAllDocumentsDetails();
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }
        public void DeleteReferenceDetails(Login login)
        {
            try
            {
                var headerHomePagePo = new HeaderPo(Driver);
                var fmpLogin = new FmpLoginPo(Driver);
                var profilePage = new ProfileMenuPo(Driver);
                var referencePo = new ReferencePo(Driver);
                var profileDetail = new ProfileDetailPagePo(Driver);
                var employmentDetails = new EmploymentDetailsPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                Log.Info("Step 2: Click on 'Log In' button");
                headerHomePagePo.ClickOnLogInButton();

                Log.Info($"Step 3: Login to application with credentials - Email:{login.Email}, password:{login.Password}");
                fmpLogin.LoginToApplication(login);
                headerHomePagePo.WaitUntilFmpTextLoadingIndicatorInvisible();

                Log.Info("Step 4: Go to Profile and Delete all Reference details. ");
                headerHomePagePo.ClickOnProfileIcon();
                profilePage.ClickOnProfileMenuItem();
                headerHomePagePo.WaitUntilFmpPageLoadingIndicatorInvisible();
                profileDetail.ClickOnReferenceTab();
                referencePo.DeleteAllReferenceDetail();
                profileDetail.ClickOnEmploymentTab();
                employmentDetails.DeleteAllEmployment();
            }
            catch
            {
                //Nothing
            }
            finally
            {
                Driver.Quit();
            }
        }
        public void CreateUser(SignUp signUp, UserType user = UserType.Traveler)
        {
            try
            {
                var fmpLogin = new FmpLoginPo(Driver);
                var headerHomePagePo = new HeaderPo(Driver);
                var passwordPage = new PasswordPo(Driver);
                var emailListingGrid = new EmailListingGridPo(Driver);
                var email = new EmailPo(Driver);
                var confirmPage = new ConfirmPagePo(Driver);
                var exploreMenu = new ExploreMenuPo(Driver);
                var agency = new AgencyDetailPo(Driver);
                var recruitersPo = new RecruiterListingPo(Driver);
                var rateAndReviewPo = new RateAndReviewPo(Driver);
                var recruiterDetail = new RecruiterDetailsPo(Driver);
                var signUpPage = new AboutMePo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                switch (user)
                {
                    case UserType.Traveler:
                        Log.Info("Step 2: Signing up as a new user and login to the site");
                        headerHomePagePo.ClickOnLogInButton();
                        fmpLogin.ClickOnSignUpLink();
                        break;
                    case UserType.Guest:
                        Log.Info("Step 2: Click on 'Browse All' button");
                        headerHomePagePo.ClickOnBrowseAllButton();

                        Log.Info("Step 3: Click on 'Agencies' menu item & Click on 'Fusion Medical Staffing' agency from the list");
                        exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
                        exploreMenu.ClickOnAgencyMenuItem(AgencyName);

                        Log.Info("Step 4: Click on 'View all recruiter' link , click on 'Guest recruiter' and verify recruiter page url ");
                        agency.ClickOnViewAllRecruiterLink();
                        recruitersPo.ClickOnRecruiterCard(RecruiterName);
                        recruiterDetail.ClickOnReviewRecruiterButton();

                        Log.Info("Step 5: Click on 'Sign up as a guest' link and verify sign up page is open ");
                        rateAndReviewPo.ClickOnSignUpHereLink();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(user), user, null);
                }

                Log.Info("Step 2: Fill data for 'ABOUT ME' in 'Sign Up' form & click on 'Continue' button");
                signUpPage.AddDataAboutMeInSignUpForm(signUp);
                passwordPage.FillFormAndSubmit(signUp);
                new WaitHelpers(Driver).HardWait(10000); // Waiting for 10 seconds for registration email

                Log.Info("Step 3: Open 'YopMail', Open your 'Confirm Email', click on 'Confirm Email' button");
                Driver.NavigateTo(new GlobalConstants().YopMailInbox);
                email.EnterEmailAddress(signUp.Email);
                emailListingGrid.OpenEmail("Confirm Email");
                const string confirmEmail = "Confirm Email";
                emailListingGrid.ClickOnButtonOrLink(confirmEmail);
                confirmPage.ClickOnConfirmationLogInButton();
                fmpLogin.LoginToApplication(signUp);
            }
            catch
            {
                //Nothing
            }
            finally
            {
                Driver.Quit();
            }
        }

        public void DeleteProfileSharingDetails()
        {
            try
            {
                var profilePage = new ProfileMenuPo(Driver);
                var marketPlaceLogin = new FmpLoginPo(Driver);
                var headerHomePagePo = new HeaderPo(Driver);
                var profileSharing = new ProfileSharingPo(Driver);
                var profileDetailPage = new ProfileDetailPagePo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                Log.Info("Step 2: Click on 'Log In' button");
                headerHomePagePo.ClickOnLogInButton();

                Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
                marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
                profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

                Log.Info("Step 4: Navigate to Profile Details page");
                headerHomePagePo.ClickOnProfileIcon();
                profilePage.ClickOnProfileMenuItem();
                profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

                Log.Info("Step 5: Click on 'Manage My Profile Sharing' button & Delete all Emails");
                profileDetailPage.ClickOnManageMyProfileSharingButton();
                profileSharing.DeleteAllProfileSharingEmails();
            }
            catch
            {
                //Nothing
            }
            finally
            {
                Driver.Quit();
            }
        }
        public void AddReviewForRecruiter(Login travelerLogin, TravelerRateAndReview travelerReviewData, string recruiterName)
        {
            try
            {
                var fmpHeader = new HeaderPo(Driver);
                var fmpLogin = new FmpLoginPo(Driver);
                var exploreMenu = new ExploreMenuPo(Driver);
                var agency = new AgencyDetailPo(Driver);
                var recruitersPo = new RecruiterListingPo(Driver);
                var recruiterDetail = new RecruiterDetailsPo(Driver);
                var starRatingPo = new StepRatingPo(Driver);
                var aboutMePo = new StepAboutMePo(Driver);
                var successPo = new StepSuccessPo(Driver);
                var scaleAndReviewPo = new StepReviewPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                Log.Info($"Step 2: Login to application with credentials - Email:{travelerLogin.Email}, password:{travelerLogin.Password}");
                fmpHeader.ClickOnLogInButton();
                fmpLogin.LoginToApplication(travelerLogin);
                fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

                Log.Info($"Step 3: Click on 'Browse All' button & Search for recruiter: {recruiterName}");
                fmpHeader.ClickOnBrowseAllButton();
                exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
                exploreMenu.ClickOnAgencyMenuItem(AgencyName);

                Log.Info($"Step 4: Add the review for the: {recruiterName}");
                agency.ClickOnViewAllRecruiterLink();
                recruitersPo.ClickOnRecruiterCard(recruiterName);
                recruiterDetail.ClickOnReviewRecruiterButton();
                aboutMePo.SelectTravelJobsDropdown(RateAndReviewDetails.NumberOfTravelJobs);
                aboutMePo.ClickOnNextButton();
                starRatingPo.GiveOverAllRating(travelerReviewData.OverallRating);
                starRatingPo.GiveAbilitiesRating(travelerReviewData);
                aboutMePo.ClickOnNextButton();
                scaleAndReviewPo.ScrollRateAndReviewScale(travelerReviewData);
                scaleAndReviewPo.AddReviewForRecruiter(travelerReviewData.ReviewMessage);
                aboutMePo.ClickOnNextButton();
                aboutMePo.WaitUntilFmpPageLoadingIndicatorInvisible();
                successPo.ClickOnSubmitReviewButton();
                successPo.WaitUntilFmpTextLoadingIndicatorInvisible();
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }

        public void ShareProfileWithTraveler(IList<Login> users, string recruiterEmail, List<Document> documents = null)
        {
            try
            {
                var marketPlaceLogin = new FmpLoginPo(Driver);
                var headerHomePagePo = new HeaderPo(Driver);
                var profileSharing = new ProfileSharingPo(Driver);
                var profileDetailPage = new ProfileDetailPagePo(Driver);
                var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
                var addDocumentsPopUp = new AddDocumentPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                Driver.NavigateTo(FusionMarketPlaceUrl);

                foreach (var userType in users)
                {
                    Log.Info(
                        $"Step 1: Login to application with credentials - Email:{userType.Email}, password:{userType.Password}");
                    headerHomePagePo.ClickOnLogInButton();
                    marketPlaceLogin.LoginToApplication(userType);
                    profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

                    Log.Info("Step 2: Navigate to Profile Details page and Click on 'My document' tab");
                    profileDetailPage.NavigateToPage();

                    if (documents != null)
                    {
                        myDocumentsDetails.ClickOnMyDocumentsTabButton();
                        Log.Info(
                            "Step 3: Click on 'upload documents from your device' link, Select document, click on 'Upload Document' button & verify document gets added");
                        const int droppedDocumentRowNumber = 1;
                        addDocumentsPopUp.UploadDocumentFromDevice(documents.First(), droppedDocumentRowNumber);
                        addDocumentsPopUp.ClickOnUploadDocumentsButton();
                    }

                    Log.Info(
                        "Step 4: Click on 'Manage My Profile Sharing' button, click on 'Add Recipient Via Email' button");
                    profileDetailPage.ClickOnManageMyProfileSharingButton();
                    profileSharing.DeleteAllProfileSharingEmails();
                    profileSharing.ClickOAddRecipientViaEmailButton();
                    profileSharing.AddShareMyProfileDetails(recruiterEmail);
                    profileSharing.ClickOnCloseIconAndClickOnLogout();
                }
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }
    }
}

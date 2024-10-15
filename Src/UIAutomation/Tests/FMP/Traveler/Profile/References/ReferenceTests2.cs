using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.Employment;
using UIAutomation.PageObjects.FMP.Traveler.Profile.References;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.References
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class ReferenceTests2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("ReferenceTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteReferenceDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Reference_VerifyRecordIsAttachedToEmploymentForm()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var referenceDetail = new ReferenceDetailPo(Driver);
            var employmentDetail = new EmploymentDetailsPo(Driver);
            var referencePo = new ReferencePo(Driver);
            var employmentPo = new EmploymentPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Add employment detail if Employment history is not filled");
            profileDetail.ClickOnReferenceTab();
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            if (referenceDetail.IsAddEmploymentLinkDisplayed())
            {
                referenceDetail.ClickOnAddEmploymentHistoryLink();
                employmentDetail.ClickOnAddPositionButton();
                employmentPo.EnterAddEmploymentData(addEmploymentData);
                employmentPo.WaitUntilFmpPageLoadingIndicatorInvisible();
                profileDetail.ClickOnReferenceTab();
            }
            referenceDetail.ClickOnAddReferenceButton();

            Log.Info("Step 6: Enter 'Add Reference' details ");
            var referenceData = ReferencesDataFactory.AddReferenceDetail();
            referencePo.EnterReferenceData(referenceData);
            var expectedReferenceData = referenceDetail.GetReferenceFirstAndLastName();
            referencePo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Verify reference is attached to 'Edit employment' form ");
            profileDetail.ClickOnEmploymentTab();
            employmentDetail.ClickOnEditButton();
            var actualReferenceData = employmentPo.GetReferenceRecordDetail();
            Assert.AreEqual(FmpConstants.EmploymentDeleteRecordReferenceMessage, employmentPo.GetDeleteRecordReferenceMessage(), "Reference message is not matched");
            Assert.AreEqual(expectedReferenceData, actualReferenceData, "Attached reference is not matched");

            Log.Info("Step 8: Delete reference detail and Verify Reference message is not displayed in 'Edit employment' form");
            employmentPo.ClickOnCancelButton();
            profileDetail.ClickOnReferenceTab();
            referenceDetail.ClickOnEditButton();
            referencePo.ClickOnDeleteReferenceButton();
            referencePo.WaitUntilFmpPageLoadingIndicatorInvisible();
            profileDetail.ClickOnEmploymentTab();
            employmentDetail.ClickOnEditButton();
            Assert.IsFalse(employmentPo.IsDeleteRecordReferenceMessageDisplayed(), "Reference message is still displayed");

            //Clean up
            try
            {
                employmentPo.ClickOnDeleteEmploymentButton();
            }
            catch
            {
                //Do nothing
            }
        }
    }
}

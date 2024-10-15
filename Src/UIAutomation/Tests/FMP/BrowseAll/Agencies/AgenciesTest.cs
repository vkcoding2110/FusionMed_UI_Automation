using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Jobs.JobDetails;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.BrowseAll.Agencies;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.BrowseAll.Agencies
{
    [TestClass]
    [TestCategory("Agencies"), TestCategory("FMP")]
    public class AgenciesTest : FmpBaseTest
    {
        private readonly List<string> AgencyNames = GetAgencyByNames().Select(x => x.Name).OrderBy(n => n).ToList();
        private const string ExploreMenuAgencyButton = "Agencies";
        private static readonly Profile UserLogin = GetProfileUsersByType("QuickApplicationFormTest");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatAgencyMenuOpenedAndClosedSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item & Verify 'Agencies' menu popup gets open");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgencyButton);
            Assert.AreEqual(ExploreMenuAgencyButton, exploreMenu.GetAgencyMenuHeaderText(), "Agencies menu header text doesn't match");

            Log.Info("Step 4: Click on 'Back' Arrow & verify 'Explore' menu popup gets open");
            exploreMenu.ClickOnBackButton();
            Assert.IsTrue(exploreMenu.IsExploreMenuOpened(), "Explore menu is not opened");

            Log.Info("Step 5: Click on jobs menu 'Close' icon & verify popup gets closed");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgencyButton);
            exploreMenu.ClickOnExploreMenuCloseIcon();
            Assert.IsFalse(exploreMenu.IsExploreMenuOpened(), "Agency menu is still opened");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatAgencyMenuListItemsAreCorrect()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item & Verify Agencies list is matched");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgencyButton);
            var expectedAgencyList = AgencyNames;
            var actualAgencyList = exploreMenu.GetAgencyMenuList();
            foreach (var agencyName in FmpConstants.AgencyList)
            {
                actualAgencyList.Remove(agencyName);
            }
            CollectionAssert.AreEqual(expectedAgencyList.ToList(), actualAgencyList.ToList(), "Agency list is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyAgenciesDetailsAreCorrect()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agencyDetail = new AgencyDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgencyButton);
            var agencyList = AgencyNames;
            foreach (var agencyName in FmpConstants.AgencyList)
            {
                agencyList.Remove(agencyName);
            }
            var stepCount = 4;
            foreach (var item in agencyList)
            {
                Log.Info($"Step {stepCount}: Click on '{item}' to expand & Verify Agency Details are correct");

                exploreMenu.ClickOnAgencyMenuItem(item);
                var expectedAgencyDetail = GetAgencyByName(item);
                var actualAgencyUrl = Driver.GetCurrentUrl();
                var expectedAgencyUrl = FusionMarketPlaceUrl + "agencies/" + expectedAgencyDetail.AliasName + "/";
                var expectedMetaTitle = expectedAgencyDetail.AliasName.Replace("-", " ") + " | Jobs and Benefits";
                var actualMetaTitle = Driver.GetPageTitle().Replace(",", "");
                var actualAgencyDetail = agencyDetail.GetAgencyDetails();
                Assert.AreEqual(expectedMetaTitle.ToLowerInvariant(), actualMetaTitle.ToLowerInvariant(), "Meta title is not matched");
                Assert.AreEqual(expectedAgencyDetail.Name, actualAgencyDetail.Name, "Name is not matched");
                Assert.IsTrue(actualAgencyDetail.PhoneNumber.Contains(expectedAgencyDetail.PhoneNumber), "Phone number is not correct");
                Assert.AreEqual(expectedAgencyDetail.Location.ToLowerInvariant(), actualAgencyDetail.Location.ToLowerInvariant(), "Location is not matched");
                if (!expectedAgencyDetail.PaidTimeOff.IsNullOrEmpty())
                {
                    Assert.AreEqual(expectedAgencyDetail.PaidTimeOff, actualAgencyDetail.PaidTimeOff, "PaidTimeOff is not matched");
                }
                if (!expectedAgencyDetail.Plan401.IsNullOrEmpty())
                {
                    Assert.AreEqual(expectedAgencyDetail.Plan401, actualAgencyDetail.Plan401, "Plan401 is not matched");
                }
                if (!expectedAgencyDetail.ReferralBonus.IsNullOrEmpty())
                {
                    Assert.AreEqual(expectedAgencyDetail.ReferralBonus, actualAgencyDetail.ReferralBonus, "ReferralBonus is not matched");
                }
                if (!expectedAgencyDetail.Insurance.IsNullOrEmpty())
                {
                    Assert.AreEqual(expectedAgencyDetail.Insurance.RemoveWhitespace(), actualAgencyDetail.Insurance.RemoveWhitespace(), "Insurance is not matched");
                }
                if (!expectedAgencyDetail.OtherReimbursements.IsNullOrEmpty())
                {
                    Assert.AreEqual(expectedAgencyDetail.OtherReimbursements.RemoveWhitespace(), actualAgencyDetail.OtherReimbursements.RemoveWhitespace(), "OtherReimbursements is not matched");
                }
                Assert.AreEqual(expectedAgencyDetail.Url.TrimEnd('/'), actualAgencyDetail.Url.TrimEnd('/'), "Website url is not matched");
                Assert.AreEqual(expectedAgencyDetail.Details.RemoveWhitespace(), actualAgencyDetail.Details.RemoveWhitespace(), "Details is not matched");
                Assert.AreEqual(expectedAgencyUrl, actualAgencyUrl, "Url is not matched");
                fmpHeader.ClickOnBrowseAllButton();
                exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgencyButton);
                stepCount++;
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyQuickApplyOnAgencyWorkSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agencyDetail = new AgencyDetailPo(Driver);
            var quickApply = new QuickApplyFormPo(Driver);
            var thankYou = new ThankYouPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.WaitUntilFmpPageLoadingIndicatorInvisible();
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgencyButton);
            var agencyName = "Fusion Medical Staffing";
            exploreMenu.ClickOnAgencyMenuItem(agencyName);

            Log.Info("Step 4: Click on 'Quick apply' button and Verify particular agency pop-up gets open");
            if (!agencyDetail.IsQuickApplyButtonDisplayed()) return;
            agencyDetail.ClickOnAgencyQuickApplyButton();
            var actualAgencyName = quickApply.GetAgencyNameFromPopUp();
            Assert.AreEqual(agencyName, actualAgencyName, "Agency name is not matched");

            Log.Info("Step 5: Add details in 'Agency Quick Apply' pop-up");
            var quickApplyData = QuickApplyDataFactory.AddQuickApplyInformation();
            quickApply.AddQuickApplyFormData(quickApplyData);
            quickApply.ClickOnSendNow();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 6 : Verify Thanks message is matched ");
            var expectedThanksMessage = "Thanks for applying to" + quickApplyData.PrimarySpecialty + "at" + agencyName + "!";
            var actualThanksMessage = thankYou.GetThanksMessage();
            Assert.AreEqual(expectedThanksMessage.RemoveWhitespace(), actualThanksMessage.RemoveWhitespace(), "Thanks message is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyQuickApplyOnAgencyWorkSuccessfullyAfterUserLogin()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var quickApply = new QuickApplyFormPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agencyDetail = new AgencyDetailPo(Driver);
            var thankYou = new ThankYouPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();
            var loginDetails = new Login()
            {
                Email = UserLogin.Email,
                Password = UserLogin.Password
            };

            Log.Info($"Step 3: Login to application with credentials - Email:{loginDetails.Email}, password:{loginDetails.Password}");
            fmpLogin.LoginToApplication(loginDetails);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on 'Browse All' button and Navigate to Given Agency");
            fmpHeader.ClickOnBrowseAllButton();
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgencyButton);
            var agencyName = "Fusion Medical Staffing";
            exploreMenu.ClickOnAgencyMenuItem(agencyName);
            if (!agencyDetail.IsQuickApplyButtonDisplayed()) return;
            agencyDetail.ClickOnAgencyQuickApplyButton();
            Log.Info("Step 5: Verify that Correct user details is present on Agency 'Quick apply' pop-up");
            var actualData = quickApply.GetQuickApplyData();
            Assert.AreEqual(UserLogin.FirstName, actualData.FirstName, "First name is not matched.");
            Assert.AreEqual(UserLogin.LastName, actualData.LastName, "Last name is not matched.");
            Assert.AreEqual(UserLogin.Email, actualData.Email, "Email is not matched.");
            Assert.AreEqual(UserLogin.PhoneNumber, actualData.PhoneNumber, "Phone number is not matched.");
            Assert.AreEqual(UserLogin.Category, actualData.Category, "Category is not matched.");
            Assert.AreEqual(UserLogin.PrimarySpecialty, actualData.PrimarySpecialty, "Primary specialty is not matched.");

            Log.Info("Step 6: Add data in 'Quick apply' pop-up");
            var userData = QuickApplyDataFactory.AddQuickApplyInformation();
            quickApply.AddQuickApplyFormData(userData);
            quickApply.ClickOnSendNow();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7 : Verify Thanks message is matched ");
            var expectedThanksMessage = "Thanks for applying to" + userData.PrimarySpecialty + "at" + agencyName + "!";
            var actualThanksMessage = thankYou.GetThanksMessage();
            Assert.AreEqual(expectedThanksMessage.RemoveWhitespace(), actualThanksMessage.RemoveWhitespace(), "Thanks message is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyValidationMessageIsDisplayedOnAgencyQuickApplyPopUp()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agencyDetail = new AgencyDetailPo(Driver);
            var quickApply = new QuickApplyFormPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgencyButton);
            const string agencyName = "Fusion Medical Staffing";
            exploreMenu.ClickOnAgencyMenuItem(agencyName);

            Log.Info("Step 4: Add details in 'Agency Quick Apply' pop-up");
            if (!agencyDetail.IsQuickApplyButtonDisplayed()) return;
            agencyDetail.ClickOnAgencyQuickApplyButton();
            var quickApplyData = QuickApplyDataFactory.AddQuickApplyInformation();

            Log.Info("Step 5: Clear 'First name' field and Click on 'Send now' button  and Verify Validation message is displayed for first name field ");
            quickApply.EnterFirstName(quickApplyData.FirstName);
            quickApply.EnterFirstName("");
            quickApply.EnterLastName(quickApplyData.LastName);
            quickApply.EnterEmail(quickApplyData.Email);
            quickApply.EnterMobilePhoneNumber(quickApplyData.PhoneNumber);
            quickApply.SelectCategory(quickApplyData.Category);
            quickApply.SelectPrimarySpecialty(quickApplyData.PrimarySpecialty);
            quickApply.ClickOnSomeoneReferredMe(quickApplyData.SomeoneReferredMe);
            quickApply.EnterReferredByText(quickApplyData.ReferredBy);
            quickApply.ClickOnSendNow();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForFirstNameField(), "Validation message is not displayed for First name field");

            Log.Info("Step 6: Clear 'Last name' field and Click on 'Send now' button  and Verify Validation message is displayed for last name field ");
            quickApply.EnterFirstName(quickApplyData.FirstName);
            quickApply.EnterLastName("");
            quickApply.ClickOnSendNow();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForLastNameField(), "Validation message is not displayed for Last name field");

            Log.Info("Step 7: Clear 'Email' field and Click on 'Send now' button  and Verify Validation message is displayed for Email field ");
            quickApply.EnterLastName(quickApplyData.LastName);
            quickApply.EnterEmail("");
            quickApply.ClickOnSendNow();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForEmailField(), "Validation message is not displayed for Email field");

            Log.Info("Step 8: Clear 'Mobile phone' field and Click on 'Send now' button  and Verify Validation message is displayed for Mobile phone field ");
            quickApply.EnterEmail(quickApplyData.Email);
            quickApply.EnterMobilePhoneNumber("");
            quickApply.ClickOnSendNow();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForMobilePhoneField(), "Validation message is not displayed for Mobile phone field");

            Log.Info("Step 9: Clear 'Category' field and Click on 'Send now' button  and Verify Validation message is displayed for Category field ");
            quickApply.EnterMobilePhoneNumber(quickApplyData.PhoneNumber);
            quickApply.ClearCategoryDropDown();
            quickApply.ClickOnSendNow();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForCategoryField(), "Validation message is not displayed for Category field");

            Log.Info("Step 10: Clear 'Primary Specialty' field and Click on 'Send now' button  and Verify Validation message is displayed for Primary Specialty field ");
            quickApply.SelectCategory(quickApplyData.Category);
            quickApply.ClearSpecialtyDropDown();
            quickApply.ClickOnSendNow();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForPrimarySpecialtyField(), "Validation message is not displayed for Primary Specialty field");

            Log.Info("Step 11: Clear 'ReferredBy' field and Click on 'Send now' button  and Verify Validation message is displayed for ReferredBy field ");
            quickApply.SelectPrimarySpecialty(quickApplyData.PrimarySpecialty);
            quickApply.EnterReferredByText("");
            quickApply.ClickOnSendNow();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForHearAboutUsField(), "Validation message is not displayed for ReferredBy field");
        }
    }
}

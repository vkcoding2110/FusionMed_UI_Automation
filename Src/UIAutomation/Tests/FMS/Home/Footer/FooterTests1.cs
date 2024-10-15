using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Home.Footer
{
    [TestClass]
    [TestCategory("Footer"), TestCategory("FMS")]
    public class FooterTests1 : BaseTest
    {

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void VerifyFusionMedicalStaffSectionLinksWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Fusion medical staff & verify about us page is opened");
            footer.ClickOnFusionMedStaff();
            var expectedFusionMedStaffUrl = FmsUrl + "about-us/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedFusionMedStaffUrl), $"{expectedFusionMedStaffUrl} About us page url is not matched");

            Log.Info("Step 3: Click on Careers & verify careers page is opened");
            Driver.Back();
            var expectedCareersUrl = FmsUrl + "about-us/careers/";
            Assert.AreEqual(expectedCareersUrl,footer.GetCareersLinkHref(), "Careers page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedCareersTitle = "Careers - Fusion Medical";
            // var actualCareersTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedCareersTitle, actualCareersTitle, " Careers page title is not matched");

            Log.Info("Step 4: Click on Contact Us & verify Contact Us page is opened");
            footer.ClickOnContactUs();
            var expectedContactUsUrl = FmsUrl + "about-us/contact-us/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedContactUsUrl), $"{expectedContactUsUrl} Contact Us page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedContactUsTitle = "Contact us - Fusion Medical";
            // var actualContactUsTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedContactUsTitle, actualContactUsTitle, "Contact Us page title is not matched");

            Log.Info("Step 5: Click on Student Outreach & verify Student Program page is opened");
            Driver.Back();
            footer.ClickOnStudentOutreach();
            var expectedStudentProgramUrl = FmsUrl + "student/mentorship-program/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedStudentProgramUrl), $"{expectedStudentProgramUrl} Student Program page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedStudentProgramTitle = "Student Outreach - Fusion Medical";
            // var actualStudentProgramTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedStudentProgramTitle, actualStudentProgramTitle, "Contact Us page title is not matched");

            Log.Info("Step 6: Click on Be The Change & verify Be The Change page is opened");
            Driver.Back();
            footer.ClickOnBeTheChange();
            const string expectedBeTheChangeUrl = "https://info.fusionmedstaff.com/be-the-change";
            Assert.IsTrue(Driver.IsUrlContains(expectedBeTheChangeUrl), $"{expectedBeTheChangeUrl} Be The Change page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedBeTheChangeTitle = "Be The Change | Fusion";
            // var actualBeTheChangeTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedBeTheChangeTitle, actualBeTheChangeTitle, "Be The Change page title is not matched");

            Log.Info("Step 7: Click on History & verify History page is opened");
            Driver.Back();
            footer.ClickOnHistory();
            var expectedHistoryUrl = FmsUrl + "about-us/history/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedHistoryUrl), $"{expectedHistoryUrl} History page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedHistoryTitle = "History - Fusion Medical";
            // var actualHistoryTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedHistoryTitle, actualHistoryTitle, "History page title is not matched");

            Log.Info("Step 8: Click on Online Time cards & verify Online Time cards page is opened");
            var actualOnlineTimeCardsHref = footer.GetOnlineTimeCardsHref();
            const string expectedOnlineTimeCardHref = "https://timesheets.fusionmedstaff.com/WFMS/login/WFMS/";
            Assert.AreEqual(expectedOnlineTimeCardHref,actualOnlineTimeCardsHref, "The 'Instagram' url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedOnlineTimeCardsTitle = "Web Timesheets";
            // var actualOnlineTimecardsTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedOnlineTimecardsTitle, actualOnlineTimecardsTitle, "Online Timecards page title is not matched");

            Log.Info("Step 9: Click on Skills Checklist & verify Skills Checklist page is opened");
            footer.ClickOnSkillsChecklist();
            var expectedSkillsChecklistUrl = FmsUrl + "apply/skills-checklist/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedSkillsChecklistUrl), $"{expectedSkillsChecklistUrl} Skills Checklist page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedSkillsChecklistTitle = "Skills Checklist - Fusion Medical";
            // var actualSkillsChecklistTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedSkillsChecklistTitle, actualSkillsChecklistTitle, "Skills Checklist page title is not matched");

            Log.Info("Step 10: Click on COVID 19 Response & verify employee safety page is opened");
            Driver.Back();
            footer.ClickOnCovid19Response();
            const string expectedCovid19ResponseUrl = "https://info.fusionmedstaff.com/employee-safety/";
            Assert.IsTrue(Driver.IsUrlContains(expectedCovid19ResponseUrl), $"{expectedCovid19ResponseUrl} employee safety page url is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void VerifyTravelerSectionLinksWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Traveler & verify Traveler page is opened");
            footer.ClickOnTraveler();
            var expectedTravelerUrl = FmsUrl + "traveler/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedTravelerUrl), $"{expectedTravelerUrl} Traveler page url is not matched");

            Log.Info("Step 3: Click on Nursing & verify Nursing page is opened");
            Driver.Back();
            footer.ClickOnOnNursing();
            var expectedNursingUrl = FmsUrl + "traveler/nursing/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedNursingUrl), $"{expectedNursingUrl} Nursing page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedNursingTitle = "Find the Best Travel Nurse Jobs with Benefits | Fusion Medical Staffing";
            // var actualNursingTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedNursingTitle, actualNursingTitle, "Nursing page title is not matched");

            Log.Info("Step 4: Click on Nursing Home Health & verify Nursing Home Health page is opened");
            Driver.Back();
            footer.ClickOnOnHomeHealth();
            var expectedHomeHealthUrl = FmsUrl + "traveler/home-health/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedHomeHealthUrl), $"{expectedHomeHealthUrl} Nursing Home Health page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedHomeHealthTitle = "Home Health - Fusion Medical";
            // var actualdHomeHealthTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedHomeHealthTitle, actualdHomeHealthTitle, "Home Health page title is not matched");

            Log.Info("Step 5: Click on Cath Lab & verify Cath Lab page is opened");
            Driver.Back();
            footer.ClickOnOnCathLab();
            var expectedCathLabUrl = FmsUrl + "traveler/cath-lab/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedCathLabUrl), $"{expectedCathLabUrl} Cath Lab page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedCathLabTitle = "Find the Best Travel Cath Lab Tech Jobs with Benefits | Fusion Medical Staffing";
            // var actualCathLabTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedCathLabTitle, actualCathLabTitle, "Cath Lab page title is not matched");

            Log.Info("Step 6: Click on Therapy & verify Therapy page is opened");
            Driver.Back();
            footer.ClickOnOnTherapy();
            var expectedTherapyUrl = FmsUrl + "traveler/therapy/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedTherapyUrl), $"{expectedTherapyUrl} Therapy page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedTherapyTitle = "Find the Best Travel Therapy Jobs with Benefits | Fusion Medical Staffing";
            // var actualTherapyTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedTherapyTitle, actualTherapyTitle, "Therapy page title is not matched");

            Log.Info("Step 7: Click on Laboratory & verify Laboratory page is opened");
            Driver.Back();
            footer.ClickOnOnLaboratory();
            var expectedLaboratoryUrl = FmsUrl + "traveler/laboratory/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedLaboratoryUrl), $"{expectedLaboratoryUrl} Laboratory page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedLaboratoryTitle = "Find the Best Travel Laboratory & MLT Jobs with Benefits | Fusion Medical Staffing";
            // var actualLaboratoryTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedLaboratoryTitle, actualLaboratoryTitle, "Laboratory page title is not matched");

            Log.Info("Step 8: Click on Cardiopulmonary & verify Cardiopulmonary page is opened");
            Driver.Back();
            footer.ClickOnOnCardiopulmonary();
            var expectedCardiopulmonaryUrl = FmsUrl + "traveler/cardiopulmonary/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedCardiopulmonaryUrl), $"{expectedCardiopulmonaryUrl} Cardiopulmonary page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedCardiopulmonaryTitle = "Find the Best Travel Cardiopulmonary Jobs with Benefits | Fusion Medical Staffing";
            // var actualCardiopulmonaryTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedCardiopulmonaryTitle, actualCardiopulmonaryTitle, "Cardiopulmonary page title is not matched");

            Log.Info("Step 9: Click on Long Term Care & verify Long Term Care page is opened");
            Driver.Back();
            footer.ClickOnOnLongTermCare();
            var expectedLongTermCareUrl = FmsUrl + "traveler/longtermcare/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedLongTermCareUrl), $"{expectedLongTermCareUrl} Long Term Care page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedLongTermCareTitle = "Find the Best Traveling LTC RN, LPN, & CNA Jobs | Fusion Medical Staffing";
            // var actualLongTermCareTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedLongTermCareTitle, actualLongTermCareTitle, "Long Term Care page title is not matched");

            Log.Info("Step 10: Click on Radiology & verify Radiology page is opened");
            Driver.Back();
            footer.ClickOnOnRadiology();
            var expectedRadiologyUrl = FmsUrl + "traveler/radiology/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedRadiologyUrl), $"{expectedRadiologyUrl} Radiology page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedRadiologyTitle = "Find the Best Travel Radiology Jobs with Benefits | Fusion Medical Staffing";
            // var actualRadiologyTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedRadiologyTitle, actualRadiologyTitle, "Radiology page title is not matched");

            Log.Info("Step 11: Click on Benefits & verify Benefits page is opened");
            Driver.Back();
            footer.ClickOnOnBenefits();
            var expectedBenefitsUrl = FmsUrl + "traveler/benefits/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedBenefitsUrl), $"{expectedBenefitsUrl} Benefits page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedBenefitsTitle = "Awesome Medical Benefits for Traveling Employees | Fusion Medical";
            // var actualBenefitsTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedBenefitsTitle, actualBenefitsTitle, "Benefits page title is not matched");

            Log.Info("Step 12: Click on Fusion Marketplace link & Verify Fusion marketplace page is opened");
            Driver.Back();
            var expectedFusionMarketPlaceHref = GlobalConstants.FusionMarketPlaceProductionUrl + "/healthcare-travelers/";
            var actualFusionMarketPlaceHref = footer.GetFusionMarketPlaceHref();
            Assert.IsTrue(actualFusionMarketPlaceHref.Contains(expectedFusionMarketPlaceHref), "Fusion marketplace Url title is not matched");

        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void VerifyHealthcareProvidersLinksWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Healthcare Provider & verify request staff page is opened");
            footer.ClickOnHealthcareProviders();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();
            var expectedHealthcareProviderUrl = FmsUrl + "client/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedHealthcareProviderUrl), $"{expectedHealthcareProviderUrl} Healthcare Provider page url is not matched");

            Log.Info("Step 3: Click on Request staff & verify request staff page is opened");
            Driver.Back();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();
            footer.ClickOnRequestStaff();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();
            Assert.IsTrue(Driver.IsUrlMatched(expectedHealthcareProviderUrl), $"{expectedHealthcareProviderUrl} Request staff page url is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void VerifyApplySectionLinksWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Apply and Verify Quick app page is opened.");
            footer.ClickOnApply();
            var expectedApplyUrl = FmsUrl + "apply/quick-application/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedApplyUrl), $"{expectedApplyUrl} Apply page url is not matched");

            Log.Info("Step 3: Click on Quick App & verify Quick App page is opened");
            Driver.Back();
            footer.ClickOnOnQuickApp();
            var expectedQuickAppUrl = FmsUrl + "apply/quick-application/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedQuickAppUrl), $"{expectedQuickAppUrl} Quick App page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedQuickAppTitle = "Quick application - Fusion Medical";
            // var actualQuickAppTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedQuickAppTitle, actualQuickAppTitle, "Quick App page title is not matched");

            Log.Info("Step 4: Click on Full App & verify Full App page is opened");
            Driver.Back();
            footer.ClickOnOnFullApp();
            var expectedFullAppUrl = FmsUrl + "apply/full-application/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedFullAppUrl), $"{expectedFullAppUrl} Full App page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedFullAppTitle = "Full application - Fusion Medical";
            // var actualFullAppTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedFullAppTitle, actualFullAppTitle, "Full App page title is not matched");

            Log.Info("Step 5: Click on Referral Bonus & verify Referral Bonus page is opened");
            Driver.Back();
            footer.ClickOnOnReferralBonus();
            var expectedReferralBonusUrl = FmsUrl + "referral-bonus/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedReferralBonusUrl), $"{expectedReferralBonusUrl} Referral Bonus page url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedReferralBonusTitle = "$500 Referral Bonus - Fusion Medical";
            // var actualReferralBonusTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedReferralBonusTitle, actualReferralBonusTitle, "Referral Bonus page title is not matched");

        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void VerifySocialMediaLinksWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Get Facebook icon href link & verify link is matched");
            const string expectedFacebookHref = "https://www.facebook.com/FusionMedStaff";
            var actualFacebookHref = footer.GetFacebookIconHref();
            Assert.AreEqual(expectedFacebookHref, actualFacebookHref, "Facebook Url is not matched");

            Log.Info("Step 3: Get Twitter icon href link & verify link is matched");
            const string expectedTwitterIconHref = "https://twitter.com/FusionMedStaff1";
            var actualTwitterPageTitle = footer.GetTwitterIconHref();
            Assert.AreEqual(expectedTwitterIconHref, actualTwitterPageTitle, "Twitter Page title is not matched");

            Log.Info("Step 4: Get instagram icon href link & verify link is matched");
            const string expectedInstagramIconHref = "https://www.instagram.com/fusionmedstaff/";
            var actualInstagramIconHref = footer.GetInstagramIconHref();
            Assert.AreEqual(expectedInstagramIconHref, actualInstagramIconHref, "Instagram Url is not matched");

            Log.Info("Step 5: Get Pinterest icon href link & verify link is matched");
            const string expectedPinterestIconHref = "https://www.pinterest.com/fusionmedstaff/";
            var actualPinterestIconHref = footer.GetPinterestIconHref();
            Assert.AreEqual(expectedPinterestIconHref, actualPinterestIconHref, "Pinterest Url is not matched");

            Log.Info("Step 6: Get LinkedIn icon href link & verify link is matched");
            const string expectedLinkedInIconHref = "https://www.linkedin.com/company/fusionmedicalstaffing";
            var actualLinkedInIconHref = footer.GetLinkedInIconHref();
            Assert.AreEqual(expectedLinkedInIconHref, actualLinkedInIconHref, "Linked in Url is not matched");

            Log.Info("Step 7: Get Spotify icon href link & verify link is matched");
            const string expectedSpotifyIconHref = "https://open.spotify.com/user/";
            var actualSpotifyIconHref = footer.GetSpotifyIconHref();
            Assert.IsTrue(actualSpotifyIconHref.Contains(expectedSpotifyIconHref), "Spotify Url title is not matched");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyPoliciesLinkWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Policies and Verify Employee rights page is open");
            footer.ClickOnPoliciesLink();
            var expectedPoliciesUrl = FmsUrl + "employee-rights/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedPoliciesUrl), $"{expectedPoliciesUrl} Employee rights page url is not matched");

            Log.Info("Step 3: Click on Privacy Policies and Verify Privacy Policies page is open");
            Driver.Back();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();
            footer.ClickOnPrivacyPoliciesLink();
            var expectedPrivacyPolicyUrl = FmsUrl + "privacy-policy/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedPrivacyPolicyUrl), $"{expectedPrivacyPolicyUrl} Privacy Policies page url is not matched");

            Log.Info("Step 4: Click on Terms of use and Verify Terms of use page is open");
            Driver.Back();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();
            footer.ClickOnTermsOfUseLink();
            var expectedTermsOfUseUrl = FmsUrl + "terms/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedTermsOfUseUrl), $"{expectedTermsOfUseUrl} Terms of use page url is not matched");

            Log.Info("Step 5: Get href of Joint Commission's Gold Seal of Approval");
            Driver.Back();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();
            const string expectedJointCommissionHref = "https://www.jointcommission.org/report_a_complaint.aspx";
            var actualJointCommissionHref = footer.GetJointCommissionApprovalHref();
            Assert.IsTrue(actualJointCommissionHref.Contains(expectedJointCommissionHref), "Joint commission Url is not matched");
        }
    }
}

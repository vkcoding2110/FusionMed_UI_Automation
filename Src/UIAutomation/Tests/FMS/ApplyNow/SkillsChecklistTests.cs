using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using UIAutomation.DataFactory.FMS.ApplyNow;
using UIAutomation.PageObjects.FMS.ApplyNow;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.ApplyNow
{
    [TestClass]
    [TestCategory("ApplyNow"), TestCategory("FMS")]
    public class SkillsChecklistTests : BaseTest
    {
        private readonly JObject JobTypeObject = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMS/Jsons/skills.json"));
        private readonly string SearchUrl = FmsUrl + "search/";

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifySkillsCheckListFormOpenedSuccessfully()
        {
            var quickApplication = new QuickApplicationPo(Driver);
            var homePage = new HomePagePo(Driver);
            var applyNow = new ApplyNowPo(Driver);

            Log.Info("Step 1: Navigate to 'Apply Now' page, click on 'Skills CheckList' link & verify 'Skills Checklist' page gets open");
            quickApplication.NavigateToPage();
            applyNow.ClickOnSkillsChecklistForm();
            applyNow.WaitUntilMpPageLoadingIndicatorInvisible();
            homePage.CloseReviewPopupIfPresent();
            const string expectedTitle = "Skills Checklist - Fusion Medical";
            var expectedUrl = FmsUrl + FmsConstants.SkillsCheckListUrl;
            var actualTitle = Driver.GetPageTitle();
            var actualUrl = Driver.GetCurrentUrl();
            Assert.AreEqual(expectedTitle, actualTitle, "Title is not matched");
            Assert.AreEqual(expectedUrl, actualUrl, "Page url is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifySkillsListMenuItemsSubItemsAreCorrect()
        {
            var homePage = new HomePagePo(Driver);
            var skillsChecklist = new SkillsChecklistPo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();

            string[] subMenus = { "Cardiopulmonary", "Cath Lab", "Lab", "Long Term Care", "Nursing", "Home Health", "Radiology", "Therapy" };
            var stepCount = 2;
            homePage.CloseReviewPopupIfPresent();
            foreach (var item in subMenus)
            {
                Log.Info($"Step {stepCount}: click on '{item}' category to expand & verify skills list is matched & click on '{item}' category to collapse");

                var expectedCategorySkillsList = new CSharpHelpers().GetJsonObjectChildrenToStringList(JobTypeObject,item);
                var actualCategorySkillsList = skillsChecklist.ClickOnCategoryToExpandTabAndGetSkills(item);

                foreach (var list in expectedCategorySkillsList)
                {
                    Assert.IsTrue(actualCategorySkillsList.Contains(list), "skills list item is not matched");
                }
                skillsChecklist.ClickOnCategoryToCollapseTab(item);
                stepCount++;
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatCardiopulmonaryFormSubmitWorksSuccessfully()
        {
            var skillsChecklist = new SkillsChecklistPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();
            skillsChecklist.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Cardiopulmonary' menu & Click on Sub-Menu option 'EEG Technician'");
            const string skillsMenuOption = "Cardiopulmonary";
            skillsChecklist.ClickOnCategoryToCollapseTab(skillsMenuOption);
            const string subList = "EEG Technician";
            skillsChecklist.ClickOnJobsSubList(subList);

            Log.Info($"Step 3: Enter data in '{subList}' sub menu option survey form & verify 'Thank You' page gets open successfully");
            var surveyData = SkillsCheckListDataFactory.AddDataInSurveyForm();
            skillsChecklist.EnterDataInSkillsCheckListSurveyForm(surveyData);
            skillsChecklist.WaitUntilMpSubmitFormLoadingIndicatorInvisible();

            var expectedUrl = FmsUrl + "apply/skills-checklist/thank-you/";
            new WaitHelpers(Driver).WaitUntilUrlContains(expectedUrl, 15);
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched: expected {expectedUrl}, actual: {Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.FirstName), $"First Name{surveyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.LastName), $"Last Name{surveyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.ApplicantPhone), $"Phone Number{surveyData.ApplicantPhone} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.AreEqual(FmsConstants.SkillChecklistSubmissionThanksForApplyText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Thank You page header text is not matched");

            Log.Info($"Step 4: Verify full app link present & matches with expected");
            const string fullAppFormName = "full application";
            Assert.IsTrue(thanksPage.IsFillOutFormUrlPresent(fullAppFormName), "Full Application link is not present");
            var expectedFullAppUrl = FmsUrl + FmsConstants.FullApplicationUrl;
            var actualFullAppUrl = thanksPage.GetFillOutAppHref(fullAppFormName);
            Assert.AreEqual(expectedFullAppUrl.ToLower(), actualFullAppUrl.ToLower(), "Full Application Href is not matched");


            Log.Info("Step 5: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(SearchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatCathLabFormSubmitWorksSuccessfully()
        {
            var skillsChecklist = new SkillsChecklistPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();
            skillsChecklist.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Cath Lab' menu & Click on Sub-Menu option 'Cath Lab RN'");
            const string skillsMenuOption = "Cath Lab";
            skillsChecklist.ClickOnCategoryToCollapseTab(skillsMenuOption);
            const string subList = "Cath Lab RN";
            skillsChecklist.ClickOnJobsSubList(subList);

            Log.Info($"Step 3: Enter data in '{subList}' sub menu option survey form & verify 'Thank You' page gets open successfully");
            var surveyData = SkillsCheckListDataFactory.AddDataInSurveyForm();
            skillsChecklist.EnterDataInSkillsCheckListSurveyForm(surveyData);
            skillsChecklist.WaitUntilMpSubmitFormLoadingIndicatorInvisible();

            var expectedUrl = FmsUrl + "apply/skills-checklist/thank-you/";
            new WaitHelpers(Driver).WaitUntilUrlContains(expectedUrl, 15);
            
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched: expected {expectedUrl}, actual:{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.FirstName), $"First Name{surveyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.LastName), $"Last Name{surveyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.ApplicantPhone), $"Phone Number{surveyData.ApplicantPhone} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.AreEqual(FmsConstants.SkillChecklistSubmissionThanksForApplyText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Thank You page header text is not matched");

            Log.Info("Step 4: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(SearchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatLabFormSubmitWorksSuccessfully()
        {
            var skillsChecklist = new SkillsChecklistPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();
            skillsChecklist.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Lab' menu & Click on Sub-Menu option 'Lab Assistant'");
            const string skillsMenuOption = "Lab";
            skillsChecklist.ClickOnCategoryToCollapseTab(skillsMenuOption);
            const string subList = "Lab Assistant";
            skillsChecklist.ClickOnJobsSubList(subList);

            Log.Info($"Step 3: Enter data in '{subList}' sub menu option survey form & verify 'Thank You' page gets open successfully");
            var surveyData = SkillsCheckListDataFactory.AddDataInSurveyForm();
            skillsChecklist.EnterDataInSkillsCheckListSurveyForm(surveyData);
            skillsChecklist.WaitUntilMpSubmitFormLoadingIndicatorInvisible();

            var expectedUrl = FmsUrl + "apply/skills-checklist/thank-you/";
            new WaitHelpers(Driver).WaitUntilUrlContains(expectedUrl, 15);
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched: expected {expectedUrl}, actual: {Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.FirstName), $"First Name{surveyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.LastName), $"Last Name{surveyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.ApplicantPhone), $"Phone Number{surveyData.ApplicantPhone} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.AreEqual(FmsConstants.SkillChecklistSubmissionThanksForApplyText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Thank You page header text is not matched");

            Log.Info("Step 4: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(SearchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatLongTermCareFormSubmitWorksSuccessfully()
        {
            var skillsChecklist = new SkillsChecklistPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();
            skillsChecklist.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Long Term Care' menu & Click on Sub-Menu option 'CNA'");
            const string skillsMenuOption = "Long Term Care";
            skillsChecklist.ClickOnCategoryToCollapseTab(skillsMenuOption);
            const string subList = "CNA";
            skillsChecklist.ClickOnJobsSubList(subList);

            Log.Info($"Step 3: Enter data in '{subList}' sub menu option survey form & verify 'Thank You' page gets open successfully");
            var surveyData = SkillsCheckListDataFactory.AddDataInSurveyForm();
            skillsChecklist.EnterDataInSkillsCheckListSurveyForm(surveyData);
            skillsChecklist.WaitUntilMpSubmitFormLoadingIndicatorInvisible();

            var expectedUrl = FmsUrl + "apply/skills-checklist/thank-you/";
            new WaitHelpers(Driver).WaitUntilUrlContains(expectedUrl, 15);
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched: expected {expectedUrl}, actual: {Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.FirstName), $"First Name{surveyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.LastName), $"Last Name{surveyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.ApplicantPhone), $"Phone Number{surveyData.ApplicantPhone} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.AreEqual(FmsConstants.SkillChecklistSubmissionThanksForApplyText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Thank You page header text is not matched");

            Log.Info("Step 4: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(SearchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatNursingFormSubmitWorksSuccessfully()
        {
            var skillsChecklist = new SkillsChecklistPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();
            skillsChecklist.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Nursing' menu & Click on Sub-Menu option 'Ambulatory RN'");
            const string skillsMenuOption = "Nursing";
            skillsChecklist.ClickOnCategoryToCollapseTab(skillsMenuOption);
            const string subList = "Ambulatory RN";
            skillsChecklist.ClickOnJobsSubList(subList);

            Log.Info($"Step 3: Enter data in '{subList}' sub menu option survey form & verify 'Thank You' page gets open successfully");
            var surveyData = SkillsCheckListDataFactory.AddDataInSurveyForm();
            skillsChecklist.EnterDataInSkillsCheckListSurveyForm(surveyData);
            skillsChecklist.WaitUntilMpSubmitFormLoadingIndicatorInvisible();

            var expectedUrl = FmsUrl + "apply/skills-checklist/thank-you/";
            new WaitHelpers(Driver).WaitUntilUrlContains(expectedUrl, 15);
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched: expected {expectedUrl}, actual: {Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.FirstName), $"First Name{surveyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.LastName), $"Last Name{surveyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.ApplicantPhone), $"Phone Number{surveyData.ApplicantPhone} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.AreEqual(FmsConstants.SkillChecklistSubmissionThanksForApplyText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Thank You page header text is not matched");

            Log.Info("Step 4: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(SearchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatHomeHealthFormSubmitWorksSuccessfully()
        {
            var skillsChecklist = new SkillsChecklistPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();
            skillsChecklist.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Home Health' menu & Click on Sub-Menu option 'Home Health RN'");
            const string skillsMenuOption = "Home Health";
            skillsChecklist.ClickOnCategoryToCollapseTab(skillsMenuOption);
            const string subList = "Home Health RN";
            skillsChecklist.ClickOnJobsSubList(subList);

            Log.Info($"Step 3: Enter data in '{subList}' sub menu option survey form & verify 'Thank You' page gets open successfully");
            var surveyData = SkillsCheckListDataFactory.AddDataInSurveyForm();
            skillsChecklist.EnterDataInSkillsCheckListSurveyForm(surveyData);
            skillsChecklist.WaitUntilMpSubmitFormLoadingIndicatorInvisible();

            var expectedUrl = FmsUrl + "apply/skills-checklist/thank-you/";
            new WaitHelpers(Driver).WaitUntilUrlContains(expectedUrl, 15);     
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched: expected {expectedUrl}, actual: {Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.FirstName), $"First Name{surveyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.LastName), $"Last Name{surveyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.ApplicantPhone), $"Phone Number{surveyData.ApplicantPhone} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.AreEqual(FmsConstants.SkillChecklistSubmissionThanksForApplyText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Thank You page header text is not matched");

            Log.Info("Step 4: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(SearchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatRadiologyFormSubmitWorksSuccessfully()
        {
            var skillsChecklist = new SkillsChecklistPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();
            skillsChecklist.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Radiology' menu & Click on Sub-Menu option 'Echo Technologist'");
            const string skillsMenuOption = "Radiology";
            skillsChecklist.ClickOnCategoryToCollapseTab(skillsMenuOption);
            const string subList = "Echo Technologist";
            skillsChecklist.ClickOnJobsSubList(subList);

            Log.Info($"Step 3: Enter data in '{subList}' sub menu option survey form & verify 'Thank You' page gets open successfully");
            var surveyData = SkillsCheckListDataFactory.AddDataInSurveyForm();
            skillsChecklist.EnterDataInSkillsCheckListSurveyForm(surveyData);
            skillsChecklist.WaitUntilMpSubmitFormLoadingIndicatorInvisible();

            var expectedUrl = FmsUrl + "apply/skills-checklist/thank-you/";
            new WaitHelpers(Driver).WaitUntilUrlContains(expectedUrl, 15);
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched: expected {expectedUrl}, actual: {Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.FirstName), $"First Name{surveyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.LastName), $"Last Name{surveyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.ApplicantPhone), $"Phone Number{surveyData.ApplicantPhone} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.AreEqual(FmsConstants.SkillChecklistSubmissionThanksForApplyText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Thank You page header text is not matched");

            Log.Info("Step 4: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(SearchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatTherapyFormSubmitWorksSuccessfully()
        {
            var skillsChecklist = new SkillsChecklistPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Skills CheckList' page");
            skillsChecklist.NavigateToPage();
            skillsChecklist.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Therapy' menu & Click on Sub-Menu option 'SLP'");
            const string skillsMenuOption = "Therapy";
            skillsChecklist.ClickOnCategoryToCollapseTab(skillsMenuOption);
            const string subList = "SLP";
            skillsChecklist.ClickOnJobsSubList(subList);

            Log.Info($"Step 3: Enter data in '{subList}' sub menu option survey form & verify 'Thank You' page gets open successfully");
            var surveyData = SkillsCheckListDataFactory.AddDataInSurveyForm();
            skillsChecklist.EnterDataInSkillsCheckListSurveyForm(surveyData);
            skillsChecklist.WaitUntilMpSubmitFormLoadingIndicatorInvisible();

            var expectedUrl = FmsUrl + "apply/skills-checklist/thank-you/";
            new WaitHelpers(Driver).WaitUntilUrlContains(expectedUrl, 15);         
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched: expected {expectedUrl}, actual: {Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.FirstName), $"First Name{surveyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.LastName), $"Last Name{surveyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(surveyData.ApplicantPhone), $"Phone Number{surveyData.ApplicantPhone} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.AreEqual(FmsConstants.SkillChecklistSubmissionThanksForApplyText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Thank You page header text is not matched");

            Log.Info("Step 4: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(SearchUrl), "Search 'URL' not matched");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using UIAutomation.DataObjects.FMS.ApplyNow;
using UIAutomation.PageObjects.Components;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.ApplyNow
{
    internal class SkillsChecklistPo : FmsBasePo
    {
        public SkillsChecklistPo(IWebDriver driver) : base(driver)
        {
        }

        private static By ExpandAndCollapseCategory(string category) => By.XPath($"//ul[contains(@class,'applystyles__QuickTabs')]//a[text()='{category}']");
        private static By JobList(string type) => By.XPath($"//a[text()='{type}']/following-sibling::ul//li//a");
        private static By JobsSubList(string subList) => By.XPath($"//li[contains(@class,'applystyles__SubTab')]/a[text()='{subList}']");
       
        //Survey form details
        private readonly By SurveyRadioButtonOptions = By.CssSelector(".gsurvey-survey-field");
        private readonly By OtherDetailsTextBox = By.XPath("//input[contains(@class,'InputStyled')]");
        private readonly By AgreementCheckbox = By.XPath("//div[@class='survey-checkbox']//label//span");
        private readonly By AgreementLabel = By.XPath("//*[text()='Application Agreement*']");
        private readonly By SignatureCanvas = By.XPath("//div[contains(@class,'inputTypesstyles__FormGroup')]//canvas[@class='sigCanvas']");
        private readonly By DateTextBox = By.XPath("//label[contains(text(),'Date')]/preceding-sibling::input[contains(@class,'form-control input')]/parent::div");
        private readonly By FirstNameTextBox = By.CssSelector("input#input_1000");
        private readonly By LastNameTextBox = By.CssSelector("input#input_1001");
        private readonly By CategoryInput = By.CssSelector("select#input_1002");
        private readonly By SpecialtyInput = By.CssSelector("select#input_1003");
        private readonly By EmailTextBox = By.CssSelector("input#input_10");
        private readonly By PhoneTextBox = By.CssSelector("input#input_9");
        private readonly By SubmitButton = By.XPath("//button[text()='Submit']");

        public IList<string> ClickOnCategoryToExpandTabAndGetSkills(string type)
        {
            Wait.UntilElementClickable(ExpandAndCollapseCategory(type)).ClickOn();
            return Wait.UntilAllElementsLocated(JobList(type)).Select(a => a.GetText()).ToList();
        }

        public void ClickOnCategoryToCollapseTab(string category)
        {
            Wait.UntilElementClickable(ExpandAndCollapseCategory(category)).ClickOn();
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}apply/skills-checklist/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        public void ClickOnJobsSubList(string subList)
        {
            Wait.UntilElementClickable(JobsSubList(subList)).ClickOn();
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        public void EnterDataInSkillsCheckListSurveyForm(SkillsCheckList skillsData)
        {
            var surveyList = Wait.UntilAllElementsLocated(SurveyRadioButtonOptions).Where(e => e.Displayed).ToList();
            var randomNumber = new Random();
           
            foreach (var e in surveyList)
            {
                var index = randomNumber.Next(5);
                e.FindElements(By.CssSelector("span.checkmark"))[index].ClickOn();
            }

            var textBoxList = Wait.UntilAllElementsLocated(OtherDetailsTextBox).Where(e => e.Displayed).ToList();
            textBoxList.RemoveAt(textBoxList.Count - 5);
            foreach (var e in textBoxList)
            {
                e.SendKeys(skillsData.OtherDetails);
            }

            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(AgreementLabel));
            Driver.JavaScriptScroll("0","10");
            Wait.UntilElementVisible(AgreementCheckbox).ClickOn();
            var actionBuilder = new Actions(Driver);
            actionBuilder.MoveToElement(Wait.UntilElementVisible(SignatureCanvas),100, 125).Click().ClickAndHold(Wait.UntilElementVisible(SignatureCanvas)).MoveByOffset(10, 10).MoveByOffset(-10, 10).MoveByOffset(15, 15).Release(Wait.UntilElementVisible(SignatureCanvas)).Build().Perform();
            var datePicker = new DatePickerPo(Driver);
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(DateTextBox));
            Driver.JavaScriptScroll("0", "10");
            datePicker.SelectDate(skillsData.Date, DateTextBox);
            Wait.UntilElementVisible(FirstNameTextBox).EnterText(skillsData.FirstName);
            Wait.UntilElementVisible(LastNameTextBox).EnterText(skillsData.LastName);
            Wait.UntilElementVisible(CategoryInput).SelectDropdownValueByText(skillsData.Category, Driver);
            Wait.UntilElementVisible(SpecialtyInput).SelectDropdownValueByText(skillsData.Specialty, Driver);
            Wait.UntilElementVisible(EmailTextBox).EnterText(skillsData.ApplicantEmail);
            Wait.UntilElementVisible(PhoneTextBox).EnterText(skillsData.ApplicantPhone);
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }
    }
}

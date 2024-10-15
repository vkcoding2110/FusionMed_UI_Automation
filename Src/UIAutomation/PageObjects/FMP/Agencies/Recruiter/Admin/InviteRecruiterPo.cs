using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin
{
    internal class InviteRecruiterPo : FmpBasePo
    {
        public InviteRecruiterPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FirstNameTextBox = By.CssSelector("input#edit-recruiter-first-name");
        private readonly By LastNameTextBox = By.CssSelector("input#edit-recruiter-last-name");
        private readonly By EmailTextBox = By.CssSelector("input#edit-recruiter-email");
        private readonly By PhoneNumberTextBox = By.CssSelector("input#edit-recruiter-mobile-number");
        private readonly By AboutMeTextArea = By.CssSelector("textarea#edit-recruiter-biography");
        private readonly By DepartmentDropDown = By.CssSelector("div#edit-recruiter-departments-select");
        private static By DepartmentCheckBox(string departmentOption) => By.XPath($"//ul[@aria-labelledby='edit-recruiter-departments-label']//li[text()='{departmentOption}']");
        private readonly By AddPhotoButton = By.XPath("//span[contains(text(),'Add Profile Photo')]");
        private readonly By UploadImageFileInput = By.XPath("//div[@class='dropped-files']/preceding-sibling::input");
        private readonly By SaveImageButton = By.XPath("//button[contains(@class,'CallToActionButton')]//span");
        private readonly By SendInviteButton = By.XPath("//span[text()='Send Invite']");
        private readonly By SaveButton = By.XPath("//span[text()='Save']");
        private readonly By CancelButton = By.XPath("//span[text()='cancel']");
        private readonly By CloseIcon = By.XPath("//button[contains(@class,'CloseIconWrapper')]");
        private readonly By DepartmentCloseIcon = By.XPath("//div[@id='edit-recruiter-departments-select']//button");

        // Validation 
        private readonly By ValidationMessageOfFirstNameField = By.CssSelector("p#edit-recruiter-first-name-helper-text");
        private readonly By ValidationMessageOfLastNameField = By.CssSelector("p#edit-recruiter-last-name-helper-text");
        private readonly By ValidationMessageOfEmailField = By.CssSelector("p#edit-recruiter-email-helper-text");
        private readonly By InviteRecruiterPopUp = By.XPath("//h5[contains(@class,'EditHeaderText')]");

        public string GetInviteRecruiterPopUpHeaderText()
        {
            return Wait.UntilElementVisible(InviteRecruiterPopUp).GetText();
        }
        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).Click();
        }
        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).Click();
        }
        public void ClickOnSendInviteButton()
        {
            Wait.UntilElementClickable(SendInviteButton).Click();
        }
        public void EnterRecruitersDetails(DataObjects.FMP.Agencies.Recruiter.Admin.Recruiter recruiters)
        {
            EnterFirstName(recruiters.FirstName);
            EnterLastName(recruiters.LastName);
            if (Wait.IsElementEnabled(EmailTextBox,3))
            {
                EnterEmail(recruiters.Email);
            }
            if (Wait.IsElementPresent(DepartmentCloseIcon, 3))
            {
                Wait.UntilElementClickable(DepartmentCloseIcon).Click();
                Wait.UntilElementInVisible(DepartmentCloseIcon);
            }
            Wait.UntilElementClickable(DepartmentDropDown).ClickOn();
            foreach (var department in recruiters.Department)
            {
                Wait.UntilElementClickable(DepartmentCheckBox(department)).ClickOn();
            }
            Wait.UntilElementClickable(DepartmentCheckBox(recruiters.Department.First())).PressEscKey();
            Wait.UntilElementVisible(PhoneNumberTextBox).EnterText(recruiters.PhoneNumber, true);
            Wait.UntilElementClickable(AboutMeTextArea).Click();
            Wait.UntilElementVisible(AboutMeTextArea).EnterText(recruiters.AboutMe, true);
            if (Wait.IsElementPresent(AddPhotoButton,3))
            {
                Driver.JavaScriptClickOn((AddPhotoButton));
                Wait.HardWait(2000);
                try
                {
                    Wait.UntilElementExists(UploadImageFileInput).SendKeys(recruiters.ImageFilePath);
                    Wait.HardWait(3000);
                    Driver.JavaScriptClickOn(SaveImageButton);
                    Wait.UntilElementInVisible(SaveImageButton, 5);
                }
                catch
                {
                    Driver.JavaScriptClickOn(SaveImageButton);
                }
            }
            if (Wait.IsElementPresent(SendInviteButton,3))
            {
               ClickOnSendInviteButton();
            }
            else
            {
                Wait.UntilElementClickable(SaveButton).ClickOn();
            }
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void EnterFirstName(string firstName)
        {
            Wait.UntilElementVisible(FirstNameTextBox).EnterText(firstName, true);
        }
        public void EnterLastName(string lastName)
        {
            Wait.UntilElementVisible(LastNameTextBox).EnterText(lastName, true);
        }
        public void EnterEmail(string email)
        {
            Wait.UntilElementVisible(EmailTextBox).EnterText(email, true);
        }
        public string GetValidationMessageOfFirstNameField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfFirstNameField, 10).GetText();
        }
        public string GetValidationMessageOfLastNameField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfLastNameField, 10).GetText();
        }
        public string GetValidationMessageOfEmailField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfEmailField, 10).GetText();
        }

        public bool IsInviteRecruiterPopUpDisplayed()
        {
            return Wait.IsElementPresent(CancelButton, 5);
        }
    }
}

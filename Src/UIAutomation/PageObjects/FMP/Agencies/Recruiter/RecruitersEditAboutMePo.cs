using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter
{
    internal class RecruitersEditAboutMePo : FmpBasePo
    {
        public RecruitersEditAboutMePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By EditAboutMeHeaderText = By.XPath("//button[contains(@class,'CloseIconWrapper')]/following-sibling::h5");
        private readonly By FirstNameInputBox = By.XPath("//input[@id='edit-recruiter-first-name']");
        private readonly By LastNameInputBox = By.XPath("//input[@id='edit-recruiter-last-name']");
        private readonly By EmailInputBox = By.XPath("//input[@id='edit-recruiter-email']");
        private readonly By DepartmentDropDown = By.XPath("//div[@id='edit-recruiter-departments-select']");
        private static By DepartmentCheckBox(string departmentOption) => By.XPath($"//li[normalize-space()='{departmentOption}']//input[@type='checkbox']/parent::span");
        private static By DepartmentCheckBoxEscape(string departmentOptionEscape) => By.XPath($"//li[normalize-space()='{departmentOptionEscape}']//input[@type='checkbox']");
        private readonly By DepartmentCloseIcon = By.XPath("//div[@id='edit-recruiter-departments-select']//button[contains(@class,'IconButtonStyled')]");
        private readonly By PhoneNumberTextBox = By.XPath("//input[@id='edit-recruiter-mobile-number']");
        private readonly By BioTextArea = By.XPath("//textarea[contains(@class,'inputMultiline')]");
        private readonly By SaveButton = By.XPath("//button[contains(@class,'EditButton')]/span[text()='Save']");
        private readonly By DepartmentValidationMessage = By.XPath("//p[contains(@class,'Mui-required')]");
        private readonly By CloseIcon = By.XPath("//button[contains(@class,'CloseIconWrapper')]");
        private readonly By CancelButton = By.XPath("//button[contains(@class,'EditButton')]/span[text()='cancel']");
        private readonly By EditProfileAvatar = By.XPath("//button[contains(@class,'PhotoEditButton')]");
        private readonly By AvatarCloseIcon = By.XPath("//div[contains(@class,'CloseIconWrapper')]");
        private readonly By AvatarCancelButton = By.XPath("//div[contains(@class,'CancelTouch')]/span[text()='cancel']");

        public string GetFirstNameInputBoxText()
        {         
            return Wait.UntilElementVisible(FirstNameInputBox).GetAttribute("value");
        }

        public string GetLastNameInputBoxText()
        {
            return Wait.UntilElementVisible(LastNameInputBox).GetAttribute("value");
        }

        public bool IsEmailInputBoxEnabled()
        {
            return Wait.IsElementEnabled(EmailInputBox, 3);
        }

        public void EnterBio(string bio)
        {
            Wait.UntilElementClickable(BioTextArea).Click();
            Wait.UntilElementVisible(BioTextArea).EnterText(bio, true);
        }

        public void EnterPhoneNumber(string number)
        {
            Wait.UntilElementVisible(PhoneNumberTextBox).EnterText(number, true);
        }

        public void ClickOnSaveButton()
        {
            Wait.UntilElementClickable(SaveButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnDepartmentCloseIcon()
        {
            if (Wait.IsElementPresent(DepartmentCloseIcon, 3))
            {
                Wait.UntilElementClickable(DepartmentCloseIcon).Click();
            }
        }

        public void EnterRecruitersDetails(DataObjects.FMP.Agencies.Recruiter.Admin.Recruiter recruiters)
        {
            ClickOnDepartmentCloseIcon();
            Wait.UntilElementClickable(DepartmentDropDown).ClickOn();
            foreach (var department in recruiters.Department)
            {
                Wait.UntilElementClickable(DepartmentCheckBox(department)).ClickOn();
            }
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(DepartmentCheckBox(recruiters.Department.First())));
            Wait.UntilElementExists(DepartmentCheckBoxEscape(recruiters.Department.First())).SendKeys(Keys.Escape);
            EnterPhoneNumber(recruiters.PhoneNumber);
            EnterBio(recruiters.AboutMe);
        }

        public bool IsEditAboutMePopUpPresent()
        {
            return Wait.IsElementPresent(EditAboutMeHeaderText, 3);
        }

        public string GetDepartmentValidationMessage()
        {
            return Wait.UntilElementVisible(DepartmentValidationMessage).GetText();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
        }

        public void ClickOnEditProfileAvatarButton()
        {
            Wait.UntilElementClickable(EditProfileAvatar).ClickOn();
        }

        public void ClickOnAvatarCloseIcon()
        {
            Wait.UntilElementClickable(AvatarCloseIcon).ClickOn();
        }

        public void ClickOnAvatarCancelButton()
        {
            Wait.UntilElementClickable(AvatarCancelButton).ClickOn();
        }

        public bool IsAvatarPopUpPresent()
        {
            return Wait.IsElementPresent(AvatarCloseIcon, 5);
        }
    }
}

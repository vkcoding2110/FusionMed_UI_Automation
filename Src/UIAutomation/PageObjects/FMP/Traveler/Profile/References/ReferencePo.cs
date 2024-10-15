using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.TravelerProfile.ProfileDashboard;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.References
{
    internal class ReferencePo : FmpBasePo
    {
        public ReferencePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FirstNameTextBox = By.CssSelector("input#first-name");
        private readonly By LastNameTextBox = By.CssSelector("input#last-name");
        private readonly By TitleTextBox = By.CssSelector("input#title");
        private readonly By WorkTogetherPlaceDropDown = By.CssSelector("select#where-work-together");
        private readonly By RelationshipDropDown = By.CssSelector("select#relationship");
        private readonly By DirectPhoneNumberTextBox = By.CssSelector("input#direct-phone-number");
        private readonly By EmailTextBox = By.CssSelector("input#reference-email");
        private readonly By AddReferenceButton = By.XPath("//button[contains(@class,'ReferenceEditButton')]//span[text()='Add Reference']");
        private readonly By SubmitButton = By.XPath("//button[contains(@class,'ReferenceEditButton')]//span[text()='Submit']");
        private readonly By CancelButton = By.XPath("//button[contains(@class,'ReferenceEditButton')]//span[text()='cancel']");
        private readonly By CloseIcon = By.CssSelector("div [class*='MuiDialog'] button[class*='CloseIconWrapper']");
        private readonly By DeleteReferenceButton = By.CssSelector("button[class*='DeleteButtonStyled'] span");
        private readonly By DeleteConfirmationButton = By.CssSelector("button[class*='DeleteConfirmationButton'] span");
        private readonly By AddReferenceHeaderText = By.XPath("//h5[contains(@class,'EditHeaderText')]");
        
        // Validation message 
        private readonly By FirstNameFieldValidationMessage = By.CssSelector("p#first-name-helper-text");
        private readonly By LastNameFieldValidationMessage = By.CssSelector("p#last-name-helper-text");
        private readonly By TitleFieldValidationMessage = By.CssSelector("p#title-helper-text");
        private readonly By WorkTogetherFieldValidationMessage = By.XPath("//select[@id='where-work-together']/parent::div/following-sibling::p");
        private readonly By RelationshipFieldValidationMessage = By.XPath("//select[@id='relationship']/parent::div/following-sibling::p");
        private readonly By ContactInformationFieldsValidationMessage = By.CssSelector("div[class*='ContactWrapper'] div.error");

        public void WaitTillAddReferencePopupHeaderTextDisplay() 
        {
            Wait.WaitUntilTextRefreshed(AddReferenceHeaderText);
        }

        public void EnterReferenceData(Reference addReference)
        {
            WaitTillAddReferencePopupHeaderTextDisplay();
            EnterFirstName(addReference.FirstName);
            EnterLastName(addReference.LastName);
            EnterTitle(addReference.Title);
            SelectWorkTogetherPlaceDropDown(1);
            SelectRelationshipDropDown(addReference.Relationship);
            EnterPhoneNumber(addReference.PhoneNumber);
            EnterEmail(addReference.Email);
            if (Wait.IsElementPresent(AddReferenceButton, 5))
            {
                Wait.UntilElementClickable(AddReferenceButton).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(SubmitButton).ClickOn();
                Wait.UntilElementInVisible(SubmitButton);
            }
        }

        public Reference GetReferenceData()
        {
            var firstname = Wait.UntilElementVisible(FirstNameTextBox).GetAttribute("value");
            var lastname = Wait.UntilElementVisible(LastNameTextBox).GetAttribute("value");
            var title = Wait.UntilElementVisible(TitleTextBox).GetAttribute("value");
            var workTogether = Wait.UntilElementVisible(WorkTogetherPlaceDropDown).SelectDropdownGetSelectedValue();
            var relationship = Wait.UntilElementVisible(RelationshipDropDown).SelectDropdownGetSelectedValue();
            var phoneNumber = Wait.UntilElementVisible(DirectPhoneNumberTextBox).GetAttribute("value");
            var email = Wait.UntilElementVisible(EmailTextBox).GetAttribute("value");

            return new Reference
            {
                FirstName = firstname,
                LastName = lastname,
                Title = title,
                WorkTogether = workTogether,
                Relationship = relationship,
                PhoneNumber = phoneNumber,
                Email = email
            };
        }

        public void ClickOnAddReferenceButton()
        {
            Wait.UntilElementClickable(AddReferenceButton).ClickOn();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.UntilElementInVisible(CloseIcon);
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.UntilElementInVisible(CancelButton);
        }
        public bool IsAddReferencePopUpDisplayed()
        {
            return Wait.IsElementPresent(CloseIcon,5);
        }

        public void EnterFirstName(string firstname)
        {
            Wait.UntilElementVisible(FirstNameTextBox).EnterText(firstname, true);
        }
        public void EnterLastName(string lastname)
        {
            Wait.UntilElementVisible(LastNameTextBox).EnterText(lastname, true);
        }
        public void EnterTitle(string title)
        {
            Wait.UntilElementVisible(TitleTextBox).EnterText(title, true);
        }
        public void SelectWorkTogetherPlaceDropDown(int index)
        {
            Wait.UntilElementClickable(WorkTogetherPlaceDropDown).SelectDropdownValueByIndex(index);
        }
        public void ClearWorkTogetherDropDown()
        {
            Wait.UntilElementClickable(WorkTogetherPlaceDropDown).SelectDropdownValueByIndex(0);
        }
        public void SelectRelationshipDropDown(string relationship)
        {
            Wait.UntilElementClickable(RelationshipDropDown).SelectDropdownValueByText(relationship,Driver);
        }
        public void ClearRelationshipDropDown()
        {
            Wait.UntilElementClickable(RelationshipDropDown).SelectDropdownValueByIndex(0);
        }
        public void EnterPhoneNumber(string phoneNumber)
        {
            Wait.UntilElementVisible(DirectPhoneNumberTextBox).EnterText(phoneNumber, true);
        }
        public void EnterEmail(string email)
        {
            Wait.UntilElementVisible(EmailTextBox).EnterText(email, true);
        }
        public void ClickOnDeleteReferenceButton()
        {
            Wait.UntilElementClickable(DeleteReferenceButton).ClickOn();
            Wait.UntilElementVisible(DeleteConfirmationButton);
            Wait.UntilElementClickable(DeleteConfirmationButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void DeleteAllReferenceDetail()
        {
            var allElement = new ReferenceDetailPo(Driver).GetEditButtonCount();
            for (var i = 0; i < allElement; i++)
            {
                new ReferenceDetailPo(Driver).ClickOnEditButton();
                new ReferencePo(Driver).ClickOnDeleteReferenceButton();
            }
        }
        public string GetValidationMessageDisplayedForFirstNameField()
        {
            return Wait.UntilElementVisible(FirstNameFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForLastNameField()
        {
            return Wait.UntilElementVisible(LastNameFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForTitleField()
        {
            return Wait.UntilElementVisible(TitleFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForWorkTogetherField()
        {
            return Wait.UntilElementVisible(WorkTogetherFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForRelationshipField()
        {
            return Wait.UntilElementVisible(RelationshipFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForPhoneNumberField()
        {
            return Wait.UntilElementVisible(ContactInformationFieldsValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForEmailField()
        {
            return Wait.UntilElementVisible(ContactInformationFieldsValidationMessage).GetText();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing
{
    internal class ProfileSharingPo : FmpBasePo
    {
        public ProfileSharingPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By MyProfileSharingHeaderText = By.XPath("//div[contains(@class,'ProfileShareContentInner')]/h2");
        private readonly By MyProfileSharingCloseIcon = By.XPath("//button[contains(@aria-label,'close profile share')][@align='end']//span");
        private readonly By MyProfileSharingCancelButton = By.XPath("//span[text()='close']//parent::button[contains(@class,'ButtonStyled')]");
        private readonly By AddRecipientViaEmailButton = By.XPath("//button[contains(@class,'ButtonStyled')]/span[text()='Add Recipient via Email']");
        private readonly By ShareMyProfileHeaderText = By.XPath("//div[contains(@class,'ProfileShareContentInner')]/h2");
        private readonly By ShareMyProfileCloseIcon = By.CssSelector("button[class*='IconButtonStyled'] svg[class*='CloseIcon']");
        private readonly By ShareMyProfileCancelButton = By.XPath("//span[text()='cancel']//parent::button[contains(@class,'ButtonStyled')]");
        private readonly By ShareMyProfileBackButton = By.CssSelector("button[class*='IconButtonStyled'] svg[class*='ArrowBackIcon']");
        private readonly By ShareMyProfileButton = By.XPath("//button[contains(@class,'SubmitButton')]");
        private readonly By RecipientsEmailTextBox = By.XPath("//div[contains(@class,'MuiFilledInput')]/input");
        private readonly By ShareMyFullProfileCheckbox = By.XPath("//div[contains(@class,'AcknowledgementContainer')]//span[contains(@class,'CheckboxStyled')]");
        private readonly By AllowedEmailsText = By.XPath("//div[contains(@class,'ProfileShareItemWrapper')]/p[contains(@class,'ProfileShareItemLabel')]");
        private readonly By StopSharingLink = By.XPath("//div[contains(@class,'ProfileShareItemWrapper')]/p[contains(@class,'ProfileShareItemRemove')]");
        private readonly By StopSharingButton = By.XPath("//span[text()='Stop Sharing']/parent::button[contains(@class,'ButtonStyled')]");
        private readonly By StopSharingPopUpHeaderText = By.XPath("//div[contains(@class,'ConfirmationWrapper')]/h2");
        private readonly By StopSharingCancelButton = By.XPath("//button[contains(@class,'ButtonStyled')]/span[text()='cancel']");
        private readonly By StopSharingCloseIcon = By.CssSelector("button[class*='IconButtonStyled'] svg[class*='CloseIcon']");
        private readonly By RecipientsEmailValidationMessageText = By.XPath("//div[contains(@class,'ErrorMessageContainer')]/p");
        private readonly By AllowedEmailsList = By.XPath("//div[contains(@class,'ProfileShareItemWrapper')]/p[contains(@class,'ProfileShareItemLabel')]");
        private readonly By RequireRecipientsEmailValidationMessageText = By.XPath("//div[contains(@class,'MuiFilledInput')]/following-sibling::p[contains(@class,'Mui-required')]");
        private static By StopSharingText(string email) => By.XPath($"//p[text()='{email}']/following-sibling::p[contains(@class,'ProfileShareItemRemove')]");

        public string GetMyProfileSharingPopUpHeaderText()
        {
            return Wait.UntilElementVisible(MyProfileSharingHeaderText).GetText();
        }

        public void ClickOnMyProfileSharingCloseIcon()
        {
            if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
            {
                Driver.JavaScriptClickOn(MyProfileSharingCloseIcon);
            }
            else
            {
                Wait.UntilElementClickable(MyProfileSharingCloseIcon).ClickOn();
            }
        }

        public bool IsMyProfileSharingPopUpDisplayed()
        {
            return Wait.IsElementPresent(MyProfileSharingHeaderText, 5);
        }

        public void ClickOnMyProfileSharingCancelButton()
        {
            Wait.UntilElementClickable(MyProfileSharingCancelButton).ClickOn();
        }

        public void ClickOAddRecipientViaEmailButton()
        {
            Wait.UntilElementClickable(AddRecipientViaEmailButton).ClickOn();
        }

        public string GetShareMyProfileHeaderText()
        {
            return Wait.UntilElementVisible(ShareMyProfileHeaderText).GetText();
        }

        public void ClickOnShareMyProfileCloseIcon()
        {
            Wait.UntilElementClickable(ShareMyProfileCloseIcon).ClickOn();
        }

        public bool IsShareMyProfileDisplayed()
        {
            return Wait.IsElementPresent(ShareMyProfileHeaderText, 5);
        }

        public void ClickOnShareMyProfileCancelButton()
        {
            Wait.UntilElementClickable(ShareMyProfileCancelButton).ClickOn();
        }

        public void ClickOnShareMyProfileBackButton()
        {
            Wait.UntilElementClickable(ShareMyProfileBackButton).ClickOn();
        }

        public bool IsShareMyProfileButtonEnabled()
        {
            return Wait.IsElementEnabled(ShareMyProfileButton, 5);
        }
        public void ClickOnStopSharing(string email)
        {
            if (!Wait.IsElementPresent(StopSharingText(email))) return;
            Wait.UntilElementClickable(StopSharingText(email)).ClickOn();
            ClickOnStopSharingButton();
        }
        public void EnterRecipientsEmail(string email)
        {
            Wait.UntilElementClickable(RecipientsEmailTextBox).EnterText(email);
        }

        public void SelectShareMyFullProfileCheckbox()
        {
            Wait.UntilElementClickable(ShareMyFullProfileCheckbox).ClickOn();
        }

        public void ClickOnShareMyFullProfileButton()
        {
            Wait.UntilElementClickable(ShareMyProfileButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public string GetAllowedEmailsText()
        {
            return Wait.UntilElementVisible(AllowedEmailsText).GetText();
        }

        public void ClickOnStopSharingLink()
        {
            Wait.UntilElementClickable(StopSharingLink).ClickOn();
        }

        public void DeleteAllProfileSharingEmails()
        {
            if (!Wait.IsElementPresent(StopSharingLink, 3)) return;
            var allElement = Wait.UntilAllElementsLocated(StopSharingLink, 5).Where(e => e.Displayed).ToList().Count;
            for (var i = 0; i < allElement; i++)
            {
                Wait.UntilElementClickable(StopSharingLink).ClickOn();
                Wait.UntilElementClickable(StopSharingButton).ClickOn();
                Wait.UntilElementInVisible(StopSharingButton);
            }
        }

        public void ClickOnStopSharingButton()
        {
            Wait.UntilElementClickable(StopSharingButton).ClickOn();
            Wait.UntilElementInVisible(StopSharingButton);
        }

        public bool IsRecipientsEmailDisplayed()
        {
            return Wait.IsElementPresent(AllowedEmailsText, 5);
        }

        public void ClickStopSharingCancelButton()
        {
            Wait.UntilElementClickable(StopSharingCancelButton).ClickOn();
        }

        public bool IsStopSharingPopUpDisplayed()
        {
            return Wait.IsElementPresent(StopSharingPopUpHeaderText, 5);
        }

        public void ClickOnStopSharingCloseIcon()
        {
            Wait.UntilElementClickable(StopSharingCloseIcon).ClickOn();
        }

        public void ClickOnStopSharingBackButton()
        {
            Wait.UntilElementClickable(ShareMyProfileBackButton).ClickOn();
        }

        public string GetRecipientsEmailValidationMessageText()
        {
            return Wait.UntilElementVisible(RecipientsEmailValidationMessageText).GetText();
        }

        public IList<string> GetAllowedEmailsList()
        {
            return Wait.UntilAllElementsLocated(AllowedEmailsList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }

        public string GetRequireRecipientsEmailValidationMessageText()
        {
            return Wait.UntilElementVisible(RequireRecipientsEmailValidationMessageText).GetText();
        }

        public void AddShareMyProfileDetails(string email)
        {
            EnterRecipientsEmail(email);
            SelectShareMyFullProfileCheckbox();
            ClickOnShareMyFullProfileButton();
        }

        public void ClickOnCloseIconAndClickOnLogout()
        {
            ClickOnMyProfileSharingCloseIcon();
            new HeaderPo(Driver).ClickOnProfileIcon();
            new ProfileMenuPo(Driver).ClickOnLogOutButton();
            WaitUntilFmpPageLoadingIndicatorInvisible();
            Driver.RefreshPage();
        }
    }
}

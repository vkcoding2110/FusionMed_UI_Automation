using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing.NativeApp
{
    internal class ProfileSharingPo : FmpBasePo
    {
        public ProfileSharingPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By AddRecipientViaEmailButton = By.XPath("//android.widget.TextView[@text='Add Recipient via Email']/parent::android.widget.Button");
        private readonly By ShareMyProfileHeaderText = By.XPath("//android.widget.TextView[contains(@text,'The recipient will get')]/preceding-sibling::android.widget.TextView");
        private readonly By ShareMyProfileCloseIcon = By.XPath("//android.widget.ScrollView/parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.widget.Button");
        private readonly By ShareMyProfileCancelButton = By.XPath("//android.widget.TextView[@text='close']/parent::android.widget.Button");
        private readonly By MyProfileSharingHeaderText = By.XPath("//android.widget.TextView[contains(@text,'Allow or turn off access')]/preceding-sibling::android.widget.TextView");
        private readonly By ShareMyProfileBackButton = By.XPath("//android.widget.ScrollView/preceding-sibling::android.widget.Button");
        private readonly By StopSharingLink = By.XPath("//android.widget.TextView[@text='Stop sharing']");
        private readonly By StopSharingButton = By.XPath("//android.widget.TextView[@text='Stop Sharing']/parent::android.widget.Button");
        private readonly By ShareMyProfileButton = By.XPath("//android.widget.TextView[@text='Share My Profile']/parent::android.widget.Button");
        private readonly By RecipientsEmailTextBox = By.XPath("//android.widget.TextView[contains(@text,'Email')]/parent::android.view.ViewGroup/preceding-sibling::android.widget.EditText");
        private readonly By ShareMyFullProfileCheckbox = By.XPath("//android.widget.TextView[contains(@text,'share my full profile')]/preceding-sibling::android.widget.CheckBox");
        private readonly By AllowedEmailsText = By.XPath("//android.widget.TextView[@text= 'Allowed Emails']/parent::android.view.ViewGroup/android.view.ViewGroup[1]/android.widget.TextView[1]");

        private readonly By StopSharingCancelButton = By.XPath("//android.widget.TextView[@text='cancel']/parent::android.widget.Button");
        private readonly By StopSharingCloseIcon = By.XPath("//android.widget.ScrollView/parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.widget.Button");
        private readonly By StopSharingPopUpHeaderText = By.XPath("//android.widget.TextView[contains(@text,'Are you sure?')]");
        private readonly By StopSharingBackButton = By.XPath("//android.widget.ScrollView/preceding-sibling::android.widget.Button");
        private readonly By RequiredRecipientsEmailValidationMessageText = By.XPath("//android.widget.TextView[contains(@text,'This field is')]");
        private readonly By RecipientsEmailValidationMessageText = By.XPath("//android.widget.TextView[contains(@text,'The recipient will get a link')]/parent::android.view.ViewGroup/android.view.ViewGroup[1]/android.widget.TextView");
        private readonly By AllowedEmailsList = By.XPath("//android.widget.TextView[@text= 'Allowed Emails']/parent::android.view.ViewGroup/android.view.ViewGroup/android.widget.TextView[1]");

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
            Wait.UntilElementInVisible(ShareMyProfileCloseIcon, 3);
        }

        public bool IsShareMyProfileDisplayed()
        {
            return Wait.IsElementPresent(ShareMyProfileHeaderText, 5);
        }
        public void ClickOnShareMyProfileCancelButton()
        {
            Wait.UntilElementClickable(ShareMyProfileCancelButton).ClickOn();
        }
        public bool IsMyProfileSharingPopUpDisplayed()
        {
            return Wait.IsElementPresent(MyProfileSharingHeaderText, 5);
        }
        public void ClickOnShareMyProfileBackButton()
        {
            Wait.UntilElementClickable(ShareMyProfileBackButton).ClickOn();
        }

        public string GetMyProfileSharingPopUpHeaderText()
        {
            return Wait.UntilElementVisible(MyProfileSharingHeaderText).GetText();
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

        public bool IsStopSharingLinkPresent()
        {
            return Wait.IsElementPresent(StopSharingLink);
        }
        public void AddShareMyProfileDetails(string email)
        {
            EnterRecipientsEmail(email);
            SelectShareMyFullProfileCheckbox();
            ClickOnShareMyFullProfileButton();
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
        }
        public string GetAllowedEmailsText()
        {
            return Wait.UntilElementVisible(AllowedEmailsText).GetText();
        }
        public void ClickOnStopSharingLink()
        {
            Wait.UntilElementClickable(StopSharingLink).ClickOn();
        }
        public void ClickStopSharingCancelButton()
        {
            Wait.UntilElementClickable(StopSharingCancelButton).ClickOn();
            Wait.UntilElementInVisible(StopSharingCancelButton, 3);
        }
        public bool IsStopSharingPopUpDisplayed()
        {
            return Wait.IsElementPresent(StopSharingPopUpHeaderText, 5);
        }
        public void ClickOnStopSharingBackButton()
        {
            Wait.UntilElementClickable(StopSharingBackButton).ClickOn();
        }
        public void ClickOnStopSharingCloseIcon()
        {
            Wait.UntilElementClickable(StopSharingCloseIcon).ClickOn();
            Wait.UntilElementInVisible(StopSharingCloseIcon, 3);
        }
        public void ClickOnStopSharingButton()
        {
            Wait.UntilElementClickable(StopSharingButton).ClickOn();
            Wait.UntilElementInVisible(StopSharingButton);
        }

        public string GetRequireRecipientsEmailValidationMessageText()
        {
            return Wait.UntilElementVisible(RequiredRecipientsEmailValidationMessageText).GetText();
        }
        public string GetRecipientsEmailValidationMessageText()
        {
            return Wait.UntilElementVisible(RecipientsEmailValidationMessageText).GetText();
        }
        public IList<string> GetAllowedEmailsList()
        {
            return Wait.UntilAllElementsLocated(AllowedEmailsList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }
    }
}

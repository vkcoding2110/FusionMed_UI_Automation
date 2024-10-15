using System;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Jobs.JobDetails;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs
{
    internal class JobsDetailsPo : FmpBasePo
    {
        public JobsDetailsPo(IWebDriver driver) : base(driver)
        {
        }
        //Job Details
        private readonly By JobTitle = By.XPath("//div[contains(@class,'JobTitle')]//h1");
        private readonly By JobLocation = By.XPath("//div[contains(@class,'JobLocation-sc')]");
        private readonly By BackToResultsButton = By.XPath("//div[contains(@class,'BackToResultsRow')]//span[text()='Back to results']");
        private readonly By ApplyForThisJobButton = By.XPath("//div[contains(@class,'JobDetailsCardRightPanel')]/div//button[contains(@class,'JobDetailsQuickApplyButton')]");
        private readonly By AgencyLink = By.XPath("//div[contains(@class,'AgencyLogoContainer')]//img");

        //SocialMedia Icon
        private readonly By FacebookIcon = By.XPath("//div[contains(@class,'hareButton')]/a[@aria-label='Share on Facebook']");
        private readonly By TwitterIcon = By.XPath("//div[contains(@class,'hareButton')]/a[@aria-label='Share on Twitter']");
        private readonly By LinkedInIcon = By.XPath("//div[contains(@class,'hareButton')]/a[@aria-label='Share on LinkedIn']");

        private readonly By TellMeButton = By.CssSelector("div[class*='JobFacilityContact'] button");
        private readonly By DisclosureText = By.CssSelector("span[class*='DisclosureText']");
        private readonly By DisclosureOkayButton = By.CssSelector("button[class*='OkayButton']");
        private readonly By CancelButton = By.XPath("//button[contains(@class,'CancelButton')]");

        //Share Job Detail
        private readonly By JobId = By.XPath("//span[contains(@class,'JobDetailsJobId')]");
        private readonly By FacilityInfo = By.CssSelector("div[class*='JobFacilityContact'] span");
        private readonly By FacilityType = By.CssSelector("div[class*='DetailsRow'] div span");
        private readonly By NumberOfBeds = By.XPath("//div[contains(@class,'DetailsRow')]//h6[text()='Number Of Beds']//following-sibling::span");
        private readonly By Shift = By.XPath("//div[contains(@class,'gridstyles')]//h6[text()='Shift']//following-sibling::span");
        private readonly By AssignmentLength = By.XPath("//div[contains(@class,'gridstyles')]//h6[text()='Assignment Length']//following-sibling::span");
        private readonly By JobQuality = By.XPath("//div[contains(@class,'gridstyles')]//h6[text()='Job Quantity']//following-sibling::span");
        private readonly By Type = By.XPath("//div[contains(@class,'gridstyles')]//h6[text()='Type']//following-sibling::span");

        //Email Job
        private readonly By EmailJobButton = By.XPath("//button[contains(@class,'EmailButton')]");
        private readonly By EmailJobButtonDeviceButton = By.XPath("//div[contains(@class,'CallToActionWrapper')]//button[contains(@class,'EmailButton')]");
        private readonly By RecipientsEmailTextBox = By.XPath("//div[contains(@class,'MuiFilledInput-root')]//input[@id='share_email_list']");
        private readonly By IncludeMessageTextArea = By.XPath("//div[contains(@class,'MuiFilledInput')]//textarea[@id='share_email_message']");
        private readonly By SendEmailButton = By.XPath("//button[contains(@class,'SendEmailButton')]");
        private readonly By SharedJobHeaderText = By.XPath("//span[contains(@class,'SendEmailSuccessTextHeader')]");
        private readonly By JobName = By.CssSelector("p[class*='SendEmailSuccessText'] span");
        private readonly By SharedJobMessageText = By.XPath("//p[contains(@class,'SendEmailSuccessText')]");
        private readonly By EmailJobCancelButton = By.XPath("//div[contains(@class,'CancelTouch')]/span");

        //Successfully Shared Job
        private readonly By SuccessfullySharedJobCloseIcon = By.XPath("//div[contains(@class,'CloseTouch')]");
        private readonly By SuccessfullySharedJobCloseButton = By.CssSelector("button[class*='CloseButton']");

        //Closed Job
        private readonly By ClosedJobMessage = By.XPath("//div[contains(@class,'JobFilledWarningWrapper')]/div/following-sibling::div");

        //Device Elements
        private readonly By BackToResultsDeviceButton = By.XPath("//button[contains(@class,'BackIconWrapper')]");
        private readonly By ApplyForThisJobDeviceButton = By.XPath("//div[contains(@class,'JobDetailsQuickApplyMobile')]//button");
        private readonly By ShareIconDevice = By.XPath("//button[contains(@class,'SocialShareIconWrapper')]");

        //Sort By Start Date
        private readonly By StartsDate = By.XPath("//div[contains(@class,'gridstyles')]/h6[text()='Starts']/following-sibling::span");

        //Agency Offer Card
        private readonly By TotalAgencyCards = By.XPath("//div[contains(@class,'slick-slide')]//div[contains(@class,'CarouselCardContainer')]");
        private readonly By TotalOffers = By.CssSelector("div[class*='OfferCountContainer'] h5");
        private static By AgencyCardPayAmount(int index) => By.XPath($"//div[@data-index='{index - 1}']//span/div[contains(@class,'CardPayPackage')]/p");
        private readonly By JobPayAmount = By.XPath("//span[@class='money']");
        private static By AgencyNameFromCard(int index) => By.XPath($"//div[@data-index='{index - 1}']//span//div[contains(@class,'agency-logo')]//following-sibling::div/h4");
        private static By AgencyOfferCard(int index) => By.XPath($"//div[@data-index='{index - 1}']");
        private static By SelectedAgencyOfferText(int offerNumber) => By.XPath($"//div[contains(@class,'CardOffer')][text()='Option {offerNumber} Selected']");
        private readonly By AgencyCardPreviousButton = By.CssSelector("div[class*='PayPackageWrapper'] button[class*='slick-arrow slick-prev']");
        private static By AgencyCardDisabledText(int index) => By.XPath($"//div[@data-index='{index - 1}']//div[contains(@class,'DisabledSection')]//p[contains(@class,'DisabledText')]");

        public string GetAgencyName()
        {
            return Wait.UntilAllElementsLocated(AgencyLink).First(x => x.IsDisplayed()).GetAttribute("alt");
        }

        public void ClickOnAgencyLink()
        {
            Wait.UntilAllElementsLocated(AgencyLink).First(x => x.IsDisplayed()).ClickOn();
        }
        public string GetJobTitle()
        {
            Wait.UntilElementVisible(JobTitle);
            return Wait.UntilElementVisible(JobTitle).GetText();
        }
        public bool IsJobTitlePresent()
        {
            return Wait.IsElementPresent(JobTitle, 3);
        }
        public string GetJobLocation()
        {
            return Wait.UntilAllElementsLocated(JobLocation).First(x => x.IsDisplayed()).GetText();
        }

        public void ClickOnBackToResultsButton()
        {
            Wait.UntilElementClickable(BaseTest.PlatformName != PlatformName.Web ? BackToResultsDeviceButton : BackToResultsButton).ClickOn();
        }
        public void ClickOnApplyForThisJobButton()
        {
            Wait.UntilElementClickable(BaseTest.PlatformName != PlatformName.Web ? ApplyForThisJobDeviceButton : ApplyForThisJobButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public string GetFacebookIconHref()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementClickable(ShareIconDevice).ClickOn();
            }
            return Wait.UntilElementVisible(FacebookIcon).GetAttribute("href");
        }

        public string GetTwitterIconHref()
        {
            return Wait.UntilElementVisible(TwitterIcon).GetAttribute("href");
        }

        public string GetLinkedInIconHref()
        {
            return Wait.UntilElementVisible(LinkedInIcon).GetAttribute("href");
        }

        public void ClickOnTellMeButton()
        {
            Wait.UntilElementClickable(TellMeButton).ClickOn();
        }
        public string GetDisclosureText()
        {
            return Wait.UntilElementVisible(DisclosureText).GetText();
        }

        public void ClickOnDisclosureOkayButton()
        {
            Wait.UntilElementClickable(DisclosureOkayButton).ClickOn();
            Wait.UntilElementInVisible(DisclosureOkayButton);
        }

        public bool IsDisclosureTextDisplayed()
        {
            return Wait.IsElementPresent(DisclosureText);
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(CancelButton, 1);
        }

        public bool IsCancelButtonDisplayed()
        {
            return Wait.IsElementPresent(CancelButton, 5);
        }

        public Job GetJobDetailsFromDetailPage()
        {
            var jobDetails = new Job
            {
                Id = Wait.UntilElementVisible(JobId).GetText(),
                Facility = Wait.UntilElementVisible(FacilityInfo).GetText(),
                FacilityType = Wait.UntilElementVisible(FacilityType).GetText(),
                NumberOfBeds = Wait.UntilElementVisible(NumberOfBeds).GetText(),
                Shift = Wait.UntilElementVisible(Shift).GetText(),
                AssignmentLength = Wait.UntilElementVisible(AssignmentLength).GetText(),
                Quality = Wait.UntilElementVisible(JobQuality).GetText(),
                Type = Wait.UntilElementVisible(Type).GetText()
            };
            return jobDetails;
        }

        public void ClickOnEmailJobButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementClickable(ShareIconDevice).ClickOn();
                Wait.UntilElementClickable(EmailJobButtonDeviceButton).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(EmailJobButton).ClickOn();
            }
        }

        public void EnterEmailJobDetailsPopUp(string email, string message)
        {
            Wait.UntilElementClickable(RecipientsEmailTextBox);
            Wait.UntilElementVisible(RecipientsEmailTextBox).EnterText(email);
            Wait.UntilElementVisible(IncludeMessageTextArea).EnterText(message);
            Wait.UntilElementClickable(SendEmailButton).ClickOn();
        }

        public string GetSharedThisJobDetailsHeaderText()
        {
            Wait.UntilElementVisible(SharedJobHeaderText);
            return Wait.UntilElementVisible(SharedJobHeaderText).GetText();
        }

        public string GetJobNameFromSuccessPopUp()
        {
            return Wait.UntilElementVisible(JobName).GetText();
        }

        public string GetSharedThisJobDetailsMessageText()
        {
            return Wait.UntilElementVisible(SharedJobMessageText).GetText();
        }

        public void ClickOnEmailJobCancelButton()
        {
            Wait.UntilElementVisible(EmailJobCancelButton);
            Wait.UntilElementClickable(EmailJobCancelButton).ClickOn();
        }

        public bool IsSendEmailButtonDisplayed()
        {
            return Wait.IsElementPresent(SendEmailButton);
        }

        public void ClickOnSuccessfullySharedJobCloseIcon()
        {
            Wait.UntilElementVisible(SuccessfullySharedJobCloseIcon);
            Wait.UntilElementClickable(SuccessfullySharedJobCloseIcon).ClickOn();
            Wait.UntilElementInVisible(SuccessfullySharedJobCloseIcon);
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnSuccessfullySharedJobCloseButton()
        {
            Wait.UntilElementClickable(SuccessfullySharedJobCloseButton).ClickOn();
            Wait.UntilElementInVisible(SuccessfullySharedJobCloseButton);
        }

        public bool IsSuccessfullySharedJobPopUpHeaderTextDisplayed()
        {
            return Wait.IsElementPresent(SharedJobHeaderText);
        }

        public bool IsApplyForThisJobButtonEnabled()
        {
            return Wait.IsElementEnabled(ApplyForThisJobButton);
        }

        public string GetFilledJobMessage()
        {
            return Wait.UntilElementVisible(ClosedJobMessage).GetText();
        }

        public string GetStartsDate()
        {
            return Wait.UntilElementVisible(StartsDate).GetText();
        }

        public int GetJobQuantityFromJobDetails()
        {
            return Convert.ToInt32(Wait.UntilElementVisible(JobQuality).GetText());
        }

        //Agency Offer Card
        public int GetAgencyCardCount()
        {
            return Wait.UntilAllElementsLocated(TotalAgencyCards).Where(e => e.Displayed).ToList().Count;
        }

        public string GetOffersCount()
        {
            return Wait.UntilElementVisible(TotalOffers).GetText();
        }

        public double GetPayAmountFromAgencyCard(int cardIndex)
        {
            var pay = Wait.UntilElementVisible(AgencyCardPayAmount(cardIndex)).GetText().Replace("$", "").Replace(" weekly gross", "");
            var d = Convert.ToDouble(pay);
            return Math.Round(d);
        }

        public string GetJobPayAmount()
        {
            return Wait.UntilElementVisible(JobPayAmount).GetText().Replace("\r\n", "").Replace("*/", " ");
        }

        public string GetAgencyNameFromAgencyCard(int index)
        {
            return Wait.UntilElementVisible(AgencyNameFromCard(index)).GetText();
        }

        public void ClickOnAgencyOfferCard(int index)
        {
            Wait.UntilElementClickable(AgencyOfferCard(index)).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
            Wait.HardWait(2000);
        }

        public bool IsSelectedAgencyOfferCardTextPresent(int offerNumber)
        {
            return Wait.IsElementPresent(SelectedAgencyOfferText(offerNumber));
        }

        public void ClickOnPreviousAgencyCardButton()
        {
            Wait.UntilAllElementsLocated(AgencyCardPreviousButton).First(x => x.IsDisplayed()).ClickOn();
            Wait.HardWait(3000);
        }

        public bool IsAgencyCardDisableTextPresent(int index)
        {
            return Wait.IsElementPresent(AgencyCardDisabledText(index), 5);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Components;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin
{
    internal class ReviewsDashboardPo : FmpBasePo
    {
        public ReviewsDashboardPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ReviewsHeaderText = By.XPath("//div[contains(@class,'SubheaderWrapper')]/h2");
        private readonly By MyOverallRatingsDescription = By.XPath("//div[contains(@class,'RatingsSection')]/p");
        private readonly By NoReviewRecruiterText = By.XPath("//div[contains(@class,'AdminSubpageWrapper')]//p");
        private readonly By MyOverallRatingsHeaderText = By.XPath("//div[contains(@class,'RatingsSection')]/div[contains(@class,'TopRow')]/h1");
        private readonly By HowLikelyReviewersText = By.XPath("//div[contains(@class,'HowLikelySection')]");
        private readonly By ReviewCount = By.XPath("//span[contains(@class,'ReviewCount')]");
        private readonly By ReviewerCount = By.XPath("//div[contains(@class,'ReviewAccordion')]");

        //ReviewsCategory
        private readonly By AbilityToMeetYourNeedsText = By.XPath("//div[contains(@class,'CategoryHolder')]/div[text()='Ability to meet your needs']");
        private readonly By PersonalityFitText = By.XPath("//div[contains(@class,'CategoryHolder')]/div[text()='Personality Fit']");
        private readonly By CommunicationText = By.XPath("//div[contains(@class,'CategoryHolder')]/div[text()='Communication']");
        private readonly By ThoughtfulnessText = By.XPath("//div[contains(@class,'CategoryHolder')]/div[text()='Thoughtfulness']");
        private readonly By KnowledgeText = By.XPath("//div[contains(@class,'CategoryHolder')]/div[text()='Knowledge']");
        private readonly By EfficiencyText = By.XPath("//div[contains(@class,'CategoryHolder')]/div[text()='Efficiency']");

        private readonly By StarAreaText = By.XPath("//div[contains(@class,'StarArea')]");

        //Filter
        private static By RecruiterRatingByNthRowNthColumn(int rowIndex, int columnIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[@role='cell'][{columnIndex}]");
        private static By FilterToggleIcon(string filterText) => By.XPath($"//div[contains(@class,'thead')]//div[text()='{filterText}']//following-sibling::div/button[contains(@class,'IconButtonStyled')]");
        private readonly By StartDateButton = By.XPath("//span[text()='to']/preceding-sibling::div//button");
        private readonly By EndDateButton = By.XPath("//span[text()='to']/following-sibling::div//button");
        private static By RecruiterByNthColumn(int columnIndex) => By.XPath($"//div[@class='tbody']//div[@role='row']/div[@role='cell'][{columnIndex}]");
        private readonly By TextContainsTextBox = By.XPath("//input[@placeholder='Text Contains']");
        private readonly By ReviewerRole = By.XPath("//select[contains(@class,'MuiNativeSelect-select')]");
        private static By ReviewerRoleOption(string text) => By.XPath($"//select[contains(@class,'MuiNativeSelect-select')]//*[text()='{text}']");

        private readonly By MinRating = By.XPath("//p[text()='to']/preceding-sibling::div/div/input");
        private readonly By MaxRating = By.XPath("//p[text()='to']/following-sibling::div/div/input");

        //Recruiter reviews tab
        private static By ReviewsSubTabs(string item) => By.XPath($"//div[contains(@class,'SubTabsContainer')]//button//span[text()='{item}']");
        private readonly By RecruiterReviewsHeaderText = By.XPath("//div[contains(@class,'ReviewsTabsContainer')]//button/span[text()='Recruiter Reviews']");

        //Review Response
        private static By RecruiterExpandCollapseIconByNthRowNthColumn(int rowIndex, int columnIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[@role='cell'][{columnIndex}]//div[@id='panel1a-header']");

        private readonly By WriteAResponseButton = By.XPath("//span[contains(text(),'Write a Response')]/parent::button");
        private readonly By EditResponseButton = By.XPath("//span[contains(text(),'Edit Response')]/parent::button");
        private readonly By ResponseText = By.XPath("//div[contains(@class,'ReviewResponse')]/p");
        private readonly By DeleteResponseButton = By.XPath("//span[contains(text(),'Delete Response')]/parent::button");
        private readonly By AreYouSurePopUpDeleteResponseButton = By.XPath("//div[contains(@class,'ButtonContainer')]//button//span[text()='Delete Response']");

        //Add/Delete Response/Dispute popup
        private readonly By ResponseReviewHeaderText = By.CssSelector("div[class*='ReviewActionContent'] h3");
        private readonly By ResponseTextArea = By.CssSelector("textarea[name = 'responseText']");
        private readonly By PublishResponseButton = By.CssSelector("button[class*='ReviewActionButton']");
        private readonly By ResponseRequiredValidationText = By.CssSelector("div[class*='ReviewActionWrapper'] p[class*='MuiFormHelperText']");
        private readonly By RespondReviewCloseButton = By.CssSelector("button[class*='CloseIconWrapper']");
        private readonly By RespondReviewCancelButton = By.XPath("//span[text()='cancel']/parent::button[contains(@class,'ReviewActionButton')]");

        //Add/Cancel Dispute Request
        private readonly By DisputeReviewButton = By.XPath("//span[contains(text(),'Dispute Review')]/parent::button");
        private readonly By DisputeReviewPopupHeader = By.CssSelector("div[class*='ReviewActionContent'] h3");
        private readonly By DisputeReviewPopupReviewContentText = By.CssSelector("div[class*='ReviewActionContent'] p");
        private readonly By DisputeReviewPopupRequestRejectionButton = By.XPath("//span[text()='Request Rejection']/parent::button");
        private readonly By DisputeReasoneTextArea = By.CssSelector("textarea[name = 'reasonForChange']");
        private readonly By DisputeReasonText = By.XPath("//div[contains(@class,'ReviewDetailCtaContainer')]//div[contains(@class,'ReviewReason')]");
        private readonly By RejectRequestText = By.XPath("//div[contains(@class,'ReviewDetailsCtaTitle')]/h5");
        private readonly By CancelDisputeButton = By.XPath("//span[text()='Cancel Dispute']/parent::button");
        private readonly By CancelDisputePopup = By.XPath("//h3[contains(text(),'Sure?')]/parent::div[contains(@class,'ReviewActionContent')]");
        private readonly By CancelDisputePopupContentText = By.CssSelector("div[class*='ReviewActionContent']");
        private readonly By KeepDisputeButton = By.XPath("//span[text()='keep dispute']/parent::button[contains(@class,'ReviewActionButton')]");
        private readonly By CancelDisputePopupButton = By.XPath("//span[text()='Cancel Dispute']/parent::button[contains(@class,'ReviewActionButton')]");

        //Reviewer Details
        private static By ReviewerDataRow(int rowIndex, int columnIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[@role='cell'][{columnIndex}]/div");
        private readonly By ReviewerName = By.XPath("//div[contains(@class,'ReviewDetailsUserDetails')]/h4");
        private readonly By ReviewUserDetails = By.XPath("//div[contains(@class,'ReviewDetailsUserDetails')]/h4/following-sibling::div[1]");
        private readonly By ReviewDate = By.XPath("//div[contains(@class,'ReviewDetailsReviewedDate')]/span");
        private readonly By ReviewsDescription = By.XPath("//div[contains(@class,'ReviewDetailsUserReview')]/p");
        private static By MeetYourNeedsRatingLabel(int rowIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[contains(text(),'meet your needs')]/span");
        private static By CommunicationRatingLabel(int rowIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[contains(text(),'Communication')]/span");
        private static By KnowledgeRatingLabel(int rowIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[contains(text(),'Knowledge')]/span");
        private static By ProfessionalRelationshipRatingLabel(int rowIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[contains(text(),'Personality ')]/span");
        private static By ThoughtfulnessLabel(int rowIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[contains(text(),'Thoughtfulness')]/span");
        private static By EfficiencyRatingLabel(int rowIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[contains(text(),'Efficiency')]/span");
        private static By OverallRatingLabel(int rowIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//strong[contains(text(),'Overall Rating')]/following-sibling::span");
        private readonly By ClearFiltersButton = By.XPath("//div[contains(@class,'ActionRowWrapper')]//span[text()='Clear Filters']/parent::button");
        //SystemAdmin RejectReview
        private readonly By RejectReviewsButton = By.XPath("//span[contains(text(),'Reject Review')]/parent::button");
        private readonly By RejectReviewPopupReviewContentText = By.CssSelector("div[class*='ReviewActionContent'] p");
        private readonly By RejectReviewPopupRejectAndUnPublishReviewButton = By.XPath("//span[text()='Reject & Unpublish Review']/parent::button");
        private readonly By ApproveReviewsButton = By.XPath("//span[contains(text(),'Approve Review')]/parent::button");
        private static By RecruiterReviewsByNthColumn(int rowIndex, int columnIndex) => By.XPath($"//div[@class='tbody']/div[contains(@class,'ReviewAccordion')][{rowIndex}]//div[@role='row']/div[@role='cell'][{columnIndex}]");
        private readonly By ApproveReviewPopupAndPublishReviewButton = By.XPath("//span[text()='Approve & Publish Review']/parent::button");
        private readonly By RejectReviewDate = By.CssSelector("div[class*='ReviewDetailsCtaDate']");


        public string GetReviewsHeaderText()
        {
            return Wait.UntilElementVisible(ReviewsHeaderText).GetText();
        }

        public string GetNoReviewRecruiterText()
        {
            return Wait.UntilElementVisible(NoReviewRecruiterText).GetText();
        }

        public string GetMyOverallRatingsHeaderText()
        {
            return Wait.UntilElementVisible(MyOverallRatingsHeaderText).GetText();
        }

        public bool IsHowLikelyReviewersTextPresent()
        {
            return Wait.IsElementPresent(HowLikelyReviewersText, 5);
        }

        public string GetReviewCountText()
        {
            return Wait.UntilElementVisible(ReviewCount).GetText().Substring(1, 2);
        }

        public int GetReviewerCount()
        {
            return Wait.UntilAllElementsLocated(ReviewerCount).Where(e => e.Displayed).ToList().Count;
        }

        public bool IsAbilityToMeetYourNeedsTextPresent()
        {
            return Wait.IsElementPresent(AbilityToMeetYourNeedsText, 3);
        }

        public bool IsPersonalityFitTextPresent()
        {
            return Wait.IsElementPresent(PersonalityFitText, 3);
        }

        public bool IsCommunicationTextPresent()
        {
            return Wait.IsElementPresent(CommunicationText, 3);
        }

        public bool IsThoughtfulnessTextPresent()
        {
            return Wait.IsElementPresent(ThoughtfulnessText, 3);
        }

        public bool IsKnowledgeTextPresent()
        {
            return Wait.IsElementPresent(KnowledgeText, 3);
        }

        public bool IsEfficiencyTextPresent()
        {
            return Wait.IsElementPresent(EfficiencyText, 3);
        }

        public string GetMyOverallRatingsDescriptionText()
        {
            return Wait.UntilElementVisible(MyOverallRatingsDescription).GetText();
        }

        public string GetGetStarAreaText()
        {
            return Wait.UntilElementVisible(StarAreaText).GetText().Replace("/5", "");
        }

        public string GetRatingRowText(int rowIndex, int columnIndex)
        {
            return Wait.UntilElementVisible(RecruiterRatingByNthRowNthColumn(rowIndex, columnIndex)).GetText();
        }

        public void ClickOnRecruiterFilterText(string filterText)
        {
            Wait.UntilElementClickable(FilterToggleIcon(filterText)).ClickOn();
            Wait.UntilElementVisible(FilterToggleIcon(filterText));
        }

        public void EnterStartAndEndDate(DateTime startDate, DateTime endDate)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectDate(startDate, StartDateButton, CalenderPicker.RangeCalenderYearView); 
            datePicker.SelectDate(endDate, EndDateButton, CalenderPicker.RangeCalenderYearView);
        }

        public IList<string> GetRecruitersByNthColumn(int columnIndex)
        {
            return Wait.UntilAllElementsLocated(RecruiterByNthColumn(columnIndex)).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public void SearchRecruitersByFilter(string recruiters, string label)
        {
            Wait.UntilElementVisible(TextContainsTextBox).EnterText(recruiters);
            Wait.UntilElementExists(FilterToggleIcon(label)).ClickOn();
        }

        public void SearchReviewerRoleFilter(string filterText, string recruiters)
        {
            Wait.UntilElementClickable(FilterToggleIcon(filterText)).ClickOn();
            Wait.UntilElementClickable(ReviewerRole).ClickOn();
            Wait.UntilElementClickable(ReviewerRoleOption(recruiters)).ClickOn();
        }

        public void EnterMinAndMaxRating(int min, int max, string label)
        {
            Wait.UntilElementVisible(MinRating).EnterText(min.ToString(), true);
            Wait.UntilElementVisible(MaxRating).EnterText(max.ToString(), true);
            Wait.UntilElementClickable(FilterToggleIcon(label)).ClickOn();
        }

        public void ClickOnReviewsSubTabs(string item)
        {
            Wait.UntilElementClickable(ReviewsSubTabs(item)).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public bool IsRecruiterReviewsPageOpened()
        {
            return Wait.IsElementPresent(RecruiterReviewsHeaderText, 5);
        }

        //Add/Cancel Dispute Request
        public void ClickOnDisputeReviewButton()
        {
            Driver.JavaScriptClickOn(DisputeReviewButton);
            WaitTillDisputeCancelPopupGetsOpen();
        }

        public void WaitTillDisputePopupGetsOpen()
        {
            Wait.WaitUntilTextRefreshed(DisputeReviewPopupHeader);
        }
        public bool IsDisputeReviewPopupOpened()
        {
            return Wait.IsElementPresent(DisputeReviewPopupRequestRejectionButton, 5);
        }

        public string GetDisputeReviewPopupContentText()
        {
            return Wait.UntilElementVisible(DisputeReviewPopupReviewContentText).GetText();
        }
        public void EnterDisputeReviewReason(string reasonText)
        {
            Wait.UntilElementClickable(DisputeReasoneTextArea).EnterText(reasonText);
        }
        public void ClickOnRequestRejectionButton()
        {
            Wait.UntilElementClickable(DisputeReviewPopupRequestRejectionButton).ClickOn();
        }
        public string GetDisputeReasonText()
        {
            return Wait.UntilElementVisible(DisputeReasonText).GetText().RemoveEndOfTheLineCharacter();
        }
        public string GetReviewRejectionText()
        {
            return Wait.UntilElementVisible(RejectRequestText).GetText();
        }
        public void ClickOnCancelDisputeButton()
        {
            Driver.JavaScriptClickOn(CancelDisputeButton);
            WaitTillDisputeCancelPopupGetsOpen();
        }
        public void WaitTillDisputeCancelPopupGetsOpen()
        {
            Wait.WaitUntilTextRefreshed(CancelDisputePopup);
        }
        public bool IsCancelDisputePopupOpened()
        {
            return Wait.IsElementPresent(CancelDisputePopup, 3);
        }
        public string GetCancelReviewPopupContentText()
        {
            return Wait.UntilElementVisible(CancelDisputePopupContentText).GetText().RemoveEndOfTheLineCharacter();
        }
        public void ClickOnKeepDisputeButton()
        {
            Wait.UntilElementClickable(KeepDisputeButton).ClickOn();
        }
        public bool IsCancelDisputeButtonDisplayed()
        {
            return Wait.IsElementPresent(CancelDisputeButton, 3);
        }
        public bool IsDisputeButtonDisplayed()
        {
            return Wait.IsElementPresent(DisputeReviewButton, 5);
        }
        public void ClickOnCancelDisputePopupButton()
        {
            Driver.JavaScriptClickOn(CancelDisputePopupButton);
            Wait.UntilElementInVisible(CancelDisputePopupButton, 5);
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        //Review Response
        public void ClickOnExpandCollapseIcon(int row, int column)
        {
            Wait.UntilElementClickable(RecruiterExpandCollapseIconByNthRowNthColumn(row, column)).ClickOn();
        }

        public void ClickOnWriteAResponseButton()
        {
            if (!Wait.IsElementEnabled(WriteAResponseButton))
            {
                ClickOnDeleteResponseButton();
                Wait.UntilElementClickable(AreYouSurePopUpDeleteResponseButton).ClickOn();
                WaitUntilFmpPageLoadingIndicatorInvisible();
            }

            Driver.JavaScriptClickOn(WriteAResponseButton);
        }

        public void ClickOnEditResponseButton()
        {
            Wait.UntilElementVisible(EditResponseButton);
            Wait.UntilElementClickable(EditResponseButton).ClickOn();
        }

        public void ScrollToNthRowAndColumn(int row, int column)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementClickable(RecruiterExpandCollapseIconByNthRowNthColumn(row, column)));
        }

        public string GetResponseText()
        {
            return Wait.UntilElementVisible(ResponseText).GetText();
        }

        public bool IsResponseTextPresent()
        {
            return Wait.IsElementPresent(ResponseText, 3);
        }

        public void ClickOnDeleteResponseButton()
        {
            Wait.UntilElementClickable(DeleteResponseButton).ClickOn();
        }

        public string GetRespondReviewHeaderText()
        {
            return Wait.UntilElementVisible(ResponseReviewHeaderText).GetText();
        }

        public void EnterResponseText(string response)
        {
            Wait.UntilElementClickable(ResponseTextArea).Clear();
            Wait.UntilElementClickable(ResponseTextArea).EnterText(response, true);
        }

        public void ClickOnPublishResponseButton()
        {
            Wait.UntilElementClickable(PublishResponseButton).ClickOn();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(RespondReviewCloseButton).ClickOn();
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(RespondReviewCancelButton).ClickOn();
        }

        public string GetResponseRequiredFieldValidationMessage()
        {
            return Wait.UntilElementVisible(ResponseRequiredValidationText).GetText();
        }

        public void WaitTillResponsePopupGetsOpen()
        {
            Wait.WaitUntilTextRefreshed(ResponseReviewHeaderText);
        }

        public bool IsRespondReviewPopupOpened()
        {
            return Wait.IsElementPresent(RespondReviewCloseButton, 7);
        }

        //Reviewer Details
        public string GetReviewerDataFromRow(int rowIndex, int columnIndex)
        {
            return Wait.UntilElementVisible(ReviewerDataRow(rowIndex, columnIndex)).GetText();
        }

        public void ClickOnRecruiterExpandCollapseIcon(int rowIndex, int columnIndex)
        {
            Wait.UntilElementClickable(RecruiterExpandCollapseIconByNthRowNthColumn(rowIndex, columnIndex)).ClickOn();
        }

        public string GetReviewerNameText()
        {
            return Wait.UntilElementVisible(ReviewerName).GetText();
        }

        public string GetReviewUserDetailsText()
        {
            return Wait.UntilElementVisible(ReviewUserDetails).GetText();
        }

        public string GetReviewsDescriptionText()
        {
            return Wait.UntilElementVisible(ReviewsDescription).GetText();
        }

        public DateTime GetReviewerDateText()
        {
            return DateTime.ParseExact(Wait.UntilElementVisible(ReviewDate).GetText().Replace("Reviewed", "").RemoveWhitespace(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }

        public TravelerRateAndReview GetAbilitiesRating(int rowIndex)
        {
            var meetYourNeeds = Wait.UntilElementVisible(MeetYourNeedsRatingLabel(rowIndex)).GetText();
            var knowledge = Wait.UntilElementVisible(KnowledgeRatingLabel(rowIndex)).GetText();
            var communication = Wait.UntilElementVisible(CommunicationRatingLabel(rowIndex)).GetText();
            var professionalRelationship = Wait.UntilElementVisible(ProfessionalRelationshipRatingLabel(rowIndex)).GetText();
            var support = Wait.UntilElementVisible(ThoughtfulnessLabel(rowIndex)).GetText();
            var efficiency = Wait.UntilElementVisible(EfficiencyRatingLabel(rowIndex)).GetText();
            var overallRating = Wait.UntilElementVisible(OverallRatingLabel(rowIndex)).GetText();

            return new TravelerRateAndReview()
            {
                MeetYourNeedsRating = (int)Convert.ToDouble(meetYourNeeds),
                KnowledgeRating = (int)Convert.ToDouble(knowledge),
                CommunicationRating = (int)Convert.ToDouble(communication),
                ProfessionalRelationshipRating = (int)Convert.ToDouble(professionalRelationship),
                SupportRating = (int)Convert.ToDouble(support),
                EfficiencyRating = (int)Convert.ToDouble(efficiency),
                OverallRating = (int)Convert.ToDouble(overallRating),
            };
        }

        public void ClickOnClearFiltersButton()
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(ClearFiltersButton));
        }

        public bool IsClearFilterButtonEnabled()
        {
            return Wait.IsElementEnabled(ClearFiltersButton, 5);
        }

        //SystemAdmin RejectReview
        public string GetRecruiterReviewsFromRow(int rowIndex, int columnIndex)
        {
            return Wait.UntilElementVisible(RecruiterReviewsByNthColumn(rowIndex, columnIndex)).GetText();
        }

        public void ClickOnRejectReviewsButton()
        {
            Wait.UntilElementClickable(RejectReviewsButton).ClickOn();
        }

        public bool IsRejectReviewPopupOpened()
        {
            return Wait.IsElementPresent(RejectReviewPopupRejectAndUnPublishReviewButton, 5);
        }

        public string GetRejectReviewPopupContentText()
        {
            return Wait.UntilElementVisible(RejectReviewPopupReviewContentText).GetText();
        }

        public void ClickOnRejectUnPublishReviewButton()
        {
            Wait.UntilElementClickable(RejectReviewPopupRejectAndUnPublishReviewButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void ClickOnApproveReviewsButton()
        {
            Wait.UntilElementClickable(ApproveReviewsButton).ClickOn();
        }

        public bool IsApproveReviewPopupOpened()
        {
            return Wait.IsElementPresent(ApproveReviewPopupAndPublishReviewButton, 5);
        }

        public void ClickOnApproveReviewPopupButton()
        {
            Wait.UntilElementClickable(ApproveReviewPopupAndPublishReviewButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
            Wait.UntilElementInVisible(ApproveReviewPopupAndPublishReviewButton, 5);
        }

        public DateTime GetRejectReviewDateText()
        {
            return DateTime.ParseExact(Wait.UntilElementVisible(RejectReviewDate).GetText(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }
}

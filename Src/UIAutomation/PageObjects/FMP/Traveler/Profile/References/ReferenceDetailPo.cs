using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.TravelerProfile.ProfileDashboard;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.References
{
    internal class ReferenceDetailPo : FmpBasePo
    {
        public ReferenceDetailPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By AddReferenceButton = By.XPath("//button[contains(@class,'AddItemButton')]//span[text()='Add Reference']");
        private readonly By FirstAndLastNameLabel = By.XPath("//div[@data-testid='desktop-reference-content']//label[contains(@class,'ItemHeaderTitleText')]");
        private readonly By RelationshipLabel = By.XPath("//label[text()='Relationship']//following-sibling::label");
        private readonly By EmailLabel = By.XPath("//label[text()='Email']//following-sibling::label");
        private readonly By EmploymentStartDate = By.XPath("//label[text()='TRAVELER EMPLOYED']//following-sibling::label");
        private readonly By WorkTogetherPlace = By.XPath("//div[contains(@class,'ReferenceLocationItem')][1]");
        private readonly By TitleLabel = By.CssSelector("label[class*='ItemHeaderDescriptionText']");
        private readonly By EditButton = By.XPath("//button[contains(@class,'ContentEditButton')]/span[text()='edit']");
        private readonly By AddEmploymentHistoryLink = By.XPath("//a[text()='employment history']");

        //Device elements
        private readonly By AddReferencesButtonDevice = By.XPath("//button[contains(@class,'indexstyles__ButtonStyled')]//span[text()='Add Reference']");
        private readonly By FirstAndLastNameLabelDevice = By.XPath("//div[@data-testid='mobile-references-content']//label[contains(@class,'ItemHeaderTitleText')]");
        private readonly By EditButtonDevice = By.XPath("//div[contains(@class,'ItemHeaderColumn')]/button");

        public void ClickOnAddReferenceButton()
        {
            Wait.UntilElementClickable(BaseTest.PlatformName != PlatformName.Web ? AddReferencesButtonDevice : AddReferenceButton).ClickOn();
            new ReferencePo(Driver).WaitTillAddReferencePopupHeaderTextDisplay();
        }
        public bool IsAddEmploymentLinkDisplayed()
        {
            return Wait.IsElementPresent(AddEmploymentHistoryLink,5);
        }
        public void ClickOnAddEmploymentHistoryLink()
        {
            Wait.UntilElementClickable(AddEmploymentHistoryLink).ClickOn();
        }
        public void ClickOnEditButton()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(EditButtonDevice));
            }
            Wait.UntilAllElementsLocated(BaseTest.PlatformName != PlatformName.Web ? EditButtonDevice : EditButton).Last(e => e.Displayed).ClickOn();
        }
        public int GetEditButtonCount()
        {
            return Wait.UntilAllElementsLocated(BaseTest.PlatformName != PlatformName.Web ? EditButtonDevice : EditButton).Where(e => e.Displayed).ToList().Count;
        }
        public bool IsReferenceDetailDisplayed()
        {
            return Wait.IsElementPresent(RelationshipLabel,5);
        }
        public string GetReferenceFirstAndLastName()
        {
            return Wait.UntilElementVisible(BaseTest.PlatformName != PlatformName.Web ? FirstAndLastNameLabelDevice : FirstAndLastNameLabel).GetText();
        }
        public Reference GetReferenceDetail()
        {
            var firstnameAndLastname = Wait.UntilAllElementsLocated(BaseTest.PlatformName != PlatformName.Web ? FirstAndLastNameLabelDevice : FirstAndLastNameLabel).Last(e => e.Displayed).GetText().Split(" ");
            var firstname = firstnameAndLastname.First();
            var lastname = firstnameAndLastname.Last();
            var relationship = Wait.UntilAllElementsLocated(RelationshipLabel).Last(e => e.Displayed).GetText();
            var email = Wait.UntilAllElementsLocated(EmailLabel).Last(e => e.Displayed).GetText();
            var title = Wait.UntilAllElementsLocated(TitleLabel).Last(e => e.Displayed).GetText();
            var workTogether = Wait.UntilAllElementsLocated(WorkTogetherPlace).Last(e => e.Displayed).GetText();

            return new Reference
            {
                FirstName = firstname,
                LastName = lastname,
                Relationship = relationship,
                Email = email,
                Title = title,
                WorkTogether = workTogether,
            };
        }

        public DataObjects.FMP.Traveler.Profile.Employment GetStartDateAndEndDate()
        {
            var startAndEndDate =
                Wait.UntilAllElementsLocated(EmploymentStartDate).Last(e => e.Displayed).GetText().RemoveWhitespace();

            var employmentStartDate = startAndEndDate.Split("-").First().RemoveWhitespace();
            var employmentEndDate = startAndEndDate.Contains("Present") ? startAndEndDate.Replace("Present", DateTime.Now.ToString("MM/yyyy").Replace("-", "/")).Split("-").Last() : startAndEndDate.Split("-").Last().RemoveWhitespace();

            return new DataObjects.FMP.Traveler.Profile.Employment
            {
                StartDate = DateTime.ParseExact(employmentStartDate, "MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(employmentEndDate, "MM/yyyy", CultureInfo.InvariantCulture)
            };
        }
    }
}
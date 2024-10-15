using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.Employment
{
    internal class EmploymentDetailsPo : FmpBasePo
    {
        public EmploymentDetailsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By AddPositionButton = By.CssSelector("button[class*='AddItemButton']");

        //Edit Employment Button
        private readonly By EditButton = By.XPath("//button[contains(@class,'ContentEditButton')]/span[contains(@class,'label')]");
      
        //Verify Employment Details
        private readonly By FacilityLabel = By.CssSelector("div[class*='ItemHeaderColumn'] label");
        private readonly By CityAndStateLabel = By.XPath("//label[contains(text(),'My Facility')]//parent::div//label[2]");
        private readonly By DepartmentLabel = By.XPath("//label[text()='Department']//following-sibling::label");
        private readonly By SpecialtyLabel = By.XPath("//label[text()='Specialty']//following-sibling::label");
        private readonly By JobSettingLabel = By.XPath("//label[text()='Job Setting']//following-sibling::label");
        private readonly By HoursLabel = By.XPath("//label[text()='Hours per week']//following-sibling::label");
        private readonly By ChartingSystemsLabel = By.XPath("//label[text()='Charting System']//following-sibling::label");
        private readonly By UnitDetailsLabel = By.XPath("//label[text()='Unit Details']//following-sibling::label");
        private readonly By PatientRatioLabel = By.XPath("//label[text()='Patient Ratio']//following-sibling::label");
        private readonly By ChargeOrSupervisor = By.CssSelector("label[class*='ItemHeaderDescriptionSubText']");
        private readonly By JobDescription = By.XPath("//label[text()='Job Description']//following-sibling::label");

        //Time Between Jobs
        private readonly By TimeBetweenJobsText = By.XPath("//div[contains(@class,'ItemContent')]//p");
        private readonly By AddTimeBetweenJobsButton = By.XPath("//div[contains(@class,'ItemContent')]//p/span");
        private readonly By EditTimeBetweenJobsButton = By.XPath("//label[text()='Time Between Jobs']/parent::div/parent::div/parent::div/preceding-sibling::div/button");
        private readonly By EditTimeBetweenJobsButtonDeivce = By.XPath("//label[text()='Time Between Jobs']/parent::div/following-sibling::div/button");

        //Time Between Jobs Details
        private static By TimeBetweenJobsHeaderLabel(int row) => By.XPath($"//div[@data-testid='desktop-employment-content'][{row}]//label[contains(@class,'ItemHeaderTitleText')]");
        private static By TimeBetweenJobsCityAndStateLabel(int row) => By.XPath($"//div[@data-testid='desktop-employment-content'][{row}]//label[contains(@class,'ItemHeaderDescriptionText')]");
        private static By TimeBetweenJobsStartAndEndDateLabel(int row) => By.XPath($"//div[@data-testid='desktop-employment-content'][{row}]//label[contains(@class,'ItemHeaderTitleSubText')]");
        private static By TimeBetweenJobsNonMedicalEmploymentLabel(int row) => By.XPath($"//div[@data-testid='desktop-employment-content'][{row}]//label[contains(@class,'ItemHeaderDescriptionSubText')][text()='Non-medical employment']");

        //Device elements
        private readonly By AddEmploymentButtonDevice = By.CssSelector("button[class*='EmploymentEditButton']");
        private static By TimeBetweenJobsCityAndStateLabelDevice(int row) => By.XPath($"//div[@data-testid='mobile-employment-content'][{row}]//label[contains(@class,'ItemHeaderDescriptionText')]");


        public void ClickOnAddPositionButton()
        {
            if (Wait.IsElementPresent(EditButton,5))
            {
                DeleteAllEmployment();
            }

            if (BaseTest.PlatformName != PlatformName.Web)
            {
                if (BaseTest.PlatformName.Equals(PlatformName.Android))
                {
                    Driver.MoveToElement(Wait.UntilElementExists(AddEmploymentButtonDevice));
                }
                Wait.UntilElementClickable(AddEmploymentButtonDevice).ClickOn();
            }
            else
            {
                Wait.UntilElementVisible(AddPositionButton, 5);
                Wait.UntilElementClickable(AddPositionButton).ClickOn();
            }
            new EmploymentPo(Driver).WaitTillEmploymentPopupHeaderGetsDisplay();
        }

        //Edit Employment
        public void ClickOnEditButton()
        {
            Wait.UntilElementVisible(EditButton,20);
            if (BaseTest.PlatformName != PlatformName.Web)
            {
               if (BaseTest.PlatformName.Equals(PlatformName.Android))
               {
                 Driver.MoveToElement(Wait.UntilElementExists(EditButton));
               }
               Wait.UntilAllElementsLocated(EditButton).First(e => e.Displayed).ClickOn();
            }
            else
            {
                Wait.UntilAllElementsLocated(EditButton).Last(e => e.Displayed).ClickOn();
            }
        }

        //Delete All Employment Details 
        public void DeleteAllEmployment()
        {
            var empPo = new EmploymentPo(Driver);

            var allElement = Wait.UntilAllElementsLocated(EditButton).Where(e => e.Displayed).ToList().Count;
            for (var i = 0; i < allElement; i++)
            {
                Driver.JavaScriptClickOn(Wait.UntilElementExists(EditButton));
                new EmploymentPo(Driver).WaitTillEmploymentPopupHeaderGetsDisplay();
                empPo.ClickOnDeleteEmploymentButton();
                Wait.HardWait(2000);
                Driver.RefreshPage();
                Wait.HardWait(1000);
                WaitUntilFmpTextLoadingIndicatorInvisible();
            }
        }

        //Verify Employment Details 
        public DataObjects.FMP.Traveler.Profile.Employment GetEmploymentDetailsFromProfileDetailPage()
        {
            var employmentDetails = new DataObjects.FMP.Traveler.Profile.Employment();
            if (Wait.IsElementPresent(CityAndStateLabel,3))
            {
                var cityAndState = Wait.UntilElementVisible(CityAndStateLabel).GetText();
                employmentDetails.City = cityAndState.Split(",").First();
                employmentDetails.State = cityAndState.Split(" ").Last();
                employmentDetails.OtherFacility = Wait.UntilElementVisible(FacilityLabel).GetText();
            }
            employmentDetails.FacilityOption = Wait.UntilElementVisible(FacilityLabel).GetText();
            employmentDetails.SupervisorEmployment = Wait.IsElementPresent(ChargeOrSupervisor, 5);
            employmentDetails.Category = Wait.UntilElementVisible(DepartmentLabel).GetText();
            employmentDetails.Specialty = Wait.UntilElementVisible(SpecialtyLabel).GetText();
            employmentDetails.JobSettingInput = Wait.UntilAllElementsLocated(JobSettingLabel).Select(e => e.GetText()).ToList();
            employmentDetails.Hours = Wait.UntilElementVisible(HoursLabel).GetText();
            employmentDetails.ChartingSystemInput = Wait.UntilAllElementsLocated(ChartingSystemsLabel).Select(e => e.GetText()).ToList();
            employmentDetails.UnitAmount = Wait.UntilElementVisible(UnitDetailsLabel).GetText().Split(" ").First();
            employmentDetails.UnitType = Wait.UntilElementVisible(UnitDetailsLabel).GetText().Split(" ").Last();
            employmentDetails.PatientRatio = Wait.UntilElementVisible(PatientRatioLabel).GetText();
            employmentDetails.JobDescription = Wait.UntilElementVisible(JobDescription).GetText();
            return employmentDetails;
        }

        public bool IsEmploymentDetailsHeaderTextDisplayed(string headerText)
        {
            if (!Wait.IsElementPresent(FacilityLabel,5)) return false;
            var allElements = Wait.UntilAllElementsLocated(FacilityLabel);
            return allElements.Select(e => e.GetText().Trim().Equals(headerText)).FirstOrDefault();
        }

        public string GetAddTimeBetweenJobsText()
        {
            return Wait.UntilElementVisible(TimeBetweenJobsText).GetText().Replace("\n", "").Replace("\r", "").Replace("\t", "");
        }

        public bool IsAddTimeBetweenJobsSectionDisplayed()
        {
            return Wait.IsElementPresent(TimeBetweenJobsText,5);
        }

        public void ClickOnAddTimeBetweenJobsButton()
        {
            Wait.UntilElementClickable(AddTimeBetweenJobsButton).ClickOn();
            new TimeBetweenJobsPo(Driver).WaitTillAddTimeBetweenJobsPopupGetsVisible();
        }

        public void ClickOnEditTimeBetweenJobsButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementVisible(EditTimeBetweenJobsButtonDeivce, 20);
                Wait.UntilAllElementsLocated(EditTimeBetweenJobsButtonDeivce).First().ClickOn();
            }
            else
            {
                Wait.UntilElementVisible(EditTimeBetweenJobsButton, 20);
                Wait.UntilAllElementsLocated(EditTimeBetweenJobsButton).First().ClickOn();
            }
        }

        public string GetTimeOffDetailsHeaderText(int row)
        {
            return Wait.UntilElementVisible(TimeBetweenJobsHeaderLabel(row)).GetText();
        }

        public TimeBetweenJobs GetTimeOffBetweenJobsDetails(int row)
        {
            var city = Wait.UntilElementVisible(TimeBetweenJobsCityAndStateLabel(row)).GetText().Split(", ").First();
            var state = Wait.UntilElementVisible(TimeBetweenJobsCityAndStateLabel(row)).GetText().Split(", ").Last();
            var startDate = Wait.UntilElementVisible(TimeBetweenJobsStartAndEndDateLabel(row)).GetText().Split(" -").First();
            var endDate = Wait.UntilElementVisible(TimeBetweenJobsStartAndEndDateLabel(row)).GetText().Split("- ").Last();
            return new TimeBetweenJobs
            {
                City = city,
                State = state,
                StartDate = DateTime.ParseExact(startDate, "MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(endDate, "MM/yyyy", CultureInfo.InvariantCulture),
            };
        }

        public string GetStateName(int row)
        {
            return Wait.UntilElementVisible(BaseTest.PlatformName != PlatformName.Web ? TimeBetweenJobsCityAndStateLabelDevice(row) : TimeBetweenJobsCityAndStateLabel(row)).GetText().Split(", ").First();
        }

        public bool IsNonMedicalFieldLabelDisplayed(int row)
        {
            return Wait.IsElementPresent(TimeBetweenJobsNonMedicalEmploymentLabel(row), 5);
        }
    }
}

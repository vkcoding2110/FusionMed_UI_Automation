using System;
using System.Collections.Generic;
using System.Globalization;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.Components;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.Employment
{
    internal class TimeBetweenJobsPo : FmpBasePo
    {
        public TimeBetweenJobsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By TimeOffCheckBoxInput = By.XPath("//span[contains(text(),'currently taking time off between healthcare jobs')]/preceding-sibling::span");
        private readonly By TimeOffSelectedCheckBox = By.XPath("//span[contains(@class,'MuiCheckbox-colorPrimary')][contains(@class,'checked')]/following-sibling::span");
        private readonly By StartDate = By.CssSelector("input#employment-start-date");
        private readonly By EndDate = By.CssSelector("input#employment-end-date");
        private readonly By TimeAwayCity = By.CssSelector("input#time-away-city");
        private readonly By TimeAwayState = By.CssSelector("select#time-away-state");
        private readonly By NonMedicalFieldCheckBoxInput = By.XPath("//span[contains(text(),'working in a non-medical field')]/preceding-sibling::span");
        private readonly By NonMedicalFieldSelectedCheckBox = By.XPath("//span[contains(text(),'working in a non-medical field')]");
        private readonly By AddTimeBetweenJobsButton = By.XPath("//button[contains(@class,'EmploymentEditButton')]/span[text()='Add Time Between Jobs']");
        private readonly By SubmitButton = By.XPath("//button[contains(@class,'EmploymentEditButton')]/span[text()='Submit']");
        private readonly By AddTimeBetweenJobsHeaderText = By.XPath(" //div[contains(@class,'TimeAwayWrapper')]//h5[contains(@class,'EditHeaderText')]");
       
        //Delete Time Between Jobs Button
        private readonly By DeleteTimeBetweenJobsButton = By.XPath("//button[contains(@class,'DeleteButtonStyled')]/span[text()=' delete time between jobs']");
        private readonly By DeleteConfirmationButton = By.CssSelector("button[class*='DeleteConfirmationButton'] span");

        //Close Icon & Cancel Button
        private readonly By CloseIcon = By.CssSelector("div [class*='MuiDialog'] button[class*='CloseIconWrapper']");
        private readonly By CancelButton = By.XPath("//div[contains(@class,'ButtonContainer')]//button//span[text()='cancel']");

        //Validation Message
        private readonly By StartDateValidationMessage = By.XPath("//div[contains(@class,'MonthYearSelectsStyled')]//label[text()='Start Date *']//following-sibling::p");
        private readonly By EndDateValidationMessage = By.XPath("//div[contains(@class,'MonthYearSelectsStyled')]//label[text()='End Date *']//following-sibling::p");
        private readonly By CityValidationMessage = By.XPath("//div[contains(@class,'InputStyled')]//label[text()='City']//following-sibling::p");
        private readonly By StateValidationMessage = By.XPath("//div[contains(@class,'SelectStyled')]//label[text()='State']//following-sibling::p");

        public void WaitTillAddTimeBetweenJobsPopupGetsVisible()
        {
            Wait.WaitUntilTextRefreshed(AddTimeBetweenJobsHeaderText);
        }

        public bool IsAddTimeBetweenJobsPopUpOpened()
        {
            Wait.HardWait(1000);
            return Wait.IsElementPresent(AddTimeBetweenJobsHeaderText, 5);
        }

        public void EnterTimeBetweenJobsData(TimeBetweenJobs timeOff)
        {
            SelectDeselectTimeOffCheckBox(timeOff.TimeOff);
            SelectStartDate(timeOff.StartDate);
            if (!timeOff.TimeOff)
            {
                SelectEndDate(timeOff.EndDate);
            }
            EnterTimeAwayCity(timeOff.City);
            SelectTimeAwayState(timeOff.State);
            SelectDeselectNonMedicalFieldCheckBox(timeOff.NonMedicalField);
            if (Wait.IsElementPresent(SubmitButton,5))
            {
                ClickOnSubmitButton();
            }
            else
            {
                ClickOnAddTimeBetweenJobsButton();
                WaitUntilFmpPageLoadingIndicatorInvisible();
            }
        }

        public void SelectDeselectTimeOffCheckBox(bool timeOff)
        {
            Wait.UntilElementClickable(TimeOffCheckBoxInput).ClickOn();
            if (timeOff)
            {
                if (!Wait.IsElementPresent(TimeOffSelectedCheckBox,5))
                {
                    Wait.UntilElementClickable(TimeOffCheckBoxInput).ClickOn();
                }
            }
            else
            {
                if (Wait.IsElementPresent(TimeOffSelectedCheckBox))
                {
                    Wait.UntilElementClickable(TimeOffCheckBoxInput).ClickOn();
                }
            }
        }

        public bool IsEndDateInputPresent()
        {
            return Wait.IsElementPresent(EndDate,5);
        }

        public void SelectStartDate(DateTime startDate)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectMonthAndYear(startDate, StartDate);
        }

        public void ClearStartDate()
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.ClearDateSelection(StartDate);
        }

        public void SelectEndDate(DateTime endDate)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectMonthAndYear(endDate, EndDate);
        }

        public void ClearEndDate()
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.ClearDateSelection(EndDate);
        }

        public void EnterTimeAwayCity(string city)
        {
            Wait.UntilElementVisible(TimeAwayCity).EnterText(city, true);
        }

        public void SelectTimeAwayState(string state)
        {
            Wait.UntilElementClickable(TimeAwayState).SelectDropdownValueByText(state, Driver);
        }

        public void SelectDeselectNonMedicalFieldCheckBox(bool timeOff)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(StartDate));
            Wait.UntilElementClickable(NonMedicalFieldCheckBoxInput).ClickOn();
            if (timeOff)
            {
                if (!Wait.IsElementPresent(NonMedicalFieldSelectedCheckBox))
                {
                    Wait.UntilElementClickable(NonMedicalFieldCheckBoxInput).ClickOn();
                }
            }
            else
            {
                if (!Wait.IsElementPresent(NonMedicalFieldSelectedCheckBox))
                {
                    Wait.UntilElementClickable(NonMedicalFieldCheckBoxInput).ClickOn();
                }
            }
        }

        public void ClickOnAddTimeBetweenJobsButton()
        {
            Wait.UntilElementClickable(AddTimeBetweenJobsButton).ClickOn();
        }

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public List<TimeBetweenJobs> GetTimeBetweenJobsDetailsFromPopUp()
        {
            var startDate = Wait.UntilElementVisible(StartDate).GetAttribute("value");
            var endDate = Wait.UntilElementVisible(EndDate).GetAttribute("value");
            var city = Wait.UntilElementVisible(TimeAwayCity).GetAttribute("value");
            var state = Wait.UntilElementVisible(TimeAwayState).SelectDropdownGetSelectedValue();

            return new List<TimeBetweenJobs>()
            {
                new TimeBetweenJobs()
                {
                    StartDate = DateTime.ParseExact(startDate, "MMMM yyyy", CultureInfo.InvariantCulture),
                    EndDate = DateTime.ParseExact(endDate, "MMMM yyyy", CultureInfo.InvariantCulture),
                    City = city,
                    State = state
                }
            };
        }

        public void ClickOnDeleteTimeBetweenJobsButton()
        {
            Wait.UntilElementClickable(DeleteTimeBetweenJobsButton).ClickOn();
            Wait.UntilElementClickable(DeleteConfirmationButton).ClickOn();
            new FmpBasePo(Driver).WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.HardWait(2000);
        }

        public bool IsTimeOffPopUpPresent()
        {
            return Wait.IsElementPresent(CloseIcon,3);
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.HardWait(2000);
        }

        public string GetStartDateValidationMessage()
        {
            return Wait.UntilElementVisible(StartDateValidationMessage).GetText();
        }

        public string GetEndDateValidationMessage()
        {
            return Wait.UntilElementVisible(EndDateValidationMessage).GetText();
        }

        public string GetTimeAwayCityValidationMessage()
        {
            return Wait.UntilElementVisible(CityValidationMessage).GetText();
        }

        public void ClearTimeAwayState()
        {
            Wait.UntilElementClickable(TimeAwayState).SelectDropdownValueByIndex(0);
        }

        public string GetTimeAwayStateValidationMessage()
        {
            return Wait.UntilElementVisible(StateValidationMessage).GetText();
        }
    }
}

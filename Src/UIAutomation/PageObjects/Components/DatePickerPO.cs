using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Components
{
    internal class DatePickerPo : BasePo
    {
        public DatePickerPo(IWebDriver driver) : base(driver)
        {
        }

        //FlatPicker Calender
        private readonly By FlatPickerCalenderField = By.CssSelector("div[class*='flatpickr-calendar']");
        private readonly By FlatPickerCalenderSelectedMonth = By.CssSelector("select[class*='monthDropdown']");
        private readonly By FlatPickerCalenderSelectedYear = By.CssSelector("input[class='numInput cur-year']");
        private readonly By FlatPickerCalenderIncreaseYear = By.CssSelector("span[class='arrowUp']");
        private readonly By FlatPickerCalenderDecreaseYear = By.CssSelector("span[class='arrowDown']");
        private static By FlatPickerCalenderDateSelect(string day) => By.XPath($"//span[@class='flatpickr-day '][text()='{day}']");

        //Range Calender Common Locators 
       
        private static By RangeCalenderYearOption(string year) => By.XPath($"//div[contains(@class,'MuiPickersYearSelection')]/div[text()='{year}']");
        private readonly By RangeCalenderMonthAndYearText = By.XPath("//div[contains(@class,'transitionContainer')]/p");
        private static By RangeCalenderSelectDay(string day) => By.XPath($"//div[@role='presentation']//p[text()='{day}']");
        private readonly By RangeCalenderOkButton = By.XPath("//div[contains(@class,'MuiDialogActions')]/button/span[text()='OK']");

        //Calendar for Android
        private readonly By CalendarNextButtonAndroid = By.XPath("//*[@resource-id='android:id/next']");
        private readonly By CalendarPreviousButtonAndroid = By.XPath("//*[@resource-id='android:id/prev']");
        private readonly By CalendarMonthAndroid = By.XPath("//*[@resource-id='android:id/date_picker_header_date']");
        private readonly By CalendarYearAndroid = By.XPath("//*[@resource-id='android:id/date_picker_header_year']");
        private readonly By CalenderSetAndroidButton = By.XPath("//android.widget.Button[@text='SET'] | //android.widget.Button[@text='OK']");
        private static By CalenderDayAndroid(string day) => By.XPath($"//android.view.View[@text='{day}']");
        private readonly By CalendarClearButtonAndroid = By.XPath("//*[@resource-id='android:id/button3'] | //android.widget.Button[@text='CLEAR']");

        //MonthYearPicker
        private static By SelectedMonthOrYear(string monthOrYear) => By.XPath($"//div[@class='MuiPickersBasePicker-pickerView']//div[text()='{monthOrYear}']");
        private readonly By ClearButton = By.XPath("//span[text()='Clear']/parent::button");

        //Range Calender Month View
        private readonly By RangeCalenderMonthViewYearDropDown = By.XPath("//h6[contains(@class,'MuiTypography-subtitle1')]");
        private readonly By RangeCalenderMonthViewPreviousMonthButton = By.XPath("//button[contains(@class,'MuiPickersCalendarHeader-iconButton')][@tabindex='0']");
        private readonly By RangeCalenderMonthViewNextMonthButton = By.XPath("button //button[contains(@class,'MuiPickersCalendarHeader-iconButton')][@tabindex='-1']");

        //Range Calender Year View
        private readonly By RangeCalenderYearViewPreviousMonthButton = By.XPath("//div[contains(@class,' MuiPickersCalendarHeader-transitionContainer')]//preceding-sibling::button");
        private readonly By RangeCalenderYearViewNextMonthButton = By.XPath("//div[contains(@class,' MuiPickersCalendarHeader-transitionContainer')]//following-sibling::button");

        public void SelectDate(DateTime date, By dateField, CalenderPicker calenderPicker = CalenderPicker.FlatPickerCalender)
        {
            Wait.UntilElementClickable(dateField).ClickOn();

            var day = date.ToString("%d");
            var monthName = date.ToString("MMMM");
            var month = Convert.ToInt32(date.ToString("MM"));
            var year = Convert.ToInt32(date.ToString("yyyy"));

            if (BaseTest.PlatformName != PlatformName.AndroidApp)
            {
                Wait.UntilElementVisible(dateField, 10);
            }

            switch (calenderPicker)
            {
                case CalenderPicker.FlatPickerCalender:
                    {
                        switch (BaseTest.PlatformName)
                        {
                            case PlatformName.Web:

                                Wait.UntilElementVisible(FlatPickerCalenderSelectedMonth);
                                var datePicker = Wait.UntilAllElementsLocated(FlatPickerCalenderField).First(e => e.IsDisplayed());
                                var selectedMonth = datePicker.FindElement(FlatPickerCalenderSelectedMonth);
                                var selectedYear = datePicker.FindElement(FlatPickerCalenderSelectedYear);
                                var increaseYear = datePicker.FindElement(FlatPickerCalenderIncreaseYear);
                                var decreaseYear = datePicker.FindElement(FlatPickerCalenderDecreaseYear);

                                var yearInt = Convert.ToInt32(selectedYear.GetAttribute("value"));
                                selectedMonth.SelectDropdownValueByText(monthName);
                                if (yearInt > year)
                                {
                                    var diff = yearInt - year;
                                    for (var i = 1; i <= diff; i++)
                                    {
                                        decreaseYear.ClickOn();
                                    }
                                }
                                else if (yearInt < year)
                                {
                                    var diff = year - yearInt;
                                    for (var i = 1; i <= diff; i++)
                                    {
                                        increaseYear.ClickOn();
                                    }
                                }
                                Wait.UntilAllElementsLocated(FlatPickerCalenderDateSelect(day)).First(e => e.Displayed).ClickOn();
                                break;

                            case PlatformName.Android or PlatformName.AndroidApp:

                                var androidDriver = (AndroidDriver<AndroidElement>)Driver;
                                androidDriver.Context = "NATIVE_APP";

                                var currentCalendar = DateTime.ParseExact(Wait.UntilElementVisible(CalendarMonthAndroid).GetText(), "ddd, MMM d", CultureInfo.InvariantCulture);
                                var currentYear = DateTime.ParseExact(Wait.UntilElementVisible(CalendarYearAndroid).GetText(), "yyyy", CultureInfo.InvariantCulture);
                                if (currentCalendar.Month == date.Month && currentYear.Year == date.Year)
                                {
                                    Wait.UntilElementVisible(CalenderDayAndroid(day)).Click();
                                    Wait.UntilElementClickable(CalenderSetAndroidButton).ClickOn();
                                    if (BaseTest.PlatformName != PlatformName.AndroidApp)
                                    {
                                        androidDriver.Context = "CHROMIUM";
                                    }
                                    return;
                                }
                                var monthDifference = (date.Year - currentCalendar.Year) * 12 + date.Month - currentCalendar.Month;

                                for (var i = 0; i < Math.Abs(monthDifference); i++)
                                {
                                    switch (monthDifference)
                                    {
                                        case > 0:
                                            Wait.UntilElementClickable(CalendarNextButtonAndroid).Click();
                                            break;
                                        case < 0:
                                            Wait.UntilElementClickable(CalendarPreviousButtonAndroid).Click();
                                            break;
                                    }
                                }
                                Wait.UntilElementVisible(CalenderDayAndroid(day)).Click();
                                Wait.UntilElementClickable(CalenderSetAndroidButton).ClickOn();
                                if (BaseTest.PlatformName != PlatformName.AndroidApp)
                                {
                                    androidDriver.Context = "CHROMIUM";
                                }
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    }
                case CalenderPicker.RangeCalenderMonthView:
                    {
                        switch (BaseTest.PlatformName)
                        {
                            case PlatformName.Web:
                                var selectedMonthName = Wait.UntilElementVisible(RangeCalenderMonthAndYearText).GetText().Split().First();
                                var actualMonth = DateTime.ParseExact(selectedMonthName, "MMMM", CultureInfo.InvariantCulture).Month;
                                Wait.HardWait(1000);
                                Wait.UntilElementClickable(RangeCalenderMonthViewYearDropDown).ClickOn();
                                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(RangeCalenderYearOption(date.ToString("yyyy"))));
                                Wait.UntilElementVisible(RangeCalenderYearOption(date.ToString("yyyy"))).ClickOn();
                                if (actualMonth > month)
                                {
                                    var diff = actualMonth - month;
                                    for (var i = 1; i <= diff; i++)
                                    {
                                        Wait.UntilElementClickable(RangeCalenderMonthViewPreviousMonthButton).ClickOn();
                                    }
                                }
                                else if (actualMonth < month)
                                {
                                    var diff = actualMonth - month;
                                    for (var i = 1; i <= diff; i++)
                                    {
                                        Wait.UntilElementClickable(RangeCalenderMonthViewNextMonthButton).ClickOn();
                                    }
                                }
                                Wait.HardWait(1000);
                                Wait.UntilAllElementsLocated(RangeCalenderSelectDay(day)).First(e => e.Displayed).ClickOn();
                                Wait.UntilElementClickable(RangeCalenderOkButton).ClickOn();
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                case CalenderPicker.RangeCalenderYearView:
                    {
                        switch (BaseTest.PlatformName)
                        {
                            case PlatformName.Web:
                                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(RangeCalenderYearOption(date.ToString("yyyy"))));
                                Wait.UntilElementClickable(RangeCalenderYearOption(date.ToString("yyyy"))).ClickOn();

                                var selectedMonthName = Wait.UntilElementVisible(RangeCalenderMonthAndYearText).GetText().Split().First();
                                var actualMonth = DateTime.ParseExact(selectedMonthName, "MMMM", CultureInfo.InvariantCulture).Month;
                                Wait.HardWait(1000);
                                if (actualMonth > month)
                                {
                                    var diff = actualMonth - month;
                                    for (var i = 1; i <= diff; i++)
                                    {
                                        Wait.UntilElementClickable(RangeCalenderYearViewPreviousMonthButton).ClickOn();
                                    }
                                }
                                else if (actualMonth < month)
                                {
                                    var diff = Math.Abs(actualMonth - month);
                                    for (var i = 1; i <= diff; i++)
                                    {
                                        Wait.UntilElementClickable(RangeCalenderYearViewNextMonthButton).ClickOn();
                                    }
                                }
                                Wait.HardWait(1000);
                                Wait.UntilAllElementsLocated(RangeCalenderSelectDay(day)).First(e => e.Displayed).ClickOn();
                                Wait.UntilElementClickable(RangeCalenderOkButton).ClickOn();
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(calenderPicker), calenderPicker, null);
            }

        }

        public void SelectMonthAndYear(DateTime date, By dateFieldInput)
        {
            Wait.UntilElementClickable(dateFieldInput).ClickOn();
            var month = date.ToString("MMM");
            var year = date.ToString("yyyy");
            Wait.UntilElementClickable(SelectedMonthOrYear(year)).ClickOn();
            Wait.UntilElementClickable(SelectedMonthOrYear(month)).ClickOn();
            Wait.UntilElementInVisible(SelectedMonthOrYear(month));
        }
        public void ClearDateSelection(By dateFieldInput)
        {
            Wait.UntilElementClickable(dateFieldInput).ClickOn();
            switch (BaseTest.PlatformName)
            {
                case PlatformName.Web:
                {
                    Wait.UntilElementClickable(ClearButton).ClickOn();
                    Wait.UntilElementInVisible(ClearButton);
                    break;
                }
                case PlatformName.Android:
                {
                    var androidDriver = (AndroidDriver<AndroidElement>)Driver;
                    androidDriver.Context = "NATIVE_APP";
                    Wait.UntilElementClickable(CalendarClearButtonAndroid).ClickOn();
                    Wait.UntilElementInVisible(CalendarClearButtonAndroid);
                    androidDriver.Context = "CHROMIUM";
                    break;
                }
                case PlatformName.Ios:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

using System;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core
{
    internal class HolidayTimePo : CoreBasePo
    {
        public HolidayTimePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By Iframe = By.CssSelector("iframe.iframe.fullheight");
        private readonly By HolidayTimeHeader = By.CssSelector("div.header span.lead");
        private readonly By HolidayDateInput = By.Id("txtHolidayDate");

        private static By HolidayCalenderDayLocator(string day) => By.XPath($"(//table[@class='ui-datepicker-calendar'])//a[text()={day}]");
        private readonly By NextMonthButton = By.CssSelector("div.ui-datepicker-header a[title='Next']");
        private readonly By ChooseFileInput = By.CssSelector("input#FileUpload.form-control");
        private readonly By UploadFileButton = By.Id("upload-form-input");


        
        public void SwitchToIFrame()
        {
            Driver.SwitchToDefaultIframe();
            Driver.SwitchToIframe(Wait.UntilElementExists(Iframe));
        }

        public string GetHolidayTimeHeaderText() 
        {
            return Wait.UntilElementVisible(HolidayTimeHeader).GetText();

        }    

        public void SelectTomorrowDateFromCalendar()
        {           
            Wait.HardWait(2000);
            Wait.UntilElementVisible(HolidayDateInput).ClickOn();
            var tomorrowDate = DateTime.Today.AddDays(1).ToString("dd");
            if (tomorrowDate.Equals("30") || tomorrowDate.Equals("31") || tomorrowDate.Equals("01"))
            {
                tomorrowDate = "1";
                Wait.UntilElementClickable(NextMonthButton).ClickOn();
            }
            Wait.UntilElementVisible(HolidayCalenderDayLocator(tomorrowDate)).ClickOn();
        }

        public string GetHolidayDateAttribute()
        {
            return Wait.UntilElementClickable(HolidayDateInput).GetAttribute("value");
        }

        public void ChooseFile(string path)
        {
            Wait.UntilElementClickable(ChooseFileInput).SendKeys(path);
            Wait.HardWait(3000);
        }

        public string GetSelectedFileAttribute()
        {
            return Wait.UntilElementClickable(ChooseFileInput).GetAttribute("value");
        }

        public void ClickOnUploadFileButton()
        {
            Wait.UntilElementClickable(UploadFileButton).ClickOn();
        }
    }
}

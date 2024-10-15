using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Mailinator
{
    internal class EmailListingGridPo : BasePo
    {
        public EmailListingGridPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By AllEmailRows = By.CssSelector("table[class*='jambo_table'] tbody tr");
        private readonly By SubjectInfo = By.CssSelector("td:nth-of-type(4)");

        public static IDictionary<string, string> AllEmailsSubjectAndFromInfo { get; internal set; }

        public void OpenEmail(string from, string subject)
        {
            var allRows = Wait.UntilAllElementsLocated(AllEmailRows).Where(e => e.Displayed).ToList();
            if (allRows.Select(element => element.GetText()).Any(rowText => rowText.Contains(subject) && rowText.Contains(@from)))
            {
                Wait.UntilElementClickable(SubjectInfo).FindElement(By.TagName("a")).ClickOn();
            }
        }

    }
}

using OpenQA.Selenium;
using System.Linq;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Common
{
    internal class ResultGridCommonPo : CoreBasePo
    {
        public ResultGridCommonPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By GridIFrame = By.XPath("//div[contains(@id,'v-pills-pane')]//iframe[@class='iframe fullheight']");
        private readonly By GridTitle = By.ClassName("header");
        public string GetGridHeader()
        {
            SwitchToGrid();
            var data = Wait.UntilElementVisible(GridTitle, 30).GetText();
            Driver.SwitchToDefaultIframe();
            return data;
        }

        public void SwitchToGrid()
        {
            Driver.SwitchToIframe(Wait.UntilAllElementsLocated(GridIFrame).First(e => e.Displayed));
            WaitUntilCoreProcessingTextInvisible();
        }
    }
}

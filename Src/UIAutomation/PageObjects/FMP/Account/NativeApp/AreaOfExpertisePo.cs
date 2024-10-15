using OpenQA.Selenium;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account.NativeApp
{
    internal class AreaOfExpertisePo : FmpBasePo
    {
        public AreaOfExpertisePo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By HeyText = By.XPath("//android.widget.TextView[contains(@text,'Hey')]");
        private readonly By CategoryDropdown = By.XPath("//*[@class='android.widget.TextView'][@text='Category / Discipline *']/following-sibling::android.widget.TextView");
        private readonly By PrimarySpecialtyDropdown = By.XPath("//*[@class='android.widget.TextView'][@text='Primary Specialty *']/following-sibling::android.widget.TextView");
        private static By ItemsList(string item) => By.XPath($"//*[@class='android.widget.TextView'][@text='{item}']");
        private readonly By LetsGoButton = By.XPath("//android.widget.TextView[contains(@text,'Go')]/parent::android.widget.Button");

        public void FillAreaOfExpertiseForm()
        {
            if (!Wait.IsElementPresent(HeyText)) return;
            var data = EmploymentDataFactory.AddEmploymentDetails();
            ClickOnCategoryDropdown(data.Category);
            ClickOnPrimarySpecialtyDropdown(data.Specialty);
            ClickOnLetsGoButton();
        }

        private void ClickOnCategoryDropdown(string category)
        {
            Wait.UntilElementClickable(CategoryDropdown).ClickOn();
            Wait.UntilElementClickable(ItemsList(category)).ClickOn();
        }

        private void ClickOnPrimarySpecialtyDropdown(string specialty)
        {
            Wait.UntilElementClickable(PrimarySpecialtyDropdown).ClickOn();
            Wait.UntilElementClickable(ItemsList(specialty)).ClickOn();
        }

        private void ClickOnLetsGoButton()
        {
            Wait.UntilElementClickable(LetsGoButton).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }
    }
}

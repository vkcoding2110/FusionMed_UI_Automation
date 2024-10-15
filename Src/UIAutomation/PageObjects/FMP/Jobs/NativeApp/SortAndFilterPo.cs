using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs.NativeApp
{
    internal class SortAndFilterPo : FmpBasePo
    {
        public SortAndFilterPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By SortAndFilterHeaderText = By.XPath("//*[@text='Sort & Filter']");

        private readonly By SortAndFilterCloseIcon = By.XPath("//*[@text='Sort & Filter']/preceding-sibling::android.widget.Button");
        private static By SortAndFilterCategoryDropDown(string category) => By.XPath($"//android.widget.TextView[@text='{category}']/following-sibling::android.view.ViewGroup[1]");
        private readonly By ResetAllButton = By.XPath("//android.widget.TextView[@text='Reset all']//parent::android.widget.Button");
        private static By DepartmentSubCategoryRadioButton(string subCategory) => By.XPath($"//android.widget.TextView[@text='{subCategory}']/preceding-sibling::android.widget.RadioButton");
        private static By CategoryCheckBox(string checkbox) => By.XPath($"//*[@class='android.widget.TextView'][@text='{checkbox}']/preceding-sibling::android.widget.CheckBox");

        private readonly By ShowAllResultsButton = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'Results')]//parent::android.widget.Button");

        private readonly By FirstJobCard = By.XPath("//android.widget.HorizontalScrollView/parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.view.ViewGroup[1]");

        private readonly By FilterTagText = By.XPath("//android.widget.HorizontalScrollView/android.view.ViewGroup//android.widget.Button/android.view.ViewGroup/android.widget.TextView[1]");

        public bool IsSortAndFilterPopupPresent()
        {
           return  Wait.IsElementPresent(SortAndFilterHeaderText, 5);
        }

        public void ClickOnSortAndFilterCloseIcon()
        {
            Wait.UntilElementClickable(SortAndFilterCloseIcon).ClickOn();
            Wait.UntilElementInVisible(SortAndFilterCloseIcon,2);
        }

        public void ClickOnSortAndFilterCategoryDropDown(string category)
        {
            Wait.UntilElementVisible(SortAndFilterCategoryDropDown(category)).ClickOn();
        }

        public void ClickOnResetAllButton()
        {
            Wait.UntilElementClickable(ResetAllButton).ClickOn();
        }

        public void ClickOnDepartmentSubCategoryRadioButton(string subCategory)
        {
            Wait.UntilElementVisible(DepartmentSubCategoryRadioButton(subCategory)).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }

        public void ClickOnCategoryCheckbox(string checkbox)
        {
            Wait.UntilElementVisible(CategoryCheckBox(checkbox)).ClickOn();
        }

        public void ClickOnShowAllResultsButton()
        {
            Wait.UntilElementClickable(ShowAllResultsButton).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }

        public void ClickOnFirstJobCard()
        {
            Wait.UntilElementClickable(FirstJobCard).ClickOn();
            Wait.UntilElementInVisible(FirstJobCard);
        }

        public IList<string> GetFilterTagTextList()
        {
            return Wait.UntilAllElementsLocated(FilterTagText).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }
    }
}

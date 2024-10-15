
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;
using UIAutomation.DataObjects.FMP.GreenkartDataObj;

namespace UIAutomation.PageObjects.FMP.Greenkart
{
    internal class GreenkartPo : FmpBasePo
    {
        public GreenkartPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By GreenCartLogo =
            By.XPath("//span[contains(text(),'KART')]/parent::div[contains(@class,'brand greenLogo')]");
        private static By Itemquantity(string itemname) => By.XPath($"//div[contains(@class, 'product')]/div/h4[contains(text(), '{itemname}')]/parent::div/child::div[2]/input");

        private readonly By AddToCartButton =
            By.XPath("(//div[contains(@class, 'product-action')]/button[contains(text(), 'ADD TO CART')])[1]");
        private readonly By CartIcon = By.XPath("//a[contains(@class, 'cart-icon')]");
        private readonly By AllCartItemsnames = By.XPath("//div[contains(@class, 'cart-preview active')]//ul[contains(@class, 'cart-items')]/li[contains(@class, 'cart-item')]/div/p[contains(@class, 'product-name')]");
        private readonly By CartItemPrices = By.XPath("//div[contains(@class, 'cart-preview active')]//ul[contains(@class, 'cart-items')]/li/div/p[contains(@class, 'amount')]");
        private readonly By SearchBox = By.XPath("//input[contains(@class, 'search-keyword')]");
        private readonly By ItemCountOnHomePage = By.CssSelector("table tbody tr:nth-of-type(1) td strong");
        private readonly By ItemPriceCountOnHomePage = By.CssSelector("table tbody tr:nth-of-type(2) td strong");
        private static By SearchedItemNameCard(int index) => By.XPath($"//div[contains(@class, 'product')]/div['{index}']/h4");
        private readonly By TopDealsUrl = By.XPath("//a[@class='cart-header-navlink' and @href='#/offers']");
        private readonly By TopDealsSearchBoc = By.XPath("//input[contains(@id, 'search-field')]");
        private readonly By DiscountItem = By.CssSelector("table.table-bordered tbody tr td:nth-of-type(1)");
        private readonly By DiscountItemPrice = By.CssSelector("table.table-bordered tbody tr td:nth-of-type(3)");
        private readonly By OriginalPrice = By.CssSelector("table.table-bordered tbody tr td:nth-of-type(2)");
        private static By DynamicAddtoCart(string item) => By.XPath($"//div[contains(@class, 'product')]/div/h4[contains(text(), '{item}')]/parent::div/child::div[3]/button");
        private readonly By NumberOfItemsAdded = By.CssSelector("table tbody tr:nth-of-type(1) td:nth-of-type(3)");
        private readonly By ProceedToCheckoutButton = By.XPath("//div[contains(@class, 'cart-preview')]/div/button");
        private readonly By ProductCartTable = By.CssSelector("table#productCartTables");
        private readonly By ItemNamesInProductCart = By.CssSelector("table tbody tr td:nth-of-type(2)");
        private readonly By ItemQuantityInProductCart = By.CssSelector("table tbody tr td:nth-of-type(3)");
        private readonly By ItemPriceInProductCart = By.CssSelector("table tbody tr td:nth-of-type(4)");
        private readonly By ItemIndividualPriceTotalInProductCart = By.CssSelector("table tbody tr td:nth-of-type(5)");
        private readonly By RowCountOfItemsInProductCart = By.CssSelector("table tbody tr");
        private readonly By TotalAmountOnProductPage = By.CssSelector("div div:nth-child(4) span:nth-of-type(1)");
        private readonly By PlaceOrderButton = By.CssSelector("div div:nth-child(4) button:nth-child(14)");
        private readonly By SelectCountryDropDown = By.XPath("//div[contains(@class, 'wrapperTwo')]/div/select");
        private readonly By TermsAndConditionsBox = By.XPath("//div[contains(@class, 'wrapperTwo')]/input");
        private readonly By ProceedButton = By.CssSelector("div.wrapperTwo button");
        private readonly By OrderPlacedSuccessfullyMsg = By.CssSelector("div.wrapperTwo span");
        public bool IsGreenkartLogoDisplayed()
        {
            return Wait.IsElementPresent(GreenCartLogo, 8);
        }

        public void ClickOnAddToCartButton()
        {
            Wait.UntilElementClickable(AddToCartButton).ClickOn();

        }

        public void ClickOnCartIcon()
        {
            Wait.UntilElementClickable(CartIcon).ClickOn();
        }

        public IList<string> GetAllCartItemsNames()
        {           
            return Wait.UntilAllElementsLocated(AllCartItemsnames).Where(e => e.Displayed).Select(x => x.GetText()).ToList();         
        }

        public IList<string> GetRowCountOfItemsInProductCart()
        {
            return Wait.UntilAllElementsLocated(RowCountOfItemsInProductCart).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public void SearchItemName(string itemname)
        {
            Wait.UntilElementClickable(SearchBox).SendKeys(itemname);
          
           
        }

        public string GetSearchedItemName(int index)
        {
            return Wait.UntilElementVisible(SearchedItemNameCard(index)).GetText();
        }

        

        public void ClickOnTopDealsLink()
        {
            Wait.UntilElementClickable(TopDealsUrl).ClickOn();

        }

        public string GetDiscountItemName(string itemname)
        {
            Wait.UntilElementClickable(TopDealsSearchBoc).SendKeys(itemname);
            return Wait.UntilElementVisible(DiscountItem).GetText();
        }

        public string GetDiscountPrice()
        {
            return Wait.UntilElementVisible(DiscountItemPrice).GetText();
        }

        public string GetOriginalPrice()
        {
            return Wait.UntilElementVisible(OriginalPrice).GetText();
        }

        //public void AddMultipleItemsToCart(int index, string item)
        //{
        //    Wait.UntilElementClickable(DynamicAddtoCart(index, item)).ClickOn();
          
        //}

        public string VerifyNumberOfItemsAddedInCart()
        {
            return Wait.UntilElementVisible(NumberOfItemsAdded).GetText();
        }

        public IList<string> GetAllCartItemsPrices()
        {
            return Wait.UntilAllElementsLocated(CartItemPrices).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
       }


        public IList<string> GetItemQuantityInProductCart()
        {
            return Wait.UntilAllElementsLocated(ItemQuantityInProductCart).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public IList<string> GetItemPriceInProductCart()
        {
            return Wait.UntilAllElementsLocated(ItemPriceInProductCart).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public IList<string> GetItemIndividualPriceTotalInProductCart()
        {
            return Wait.UntilAllElementsLocated(ItemIndividualPriceTotalInProductCart).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public IList<string> GetItemNamesInProductCart()
        {
            return Wait.UntilAllElementsLocated(ItemNamesInProductCart).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }




        public GreenkartDataObj SelectVegetableItem(GreenkartDataObj obj)
        {
            
            for (var item = 0; item < obj.ItemName.Count; item++)
            {

                Wait.UntilElementClickable(Itemquantity(obj.ItemName[item])).Clear();
                Wait.UntilElementClickable(Itemquantity(obj.ItemName[item])).SendKeys(obj.ItemQuantity[item]);
                new WaitHelpers(Driver).HardWait(1000);
                Wait.UntilElementClickable(DynamicAddtoCart(obj.ItemName[item])).ClickOn();
                new WaitHelpers(Driver).HardWait(1000);
                
            }

            
            return new GreenkartDataObj
            {
                ItemName = obj.ItemName,
                ItemPrice = obj.ItemPrice,
                ItemQuantity = obj.ItemQuantity
            };

        }

        public string getItemCountOnHomepage()
        {
            return Wait.UntilElementVisible(ItemCountOnHomePage).GetText();
        }

        public string getItemPriceCountOnHomepage()
        {
            return Wait.UntilElementVisible(ItemPriceCountOnHomePage).GetText();
        }

        public void ClickOnProceedToCheckoutButton()
        {
            Wait.UntilElementClickable(ProceedToCheckoutButton).ClickOn();

        }
        
        public bool IsProductCartTableDisplayed()
        {
            return Wait.IsElementPresent(ProductCartTable, 4);
        }

        public string GetTotalAmountOnProductPage()
        {
            return Wait.UntilElementVisible(TotalAmountOnProductPage).GetText();
        }

        public void ClickOnPlaceOrderButton()
        {
            Wait.UntilElementVisible(PlaceOrderButton).ClickOn();
        }

        public void SelectCountryFromDropdown()
        {
            
            Wait.UntilElementVisible(SelectCountryDropDown).SelectDropdownValueByText("Algeria");

        }

        public void SelectTermsAndConditionsBox()
        {
            Wait.UntilElementVisible(TermsAndConditionsBox).ClickOn();
        }

        public void ClickOnProceedButton()
        {
            Wait.UntilElementVisible(ProceedButton).ClickOn();
        }

        public bool GetorderPlacedSuccessfullyMsg()
        {
            return Wait.IsElementPresent(OrderPlacedSuccessfullyMsg, 4);
        }


    }



    }


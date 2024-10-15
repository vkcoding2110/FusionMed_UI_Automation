using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Greenkart;
using System.Linq;
using UIAutomation.Utilities;
using UIAutomation.DataFactory.Greenkart;
using System.Collections;

namespace UIAutomation.Tests.FMP.Greenkart
{
    [TestClass]
    [TestCategory("GreenkartTestCases")]
  public class GreenkartTestCases : FmpBaseTest
    {
        //    private const string Item_1 = "Brocolli - 1 Kg";
        //    private const string Item_2 = "Beans - 1 Kg";
        private const string ExpectedDiscountItem = "Beans";
       
        //    private const int SearchedItemIndex = 1;
        //    private readonly List<string> ItemNames = GetItemNames().Select(x => x.ItemName).ToList();

            [TestMethod]
            [TestCategory("AddToCart")]
            public void VerifyThatItemAddedSuccessfully()
            {

                var GreenkartPo = new GreenkartPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {GreenkartUrl}");
                Driver.NavigateTo(GreenkartUrl);

                Log.Info("Step 2: Verify Greenkart website is opened");
                Assert.IsTrue(GreenkartPo.IsGreenkartLogoDisplayed(), "Greenkart website is not open");

                Log.Info("Step 3: Select 'Brocolli' and click on 'Add to cart' button");
                //var expecteditemname = greenkart.getItemname();
               // var Expecteditemname = GetItemDetails(Item_1);
                //Assert.AreEqual(Expecteditemname.ItemName, Item_1, "This item is not displayed on page");
                GreenkartPo.ClickOnAddToCartButton();
                new WaitHelpers(Driver).HardWait(1000);

                Log.Info("Step 4: Click on 'Cart' Icon and Verify the item is added to the cart");
                GreenkartPo.ClickOnCartIcon();
                new WaitHelpers(Driver).HardWait(1000);
                var CartItemsList = GreenkartPo.GetAllCartItemsNames();

                //Assert.IsTrue(CartItemsList.First().Contains(Item_1), "Item is not added to the cart");

            }

        //    [TestMethod]
        //    [TestCategory("SearchItem")]
        //    public void VerifySearchFunctionalityWorksSuccessfully()
        //    {
        //        var GreenkartPo = new GreenkartPo(Driver);

        //        Log.Info($"Step 1: Navigate to application at: {GreenkartUrl}");
        //        Driver.NavigateTo(GreenkartUrl);

        //        Log.Info("Step 2: Click on search bar and Enter item 'Beans - 1 Kg' and verify correct item is displayed");

        //        GreenkartPo.SearchItemName(Item_2);
        //        new WaitHelpers(Driver).HardWait(2000);
        //        var actualitem = GreenkartPo.GetSearchedItemName(SearchedItemIndex);
        //        Assert.AreEqual(Item_2, actualitem, "Correct item is not displayed");

        //    }

        //[TestMethod]
        //[TestCategory("TopDeal")]
        //public void GetDiscountPriceOfSearchedItem()
        //{
        //    var greenKartPo = new GreenkartPo(Driver);

        //    Log.Info($"Step 1: Navigate to application at: {GreenkartUrl}");
        //    Driver.NavigateTo(GreenkartUrl);

        //    Log.Info("Step 2: Click on 'Top Deals' link and verify that offers page is opened ");
        //    greenKartPo.ClickOnTopDealsLink();
        //    new WaitHelpers(Driver).HardWait(1000);
        //    Driver.SelectWindowByIndex(1);
        //    new WaitHelpers(Driver).HardWait(1000);

        //    Log.Info("Step 3: Search for item 'Beans' and compare original price and discount price");
        //    var actualDiscountItemName = greenKartPo.GetDiscountItemName(ExpectedDiscountItem);
        //    var discountPrice = greenKartPo.GetDiscountPrice();
        //    var originalPrice = greenKartPo.GetOriginalPrice();
        //    Assert.AreEqual(ExpectedDiscountItem, actualDiscountItemName, "Searched item is displayed correctly");
        //    Assert.IsTrue(Convert.ToInt32(discountPrice) < Convert.ToInt32(originalPrice), "Discount price and Original price should not be same");



        //}

        //[TestMethod]
        //[TestCategory("AddMultipleItem")]

        //public void AddMultipleItemsInCart()
        //{

        //    var GreenkartPo = new GreenkartPo(Driver);
        //    Log.Info($"Step 1: Navigate to application at: {GreenkartUrl}");
        //    Driver.NavigateTo(GreenkartUrl);

        //    Log.Info("Step 2: Add multiple items to cart and verify multiple items are added to cart");

        //    for (int i = 0; i < ItemNames.Count; i++)
        //    {
        //        GreenkartPo.AddMultipleItemsToCart(i, ItemNames[i]);
        //        new WaitHelpers(Driver).HardWait(1000);
        //    }
        //    var ItemsToBeAddedCount = ItemNames.Count().ToString();
        //var ItemCountInCart = GreenkartPo.itemsAddedToCart().ToString();
        //    Assert.AreEqual(ItemsToBeAddedCount, ItemCountInCart, "Items are not added successfully in cart");

        //}

        [TestMethod]
        [TestCategory("OrderItemsSuccessfully")]

        public void OrderItemsSuccessfully()
        {
            var greenKartPo = new GreenkartPo(Driver);
            Log.Info($"Step 1: Navigate to application at: {GreenkartUrl}");
            Driver.NavigateTo(GreenkartUrl);

            Log.Info("Step 2: Add 2 items to the cart, change quantity for both item and click on add to cart, then verify item and price value on homepage");

            var addItemToCart = GreenkartDataFactory.AddVegetableItemToCart();
            var itemsAddedToCart = greenKartPo.SelectVegetableItem(addItemToCart);
            
            Log.Info("Step 3: Verify item and price count on homepage and verify item names in cart");
            var actualItemCountOnHomePage = greenKartPo.getItemCountOnHomepage();
            var expectedItemCountOnHomePage = itemsAddedToCart.ItemName.Count();
            Assert.AreEqual(expectedItemCountOnHomePage, Convert.ToInt32(actualItemCountOnHomePage), "Item count on home page do not match");

            int firstItemTotal = Convert.ToInt32(itemsAddedToCart.ItemPrice[0]) * (Convert.ToInt32(itemsAddedToCart.ItemQuantity[0]));
            int secondItemTotal = Convert.ToInt32(itemsAddedToCart.ItemPrice[1]) * (Convert.ToInt32(itemsAddedToCart.ItemQuantity[1]));
            var arrList = new ArrayList();
            arrList.Add(firstItemTotal);
            arrList.Add(secondItemTotal);
            var individualProductTotalPrice = arrList;
           
            int expectedPriceCountOnHomePage = (Convert.ToInt32(itemsAddedToCart.ItemPrice[0]) * (Convert.ToInt32(itemsAddedToCart.ItemQuantity[0]))) + (Convert.ToInt32(itemsAddedToCart.ItemPrice[1]) * Convert.ToInt32(itemsAddedToCart.ItemQuantity[1]));
            int actulPriceCountOnHomePage = Convert.ToInt32(greenKartPo.getItemPriceCountOnHomepage());
            Assert.AreEqual(expectedPriceCountOnHomePage, actulPriceCountOnHomePage, "Sum of Item prices do not match");

            greenKartPo.ClickOnCartIcon();
            new WaitHelpers(Driver).HardWait(1000);
            var cartItems = greenKartPo.GetAllCartItemsNames();
          
            for (int i = 0; i < cartItems.Count; i++) {
                Console.WriteLine("Items in cart are: " +cartItems[i]);
        }

            Log.Info("Step 4: Click on 'Proceed to checkout' button and verify cart window is opened ");
            greenKartPo.ClickOnProceedToCheckoutButton();

            Assert.IsTrue(greenKartPo.IsProductCartTableDisplayed(), "cart window is not opened");

            Log.Info("Step 5: Verify item name, quantity, price, and total in product table");
            var itemNamesInProductCart = greenKartPo.GetItemNamesInProductCart();
            var rowCountOfItemsInProductCart = greenKartPo.GetRowCountOfItemsInProductCart();
            var itemQuantityInProductCart = greenKartPo.GetItemQuantityInProductCart();
            var itemPriceInProductCart = greenKartPo.GetItemPriceInProductCart();
            var itemIndividualPriceTotalInProductCart = greenKartPo.GetItemIndividualPriceTotalInProductCart();
            new WaitHelpers(Driver).HardWait(1000);
            for (int i = 0; i < rowCountOfItemsInProductCart.Count; i++)
            {
                Assert.AreEqual(itemNamesInProductCart[i], itemsAddedToCart.ItemName[i], "Item name added to product cart did not match");
                Assert.AreEqual(itemQuantityInProductCart[i], itemsAddedToCart.ItemQuantity[i], "Item Quantity in product cart did not match");
                Assert.AreEqual(itemPriceInProductCart[i], itemsAddedToCart.ItemPrice[i], "Item Price in product cart did not match");
                Assert.AreEqual(Convert.ToInt32(itemIndividualPriceTotalInProductCart[i]), individualProductTotalPrice[i], "Item individual price total in product cart did not match");
            }

            Log.Info("Step 6: Verify Total amount on Product cart page and click on 'Place Order' button");
            var totalAmountOnProductPage = greenKartPo.GetTotalAmountOnProductPage();
            Assert.AreEqual(expectedPriceCountOnHomePage, Convert.ToInt32(totalAmountOnProductPage), "Total price did not match in Product cart");

            greenKartPo.ClickOnPlaceOrderButton();

            Log.Info("Step 7: Select country 'Algeria' from dropdown, Agree to terms and conditions");
            greenKartPo.SelectCountryFromDropdown();
            greenKartPo.SelectTermsAndConditionsBox();

            Log.Info("Step 8: Click on Proceed button and verify order has been placed successfully");
            greenKartPo.ClickOnProceedButton();
            greenKartPo.GetorderPlacedSuccessfullyMsg();
            Assert.IsTrue(greenKartPo.GetorderPlacedSuccessfullyMsg(), "Order is not placed successfully");

        }

    }
        
   }


using System.Collections.Generic;
using UIAutomation.DataObjects.FMP.GreenkartDataObj;

namespace UIAutomation.DataFactory.Greenkart
{
    public static class GreenkartDataFactory
    {

        public static GreenkartDataObj AddVegetableItemToCart()
        {
            return new GreenkartDataObj
            {
                ItemName = new List<string> { "Brocolli - 1 Kg", "Cauliflower - 1 Kg" },
                ItemPrice = new List<string> { "120", "60" },
                ItemQuantity = new List<string> { "10", "20" }
            };
        }

    }
}

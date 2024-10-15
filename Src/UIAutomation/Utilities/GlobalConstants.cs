using System.Collections.Generic;
using UIAutomation.Tests;

namespace UIAutomation.Utilities
{
    public class GlobalConstants
    {
        //Email Domain
        public string YopMailInbox = "http://www.yopmail.com/en/";
        public static string SenderEmailText = $"Fusion Marketplace <noreply@{BaseTest.Env}.fusionmarketplace.com>";
        public static string FusionMarketPlaceProductionUrl = "https://fusionmarketplace.com";
        public static string FusionMedStaffUrl = "https://www.fusionmedstaff.com/";

        //State Names with Alias
        public static readonly Dictionary<string, string> StateListWithAliasName = new Dictionary<string, string>
        {
            {"AL","Alabama"},
            {"AK", "Alaska"},
            {"AZ", "Arizona"},
            {"AR", "Arkansas"},
            {"CA", "California"},
            {"CO", "Colorado"},
            {"CT", "Connecticut"},
            {"DE", "Delaware"},
            {"DC", "District Of Columbia"},
            {"FL", "Florida"},
            {"GA", "Georgia"},
            {"HI", "Hawaii"},
            {"ID", "Idaho"},
            {"IL", "Illinois"},
            {"IN", "Indiana"},
            {"IA", "Iowa"},
            {"KS", "Kansas"},
            {"KY", "Kentucky"},
            {"LA", "Louisiana"},
            {"ME", "Maine"},
            {"MD", "Maryland"},
            {"MA", "Massachusetts"},
            {"MI", "Michigan"},
            {"MN", "Minnesota"},
            {"MS", "Mississippi"},
            {"MO", "Missouri"},
            {"MT", "Montana"},
            {"NE", "Nebraska"},
            {"NV", "Nevada"},
            {"NH", "New Hampshire"},
            {"NJ", "New Jersey"},
            {"NM", "New Mexico"},
            {"NY", "New York"},
            {"NC", "North Carolina"},
            {"ND", "North Dakota"},
            {"OH", "Ohio"},
            {"OK", "Oklahoma"},
            {"OR", "Oregon"},
            {"PA", "Pennsylvania"},
            {"RI", "Rhode Island"},
            {"SC", "South Carolina"},
            {"SD", "South Dakota"},
            {"TN", "Tennessee"},
            {"TX", "Texas"},
            {"UT", "Utah"},
            {"VT", "Vermont"},
            {"VA", "Virginia"},
            {"WA", "Washington"},
            {"WV", "West Virginia"},
            {"WI", "Wisconsin"},
            {"WY", "Wyoming"}
        };
    }
}
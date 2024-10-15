using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Agencies;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.DataObjects.FMP.Jobs.JobDetails;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.DataObjects.FMP.GreenkartDataObj;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP
{
    public class FmpBaseTest : BaseTest
    {
        public static readonly JObject UsersJsonObj = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMP/Jsons/users.json"));
        private static readonly JObject JobsJsonObj = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMP/Jsons/jobs.json"));
        private static readonly JObject AgencyJsonObj = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMP/Jsons/Agency.json"));
        private static readonly JObject RecruiterJsonObj = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMP/Jsons/Recruiters.json"));
        private static readonly JObject JobApplicationJsonObj = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMP/Jsons/JobApplication.json"));
        private static readonly JObject GreenkartJsonObject = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMP/Jsons/Greenkart.json"));

        protected static Login GetLoginUsersByType(string userType)
        {
            return UsersJsonObj["users"].First(user => user["type"].ToString() == userType).ToObject<Login>();
        }
        protected static Login GetLoginUsersByTypeAndPlatform(string userType)
        {
            return UsersJsonObj["users"].Where(user => user["type"].ToString().ToLowerInvariant() == userType.ToLowerInvariant()).First(user => user["platform"].ToString().ToLowerInvariant() == PlatformName.ToString().ToLowerInvariant()).ToObject<Login>();
        }

        protected static Profile GetProfileUsersByType(string userType)
        {
            return UsersJsonObj["users"].First(user => user["type"].ToString().ToLowerInvariant() == userType.ToLowerInvariant()).ToObject<Profile>();
        }

        protected static string GetJobUrlByStatus(string environment, string jobsType)
        {
            return JobsJsonObj.SelectToken($"{jobsType}.{environment}.url").Value<string>();
        }
        protected static Agency GetAgencyByName(string aName)
        {
            return AgencyJsonObj["agencies"].First(name => name["Name"].ToString().ToLowerInvariant() == aName.ToLowerInvariant()).ToObject<Agency>();
        }
        protected static List<UserTypeCards> GetCardListByUserType(string userType)
        {
            return JobApplicationJsonObj[userType].ToObject<List<UserTypeCards>>();
        }

        protected static List<Agency> GetAgencyByNames()
        {
            return AgencyJsonObj["agencies"].ToObject<List<Agency>>();
        }

        protected static Recruiter GetRecruitersByName(string rName)
        {
            return RecruiterJsonObj["recruiters"].First(name => name["RecruiterName"].ToString() == rName).ToObject<Recruiter>();
        }

        protected static GreenkartDataObj GetItemDetails(string itemname)
        {
            return GreenkartJsonObject["Items"].First(name => name["ItemName"].ToString() == itemname).ToObject<GreenkartDataObj>();
        }

        protected static List<GreenkartDataObj> GetItemNames()
        {
            return GreenkartJsonObject["Items"].ToObject<List<GreenkartDataObj>>();
        }

        protected static GreenkartDataObj GetItemPrices(string price)
        {
            return GreenkartJsonObject["Items"].First(name => name["ItemPrice"].ToString() == price).ToObject<GreenkartDataObj>();
        }



    }
}

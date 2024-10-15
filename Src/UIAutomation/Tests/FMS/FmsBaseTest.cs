using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS
{
    public class FmsBaseTest : BaseTest
    {
        public static readonly JObject UsersJsonObj = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMS/Jsons/users.json"));
        private static readonly JObject JobsJsonObj = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMS/Jsons/jobs.json"));
        protected static Login GetLoginUsersByType(string userType)
        {
            return UsersJsonObj["users"].First(user => user["type"].ToString() == userType).ToObject<Login>();
        }

        protected static string GetJobsStatus(string environment, string jobsType)
        {
            return JobsJsonObj.SelectToken($"{jobsType}.{environment}.id").Value<string>();
        }

    }
}
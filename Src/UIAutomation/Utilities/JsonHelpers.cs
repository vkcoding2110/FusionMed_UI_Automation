using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Text;

namespace UIAutomation.Utilities
{
    public class JsonHelpers<TDto>
    {
        public StringContent ObjectToJsonString(object dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public TDto DeserializeJsonObject(string jsonFilePath)
        {
            return JsonConvert.DeserializeObject<TDto>(JObject.Parse(File.ReadAllText(jsonFilePath)).ToString());
        }

        public TDto DeserializeJsonObject(string jsonFilePath, string matchingNode)
        {
            return JsonConvert.DeserializeObject<TDto>(JObject.Parse(File.ReadAllText(jsonFilePath))[matchingNode].ToString());
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web;
using System.Web.Mvc;

namespace MachineLearner.Helpers
{
    public static class JsonHelpers
    {
        public static IHtmlString ToJson<T>(this T obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return MvcHtmlString.Create(JsonConvert.SerializeObject(obj, settings));
        }
    }
}
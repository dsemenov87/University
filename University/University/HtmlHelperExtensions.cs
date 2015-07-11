using System;
using Newtonsoft.Json;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        private static readonly JsonSerializerSettings settings;

        static HtmlHelperExtensions()
        {
            settings = new JsonSerializerSettings();
            // CamelCase: "MyProperty" will become "myProperty"
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }

        public static MvcHtmlString ToJSON(this HtmlHelper html, object value)
        {
            return MvcHtmlString.Create(JsonConvert.SerializeObject(value, Formatting.None, settings));
        }
    }
}

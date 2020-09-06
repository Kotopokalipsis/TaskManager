using System.Web.Http;
using Web.Filters;

namespace Web.Configuration
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ValidateModelAttribute());
            
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling 
                = Newtonsoft.Json.ReferenceLoopHandling.Serialize;     
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling 
                = Newtonsoft.Json.PreserveReferencesHandling.Objects;
        }
    }
}
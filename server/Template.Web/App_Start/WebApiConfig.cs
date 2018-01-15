using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Template.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Template.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Add CORS support iff CorsOrigin is specified in an appSetting in web.config.
            // We use this instead of <customHeaders> because it allows multiple specific origins
            // and only adds the Access-Control headers when needed.
            // Multiple origins should be comma-separated.
            var origin = ConfigUtil.GetAppSetting("CorsOrigin", "");
            if (!string.IsNullOrWhiteSpace(origin))
            {
                // SupportsCredentials means allowing cross-domain cookies.  It doesn't work with wildcard origins.
                config.EnableCors(new EnableCorsAttribute(origin, "*", "*") { SupportsCredentials = true });
            }

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
                //defaults: new { id = RouteParameter.Optional }
            );

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            var jset = json.SerializerSettings;
#if DEBUG
            jset.Formatting = Newtonsoft.Json.Formatting.Indented;
#endif
            jset.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jset.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            jset.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        }
    }
}

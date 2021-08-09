using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiContrib.Formatting.Jsonp;

namespace WebApiEmployee
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //for json indent
            // config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;


            //for jsonp
           // var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
           // config.Formatters.Insert(0, jsonpFormatter);
        
        }
    }
}

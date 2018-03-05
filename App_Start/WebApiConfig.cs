using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Mobil_Ogrenci_Yoklama_Uygulamasi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
             //config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "yoklama_bilgis",
                routeTemplate: "api/{controller}/{action}/{ders_id}/{ogrenci_id}",
                defaults: new { ders_id = RouteParameter.Optional, ogrenci_id = RouteParameter.Optional }
            );

        }
    }
}

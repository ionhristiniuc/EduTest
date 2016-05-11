using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Tracing;
using EduTestService.Core.Converters;
using EduTestService.Core.Logging;

namespace EduTestService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            log4net.Config.XmlConfigurator.Configure();
            config.Services.Replace(typeof(ITraceWriter), new FileLogger());
            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new PolymorphicQuestionConverter());
            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "AuthRoute",
            //    routeTemplate: "token"
            //    );
        }
    }
}

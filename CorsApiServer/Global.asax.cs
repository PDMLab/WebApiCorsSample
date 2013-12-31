using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using CorsApiServer.MessageHandlers;

namespace CorsApiServer
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
           WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.MessageHandlers.Add(new TokenAuthenticationHandler());

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api

        }
    }
}
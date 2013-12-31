using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CorsOwinServer.MessageHandlers;
using Microsoft.Owin.Hosting;
using Owin;

namespace CorsOwinServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var disposable = WebApp.Start<Startup>("http://localhost:12345");
            Console.ReadLine();
                disposable.Dispose();
            
        }
    }

    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableCors();
            config.MessageHandlers.Add(new TokenAuthenticationHandler());

            app.UseWebApi(config);

        }

    }
}

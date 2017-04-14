using System.Data.Entity;
using AnyPointDemo.Services;
using RamlDemo;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace AnyPointDemo
{
	public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			Database.SetInitializer(new DatabaseInitializer());
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			UnityConfig.RegisterTypes(IocContainer.Instance);
	        AutomapperConfig.RegisterTypes();
		}
	}
}

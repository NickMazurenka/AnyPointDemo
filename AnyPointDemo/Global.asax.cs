// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   The web api application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AnyPointDemo.Services;

    /// <summary>
    /// The web API application.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterTypes(IocContainer.Instance);
            AutomapperConfig.RegisterTypes();
        }
    }
}

﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   The route config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The route config.
    /// </summary>
    public static class RouteConfig
    {
        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Raml", action = "Index", id = UrlParameter.Optional });
        }
    }
}

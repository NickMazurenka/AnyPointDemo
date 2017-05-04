// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   Defines the WebApiConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo
{
    using System.Web.Http;

    /// <summary>
    /// The web API config.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public static void Register(HttpConfiguration config)
        {
			RAML.WebApiExplorer.DocumentationProviderConfig.IncludeXmlComments();
			
			
			

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RamlController.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   Defines the RamlController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo.Controllers
{
    using System.Text;
    using System.Web.Http;
    using System.Web.Mvc;

    using Raml.Parser.Expressions;

    using RAML.WebApiExplorer;

    /// <summary>
    /// The RAML controller.
    /// </summary>
    public class RamlController : Controller
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// The raw.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Raw(string version = "1")
        {
            var ramlContents = GetRamlContents(version);
            return this.File(Encoding.UTF8.GetBytes(ramlContents), "text/raml");
        }

        /// <summary>
        /// The download.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Download(string version = "1")
        {
            var ramlContents = GetRamlContents(version);
            return this.File(Encoding.UTF8.GetBytes(ramlContents), "text/raml", "generated.raml");
        }


        /// <summary>
        /// The OAUTH 1.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult OAuth1()
        {
            return this.View();
        }

        /// <summary>
        /// The OAUTH 2.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult OAuth2()
        {
            return this.View();
        }

        /// <summary>
        /// The get RAML contents.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetRamlContents(string version)
        {
            var config = GlobalConfiguration.Configuration;
            var apiExplorer = config.Services.GetApiExplorer();
            var ramlVersion = version == "0.8" ? RamlVersion.Version08 : RamlVersion.Version1;
            ApiExplorerService apiExplorerService;

            if (ramlVersion == RamlVersion.Version1)
            {
                apiExplorerService = new ApiExplorerServiceVersion1(apiExplorer, config.VirtualPathRoot);
            }
            else
            {
                apiExplorerService = new ApiExplorerServiceVersion08(apiExplorer, config.VirtualPathRoot);
            }

            var ramlDocument = apiExplorerService.GetRaml(ramlVersion);
            var ramlContents = new RamlSerializer().Serialize(ramlDocument);
            return ramlContents;
        }
    }
}
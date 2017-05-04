// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentationProviderConfig.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   Defines the DocumentationProviderConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo
{
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Description;

    using RAML.WebApiExplorer;

    /// <summary>
    /// The documentation provider config.
    /// </summary>
    public static class DocumentationProviderConfig
    {
        /// <summary>
        /// The include xml comments.
        /// </summary>
        /// <param name="xmlCommentsPath">
        /// The xml comments path.
        /// </param>
        public static void IncludeXmlComments(string xmlCommentsPath = null)
        {
            var config = GlobalConfiguration.Configuration;
            if (string.IsNullOrWhiteSpace(xmlCommentsPath))
            {
                xmlCommentsPath = GetXmlCommentsPath();
            }

            config.Services.Replace(
                typeof(IDocumentationProvider),
                new XmlCommentDocumentationProvider(xmlCommentsPath));
        }

        /// <summary>
        /// The get xml comments path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetXmlCommentsPath()
        {
            return $@"{HttpContext.Current.Server.MapPath("")}\bin\{Assembly.GetCallingAssembly().GetName().Name}.xml";
        } 
    }
}
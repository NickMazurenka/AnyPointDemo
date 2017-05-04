// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityConfig.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   Defines the UnityConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo
{
    using AnyPointDemo.Services;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// The unity config.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// The register types.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public static void RegisterTypes(IUnityContainer container)
        {
            // Win App Driver
            container.RegisterType<ICityInfoRepository, CityInfoFakeRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(_ => new CityInfoFakeRepository()));
        }
    }
}
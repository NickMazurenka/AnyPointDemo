// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomapperConfig.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   The AutoMapper config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo
{
    using AnyPointDemo.Entities;
    using AnyPointDemo.Models;

    using AutoMapper;

    /// <summary>
    /// The AutoMapper config.
    /// </summary>
    public static class AutomapperConfig
    {
        /// <summary>
        /// The register types.
        /// </summary>
        public static void RegisterTypes()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<City, CityWithoutPointsOfInterestDto>();
                cfg.CreateMap<City, CityDto>();
                cfg.CreateMap<PointOfInterest, PointOfInterestDto>();
                cfg.CreateMap<PointOfInterestForCreationDto, PointOfInterest>();
                cfg.CreateMap<PointOfInterestForUpdateDto, PointOfInterest>();
                cfg.CreateMap<PointOfInterest, PointOfInterestForUpdateDto>();
            });
        }
    }
}

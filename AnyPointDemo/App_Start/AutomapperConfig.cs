using AnyPointDemo.Entities;
using AnyPointDemo.Models;

namespace AnyPointDemo
{
	public static class AutomapperConfig
	{
		public static void RegisterTypes()
		{
			AutoMapper.Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<City, CityWithoutPointsOfInterestDto>().ReverseMap();
				cfg.CreateMap<City, CityDto>();
				cfg.CreateMap<PointOfInterest, PointOfInterestDto>();
				cfg.CreateMap<PointOfInterestForCreationDto, PointOfInterest>();
				cfg.CreateMap<PointOfInterestForUpdateDto, PointOfInterest>();
				cfg.CreateMap<PointOfInterest, PointOfInterestForUpdateDto>();
			});
		}
	}
}

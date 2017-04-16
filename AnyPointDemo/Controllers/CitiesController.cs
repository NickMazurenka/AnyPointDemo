using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AnyPointDemo.Models;
using AnyPointDemo.Services;
using AutoMapper;
using Microsoft.Practices.Unity;

namespace AnyPointDemo.Controllers
{
    /// <summary>
    /// Cities controller
    /// </summary>
    [RoutePrefix("api")]
    public class CitiesController : ApiController
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        /// <summary>
        /// Cities controller constructor
        /// </summary>
        public CitiesController()
        {
            _cityInfoRepository = IocContainer.Instance.Resolve<ICityInfoRepository>();
        }

        /// <summary>
        /// Gets list of cities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Route("cities")]
		[ResponseType(typeof(IEnumerable<CityWithoutPointsOfInterestDto>))]
        public IHttpActionResult GetCities()
        {
	        return Ok(Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(_cityInfoRepository.GetCities()));
        }

        /// <summary>
        /// Get city by cityId
        /// </summary>
        /// <remarks>
        /// Optionally lists corresponding points of interest
        /// </remarks>
        /// <param name="cityId">Id of city to show</param>
        /// <param name="includePointsOfInterest">Flag to include list of points of interest</param>
        /// <returns></returns>
        [HttpGet]
		[Route("cities/{cityId}")]
		[ResponseType(typeof(CityDto))]
        public IHttpActionResult GetCity(int cityId, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(cityId, includePointsOfInterest);
            if (city == null) return NotFound();

	        if (includePointsOfInterest)
		        return Ok(Mapper.Map<CityDto>(city));
	        else
		        return Ok(Mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }

    }
}

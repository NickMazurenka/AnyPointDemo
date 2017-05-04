// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CitiesController.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   Cities controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.Http.Results;

    using AnyPointDemo.Models;
    using AnyPointDemo.Services;

    using AutoMapper;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Cities controller
    /// </summary>
    [RoutePrefix("api")]
    public class CitiesController : ApiController
    {
        /// <summary>
        /// The city info repository.
        /// </summary>
        private readonly ICityInfoRepository cityInfoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CitiesController"/> class. 
        /// </summary>
        public CitiesController()
        {
            this.cityInfoRepository = IocContainer.Instance.Resolve<ICityInfoRepository>();
        }

        /// <summary>
        /// Get cities without points of interest.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("cities")]
        [ResponseType(typeof(IEnumerable<CityWithoutPointsOfInterestDto>))]
        public IHttpActionResult GetCities()
        {
            return this.Ok(Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(this.cityInfoRepository.GetCities()));
        }

        /// <summary>
        /// Get city with or without point of interest.
        /// </summary>
        /// <param name="cityId">
        /// Id of city.
        /// </param>
        /// <param name="includePointsOfInterest">
        /// Flag to include points of interest.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("cities/{cityId}")]
        [ResponseType(typeof(CityDto))]
        public IHttpActionResult GetCity(int cityId, bool includePointsOfInterest = false)
        {
            var city = this.cityInfoRepository.GetCity(cityId, includePointsOfInterest);
            if (city == null)
            {
                return this.NotFound();
            }

            if (includePointsOfInterest)
            {
                return this.Ok(Mapper.Map<CityDto>(city));
            }

            return this.Ok(Mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }
}

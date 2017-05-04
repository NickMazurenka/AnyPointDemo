// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointsOfInterestController.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   Points of interest controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;

    using AnyPointDemo.Entities;
    using AnyPointDemo.Models;
    using AnyPointDemo.Services;

    using AutoMapper;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Points of interest controller
    /// </summary>
    [RoutePrefix("api")]
    public class PointsOfInterestController : ApiController
    {
        /// <summary>
        /// The city info repository.
        /// </summary>
        private readonly ICityInfoRepository cityInfoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointsOfInterestController"/> class.
        /// </summary>
        public PointsOfInterestController()
        {
            this.cityInfoRepository = this.cityInfoRepository = IocContainer.Instance.Resolve<ICityInfoRepository>();
        }

        /// <summary>
        /// Get all points of interest for specified city.
        /// </summary>
        /// <param name="cityId">
        /// Id of city.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("cities/{cityId}/pointsofinterest")]
        [ResponseType(typeof(IEnumerable<PointOfInterestDto>))]
        public IHttpActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                if (!this.cityInfoRepository.CityExists(cityId))
                {
                    return this.NotFound();
                }

                return
                    this.Ok(
                        Mapper.Map<IEnumerable<PointOfInterestDto>>(
                            this.cityInfoRepository.GetPointsOfInterestForCity(cityId)));
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        }

        /// <summary>
        /// Get point of interest for specified city
        /// </summary>
        /// <param name="cityId">
        /// Id of city.
        /// </param>
        /// <param name="poiId">
        /// Id of point of interest.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("cities/{cityId}/pointsofinterest/{poiId}", Name = "GetPointOfInterest")]
        [ResponseType(typeof(PointOfInterestDto))]
        public IHttpActionResult GetPointOfInterest(int cityId, int poiId)
        {
            if (!this.cityInfoRepository.CityExists(cityId))
            {
                return this.NotFound();
            }

            var pointOfInterestFromDb = this.cityInfoRepository.GetPointOfInterestForCity(cityId, poiId);
            if (pointOfInterestFromDb == null)
            {
                return this.NotFound();
            }

            return this.Ok(Mapper.Map<PointOfInterestDto>(pointOfInterestFromDb));
        }

        /// <summary>
        /// Create point of interest for specified city
        /// </summary>
        /// <param name="cityId">
        /// Id of city.
        /// </param>
        /// <param name="pointOfInterest">
        /// Point of interest to create.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpPost]
        [Route("cities/{cityId}/pointsofinterest")]
        [ResponseType(typeof(PointOfInterestDto))]
        public IHttpActionResult CreatePointOfInterest(
            int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return this.BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                this.ModelState.AddModelError("Description", "Description should not be equal to name");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.cityInfoRepository.CityExists(cityId))
            {
                return this.NotFound();
            }

            var pointToCreate = Mapper.Map<PointOfInterest>(pointOfInterest);
            this.cityInfoRepository.AddPointOfInterestForCity(cityId, pointToCreate);

            if (!this.cityInfoRepository.Save())
            {
                return this.InternalServerError(new Exception("Failed to save changes into database"));
            }

            var createdPointOfInterest = Mapper.Map<PointOfInterestDto>(pointToCreate);

            return this.Created(this.Request.RequestUri + "/" + createdPointOfInterest.Id, createdPointOfInterest);
        }

        /// <summary>
        /// Update point of interest for specified city.
        /// </summary>
        /// <param name="cityId">
        /// Id of city.
        /// </param>
        /// <param name="poiId">
        /// Id of point of interest.
        /// </param>
        /// <param name="pointOfInterest">
        /// Resulting point of interest.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpPut]
        [Route("cities/{cityId}/pointsofinterest/{poiId}")]
        [ResponseType(typeof(HttpStatusCode))]
        public IHttpActionResult UpdatePointOfInterest(
            int cityId,
            int poiId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return this.BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                this.ModelState.AddModelError("Description", "Description should not be equal to name");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.cityInfoRepository.CityExists(cityId))
            {
                return this.NotFound();
            }

            var pointToUpdate = this.cityInfoRepository.GetPointOfInterestForCity(cityId, poiId);
            if (pointToUpdate == null)
            {
                return this.NotFound();
            }

            Mapper.Map(pointOfInterest, pointToUpdate);

            if (!this.cityInfoRepository.Save())
            {
                return this.InternalServerError(new Exception("Failed to save changes into database"));
            }

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete point of interest for specified city.
        /// </summary>
        /// <param name="cityId">
        /// Id of city.
        /// </param>
        /// <param name="poiId">
        /// Id of point of interest.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpDelete]
        [Route("cities/{cityId}/pointsofinterest/{poiId}")]
        [ResponseType(typeof(HttpStatusCode))]
        public IHttpActionResult DeletePointOfInterest(int cityId, int poiId)
        {
            if (!this.cityInfoRepository.CityExists(cityId))
            {
                return this.NotFound();
            }

            var pointEntity = this.cityInfoRepository.GetPointOfInterestForCity(cityId, poiId);
            if (pointEntity == null)
            {
                return this.NotFound();
            }

            this.cityInfoRepository.DeletePointOfInterest(pointEntity);
            if (!this.cityInfoRepository.Save())
            {
                return this.InternalServerError(new Exception("Failed to save changes into database"));
            }

            return this.StatusCode(HttpStatusCode.NoContent);
        }
    }
}

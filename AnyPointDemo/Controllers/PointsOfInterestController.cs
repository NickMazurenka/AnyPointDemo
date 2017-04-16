﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using AnyPointDemo.Entities;
using AnyPointDemo.Models;
using AnyPointDemo.Services;
using Microsoft.Practices.Unity;
using AutoMapper;

namespace AnyPointDemo.Controllers
{
    /// <summary>
    /// Points of interest controller
    /// </summary>
    [RoutePrefix("api")]
    public class PointsOfInterestController : ApiController
    {
        private ICityInfoRepository _cityInfoRepository;

        /// <summary>
        /// Points of interest controller constructor
        /// </summary>
        public PointsOfInterestController()
        {
            _cityInfoRepository = _cityInfoRepository = IocContainer.Instance.Resolve<ICityInfoRepository>();
        }

        /// <summary>
        /// Get all points of interest for city with specified id
        /// </summary>
        /// <param name="cityId">Id of city to show points of interest</param>
        /// <returns></returns>
        [HttpGet]
        [Route("cities/{cityId}/pointsofinterest")]
        [ResponseType(typeof(IEnumerable<PointOfInterestDto>))]
        public IHttpActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    StatusCode(HttpStatusCode.NotFound);
                }

                return Ok(Mapper.Map<IEnumerable<PointOfInterestDto>>(_cityInfoRepository.GetPointsOfInterestForCity(cityId)));
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

        }

        /// <summary>
        /// Get point of interest by poiId corresponding to city with specified id
        /// </summary>
        /// <param name="cityId">Id of city</param>
        /// <param name="poiId">Id of point of interest</param>
        /// <returns></returns>
        [HttpGet]
        [Route("cities/{cityId}/pointsofinterest/{poiId}", Name = "GetPointOfInterest")]
        [ResponseType(typeof(PointOfInterestDto))]
        public IHttpActionResult GetPointOfInterest(int cityId, int poiId)
        {
            if (!_cityInfoRepository.CityExists(cityId)) StatusCode(HttpStatusCode.NotFound);

            var pointOfInterestFromDb = _cityInfoRepository.GetPointOfInterestForCity(cityId, poiId);
            if (pointOfInterestFromDb == null) StatusCode(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<PointOfInterestDto>(pointOfInterestFromDb));
        }

        /// <summary>
        /// Create point of interest for city with specified id
        /// </summary>
        /// <param name="cityId">Id of city</param>
        /// <param name="pointOfInterest">Point of interest to create</param>
        /// <returns></returns>
        [HttpPost]
        [Route("cities/{cityId}/pointsofinterest")]
        [ResponseType(typeof(PointOfInterestDto))]
        public IHttpActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();

            if (pointOfInterest.Description == pointOfInterest.Name)
                ModelState.AddModelError("Description", "Description should not be equal to name");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_cityInfoRepository.CityExists(cityId)) StatusCode(HttpStatusCode.NotFound);

            var pointToCreate = Mapper.Map<PointOfInterest>(pointOfInterest);
            _cityInfoRepository.AddPointOfInterestForCity(cityId, pointToCreate);

            if (!_cityInfoRepository.Save()) return StatusCode(HttpStatusCode.InternalServerError);

            var createdPointOfInterest = Mapper.Map<PointOfInterestDto>(pointToCreate);

            return Created(Request.RequestUri + "/" + createdPointOfInterest.Id, createdPointOfInterest);
        }

        /// <summary>
        /// Update existing point of interest for city with specified id
        /// </summary>
        /// <param name="cityId">Id of city</param>
        /// <param name="id">Id of point of interest to update</param>
        /// <param name="pointOfInterest">Point of interest to create</param>
        /// <returns></returns>
        [HttpPut]
        [Route("cities/{cityId}/pointsofinterest/{poiId}")]
        [ResponseType(typeof(IHttpActionResult))]
        public IHttpActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();

            if (pointOfInterest.Description == pointOfInterest.Name)
                ModelState.AddModelError("Description", "Description should not be equal to name");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_cityInfoRepository.CityExists(cityId)) StatusCode(HttpStatusCode.NotFound);

            var pointToUpdate = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointToUpdate == null) StatusCode(HttpStatusCode.NotFound);

            Mapper.Map(pointOfInterest, pointToUpdate);

            if (!_cityInfoRepository.Save()) return StatusCode(HttpStatusCode.InternalServerError);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete point of interest for city with specified id
        /// </summary>
        /// <param name="cityId">Id of city</param>
        /// <param name="poiId">Point of interest to update</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("cities/{cityId}/pointsofinterest/{poiId}")]
        [ResponseType(typeof(StatusCodeResult))]
        public IHttpActionResult DeletePointOfAction(int cityId, int poiId)
        {
            if (!_cityInfoRepository.CityExists(cityId)) return StatusCode(HttpStatusCode.NotFound);

            var pointEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, poiId);
            if (pointEntity == null) return StatusCode(HttpStatusCode.NotFound);

            _cityInfoRepository.DeletePointOfInterest(pointEntity);
            if (!_cityInfoRepository.Save()) return StatusCode(HttpStatusCode.InternalServerError);

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
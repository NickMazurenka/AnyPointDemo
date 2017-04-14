using System.Collections.Generic;
using AnyPointDemo.Entities;

namespace AnyPointDemo.Services
{
    public interface ICityInfoRepository
    {
	    bool Save();

		// Read
		bool CityExists(int cityId);
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointsOfInterest);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

		// Write
	    void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
	    void DeletePointOfInterest(PointOfInterest pointOfInterest);

    }
}

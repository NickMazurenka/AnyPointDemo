using System.Collections.Generic;

namespace AnyPointDemo.Models
{
    /// <summary>
    /// City object with points of interest
    /// </summary>
    public class CityDto
    {
        /// <summary>
        /// Id of city
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of city
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// City description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Number of points of interest for city
        /// </summary>
        public int NumberOfPointsOfInterest => PointsOfInterest.Count;

        /// <summary>
        /// Collection of points of interest for city
        /// </summary>
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
    }
}

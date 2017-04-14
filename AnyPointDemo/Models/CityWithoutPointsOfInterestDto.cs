namespace AnyPointDemo.Models
{
    /// <summary>
    /// City without points of interest
    /// </summary>
    public class CityWithoutPointsOfInterestDto
    {
        /// <summary>
        /// Id of city
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// City description
        /// </summary>
        public string Description { get; set; }
    }
}

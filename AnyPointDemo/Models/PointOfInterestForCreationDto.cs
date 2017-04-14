using System.ComponentModel.DataAnnotations;

namespace AnyPointDemo.Models
{
	//[FluentValidation.Attributes.Validator(typeof(PointOfInterestForCreationDto))]
	/// <summary>
	/// Point of interest for creation
	/// </summary>
	public class PointOfInterestForCreationDto
    {
        /// <summary>
        /// Name of point of interest
        /// </summary>
        [Required(ErrorMessage = "Your should provide a name value")]
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// Description of point of interest
        /// </summary>
        [MaxLength(256)]
		public string Description { get; set; }
    }
}

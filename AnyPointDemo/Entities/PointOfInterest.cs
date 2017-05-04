﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointOfInterest.cs" company="TractManager, Inc.">
//   Copyright © 2017
// </copyright>
// <summary>
//   Defines the PointOfInterest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnyPointDemo.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The point of interest.
    /// </summary>
    public class PointOfInterest
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [ForeignKey("CityId")]
        public City City { get; set; }

        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        public int CityId { get; set; }
    }
}

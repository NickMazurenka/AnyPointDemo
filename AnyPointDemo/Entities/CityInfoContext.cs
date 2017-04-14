using System.Collections.Generic;
using System.Data.Entity;

namespace AnyPointDemo.Entities
{
    public sealed class CityInfoContext : DbContext
    {
		//public CityInfoContext() : base("AnyPointDemoSqlConnection") { }

		public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

	    
    }
}
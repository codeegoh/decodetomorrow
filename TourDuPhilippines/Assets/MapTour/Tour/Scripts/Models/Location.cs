

namespace Digify
{
	public class Location
	{
		public double Latitude {get; set;}
		public double Longitude {get; set;}
        public int Elevation {get; set;}

		public Location(double latitude, double longitude, int elevation)
		{
			this.Latitude = latitude;
            this.Longitude = longitude;
            this.Elevation = elevation;
		}
	}
}
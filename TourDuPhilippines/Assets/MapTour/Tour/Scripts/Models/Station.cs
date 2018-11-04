

namespace Digify
{
	public class Station
	{
		public int Id {get; set;}
		public string Name {get; set;}
		public Location Location {get; set;}
		public Forecast Forecast {get; set;}

		public Station(int id, string name, Location location, Forecast forecast)
		{
			this.Id = id;
			this.Name = name;
			this.Location = location;
			this.Forecast = forecast;
		}
	}
}
using System;

namespace Digify
{
	public class Forecast
	{
		public DateTime Timestamp {get; set;}
		public double Temperature {get; set;}
            public double WindSpeed {get; set;}
            public double SolarRadiation {get; set;}
            public double MeanSeaLevelTemperature {get; set;}
            public double Rain {get; set;}
            public double Dewpoint {get; set;}
            public double WindGust {get; set;}
            public double WindDirection {get; set;}
            public double HeatIndex {get; set;}
            public double TotalCloudCover {get; set;}
            public double RainProbability {get; set;}

		public Forecast(DateTime timestamp, double temperature, double windSpeed, 
                  double solarRadiation, double meanSeaLevelTemperature, double rain, 
                  double dewpoint, double windGust, double windDirection, double heatIndex, 
                  double totalCloudCover, double rainProbability)
		{
                  this.Timestamp = timestamp;
                  this.Temperature = temperature;
                  this.WindSpeed = windSpeed;
                  this.SolarRadiation = solarRadiation;
                  this.MeanSeaLevelTemperature = meanSeaLevelTemperature;
                  this.Rain = rain;
                  this.Dewpoint = dewpoint;
                  this.WindGust = windGust;
                  this.WindDirection = windDirection;
                  this.HeatIndex = heatIndex;
                  this.TotalCloudCover = totalCloudCover;
                  this.RainProbability = rainProbability;
		}
	}
}
using System;
using System.Collections.Generic;

namespace Digify
{
	public class WindSpeedInterpretation: WeatherInterpretation
	{
        private double windSpeed;

        public WindSpeedInterpretation(Forecast forecast) : base(forecast)
        {
            this.windSpeed = forecast.WindSpeed;
        }

        public override string Weather()
        {
            return windSpeed + " Kilometers per hour";
        }

        public override string Interpret()
        {
            string interpretation = "";

            if (windSpeed >= 115.2)
                interpretation = "Hurricane";
            else if (windSpeed >= 100.8)
                interpretation = "Violent Storm";
            else if (windSpeed >= 86.4)
                interpretation = "Storm";
            else if (windSpeed >= 75.6)
                interpretation = "Strong Gale";
            else if (windSpeed >= 61.2)
                interpretation = "Gale";
            else if (windSpeed >= 50.4)
                interpretation = "Near Gale";
            else if (windSpeed >= 39.6)
                interpretation = "Strong Breeze";
            else if (windSpeed >= 28.8)
                interpretation = "Fresh Breeze";
            else if (windSpeed >= 18)
                interpretation = "Moderate Breeze";
            else if (windSpeed >= 10.8)
                interpretation = "Gentle Breeze";
            else if (windSpeed >= 7.2)
                interpretation = "Light Breeze";
            else if (windSpeed >= 3.6)
                interpretation = "Light Air";
            else
                interpretation = "Calm";

            return interpretation;  
        }
    }
}

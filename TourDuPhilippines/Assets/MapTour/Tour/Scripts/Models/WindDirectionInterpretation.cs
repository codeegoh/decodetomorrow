using System;
using System.Collections.Generic;

namespace Digify
{
	public class WindDirectionInterpretation: WeatherInterpretation
	{
        private double windDirection;

        public WindDirectionInterpretation(Forecast forecast) : base(forecast)
        {
            this.windDirection = forecast.WindDirection;
        }

        public override string Weather()
        {
            return windDirection + " Degrees";
        }

        public override string Interpret()
        {
            string interpretation = "";

            if (windDirection > 326.25 && windDirection <= 348.75)
                interpretation = "NNW";
            else if (windDirection > 303.75)
                interpretation = "NW";
            else if (windDirection > 281.25)
                interpretation = "WNW";
            else if (windDirection > 258.75)
                interpretation = "W";
            else if (windDirection > 236.25)
                interpretation = "WSW";
            else if (windDirection > 213.75)
                interpretation = "SW";
            else if (windDirection > 191.25)
                interpretation = "SSW";
            else if (windDirection > 168.75)
                interpretation = "S";
            else if (windDirection > 146.25)
                interpretation = "SSE";
            else if (windDirection > 123.75)
                interpretation = "SE";
            else if (windDirection > 101.25)
                interpretation = "ESE";
            else if (windDirection > 78.75)
                interpretation = "E";
            else if (windDirection > 56.25)
                interpretation = "ENE";
            else if (windDirection > 33.75)
                interpretation = "NE";
            else if (windDirection > 11.25)
                interpretation = "NNE";
            else
                interpretation = "N";

            return interpretation; 
        }
    }
}

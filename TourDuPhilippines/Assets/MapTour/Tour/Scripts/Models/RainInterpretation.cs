using System;
using System.Collections.Generic;

namespace Digify
{
	public class RainInterpretation: WeatherInterpretation
	{
        private double rain;

        public RainInterpretation(Forecast forecast) : base(forecast)
        {
            this.rain = forecast.Rain;
        }

        public override string Weather()
        {
            return rain + " Millimeters";
        }


        public override string Interpret()
        {
            string interpretation = "";

            if (rain >= 20)
                interpretation = "Extreme Rain";
            else if (rain >= 10)
                interpretation = "Heavy Rain";
            else if (rain >= 5)
                interpretation = "Moderate Rain";
            else if (rain >= 1)
                interpretation = "Light Rain";
            else if (rain > 0.1)
                interpretation = "Drizzle";
            else
                interpretation = "Sunny";

            return interpretation;        
        }
    }
}

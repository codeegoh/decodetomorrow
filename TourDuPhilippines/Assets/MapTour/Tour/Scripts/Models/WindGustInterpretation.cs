using System;
using System.Collections.Generic;

namespace Digify
{
	public class WindGustInterpretation: WeatherInterpretation
	{
        private double windGust;

        public WindGustInterpretation(Forecast forecast) : base(forecast)
        {
            this.windGust = forecast.WindGust;
        }

        public override string Weather()
        {
            return windGust + " Kilometers per hour";
        }

        public override string Interpret()
        {
            string interpretation = "";

            if (windGust >= 220)
                interpretation = "Super Typhoon Strength";
            else if (windGust >= 118)
                interpretation = "Typhoon (TY) Strength";
            else if (windGust >= 89)
                interpretation = "Severe Tropical Storm (STS) Strength";
            else if (windGust >= 62)
                interpretation = "Tropical Storm (TS) Strength";
            else if (windGust >= 45)
                interpretation = "Tropical Depression (TD) Strength";
            else if (windGust >= 31)
                interpretation = "LPA Strength";
            else
                interpretation = "Normal or Calm";

            return interpretation;   
        }
    }
}
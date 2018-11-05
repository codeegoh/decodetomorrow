using System;
using System.Collections.Generic;

namespace Digify
{
	public class HeatIndexInterpretation: WeatherInterpretation
	{
        private double heatIndex;

        public HeatIndexInterpretation(Forecast forecast) : base(forecast)
        {
            this.heatIndex = forecast.HeatIndex;
        }

        public override string Weather()
        {
            return heatIndex + " Degree Celsius";
        }

        public override string Interpret()
        {
            string interpretation = "";

            if (heatIndex > 51)
                interpretation = "Extreme Danger";
            else if (heatIndex > 40)
                interpretation = "Danger";
            else if (heatIndex > 32)
                interpretation = "Extreme Caution";
            else if (heatIndex > 28)
                interpretation = "Caution";
            else
                interpretation = "No Warnings";

            return interpretation;
        }
    }
}
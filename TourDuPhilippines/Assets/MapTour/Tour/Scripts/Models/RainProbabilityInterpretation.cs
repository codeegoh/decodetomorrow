using System;
using System.Collections.Generic;

namespace Digify
{
	public class RainProbabilityInterpretation: WeatherInterpretation
	{
        private double rainProbability;

        public RainProbabilityInterpretation(Forecast forecast) : base(forecast)
        {
            this.rainProbability = forecast.RainProbability;
        }

        public override string Weather()
        {
            return rainProbability + " Percent";
        }

        public override string Interpret()
        {
            string interpretation = "";

            if (rainProbability > 60)
                interpretation = "High";
            else if (rainProbability > 30)
                interpretation = "Medium";
            else
                interpretation = "Low";

            return interpretation;  
        }
    }
}
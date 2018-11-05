using System;
using System.Collections.Generic;

namespace Digify
{
	public class TemperatureInterpretation: WeatherInterpretation
	{
        private double temperature;

        public TemperatureInterpretation(Forecast forecast) : base(forecast)
        {
            this.temperature = forecast.Temperature;
        }

        public override string Weather()
        {
            return temperature + " Degree Celsius";
        }

        public override string Interpret()
        {
            string interpretation = "";

            if (temperature > 32)
                interpretation = "Very Hot";
            else if (temperature > 27)
                interpretation = "Hot";
            else if (temperature > 22)
                interpretation = "Warm";
            else if (temperature > 17)
                interpretation = "Mild";
            else if (temperature > 12)
                interpretation = "Cool";
            else if (temperature > 7)
                interpretation = "Cold";
            else
                interpretation = "Very COld";

            return interpretation;  
        }
    }
}
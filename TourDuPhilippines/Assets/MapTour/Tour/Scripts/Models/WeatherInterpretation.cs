using System;
using System.Collections.Generic;

namespace Digify
{
	public abstract class WeatherInterpretation
	{
        protected Forecast forecast;
        public WeatherInterpretation(Forecast forecast) 
        {
           this.forecast = forecast;
        }

        public abstract string Weather();

        public abstract string Interpret();

    }
}

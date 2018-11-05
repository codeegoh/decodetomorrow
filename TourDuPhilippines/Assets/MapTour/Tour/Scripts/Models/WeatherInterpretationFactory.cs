using System;
using System.Collections.Generic;

namespace Digify
{
    public enum WeatherType { HEAT_INDEX, RAIN, RAIN_PROBABILITY, TEMPERATURE, 
        TOTAL_CLOUD_COVER, WIND_DIRECTION, WIND_GUST, WIND_SPEED };

	public class WeatherInterpretationFactory
	{
        public static string Interpret (Forecast forecast, WeatherType type)
        {
            switch(type)
            {
                case WeatherType.HEAT_INDEX: 
                    return new HeatIndexInterpretation(forecast).Interpret();
                case WeatherType.RAIN:
                    return new RainInterpretation(forecast).Interpret();
                case WeatherType.RAIN_PROBABILITY:
                    return new RainProbabilityInterpretation(forecast).Interpret();
                case WeatherType.TEMPERATURE: 
                    return new TemperatureInterpretation(forecast).Interpret();
                case WeatherType.TOTAL_CLOUD_COVER: 
                    return new TotalCloudCoverInterpretation(forecast).Interpret();
                case WeatherType.WIND_DIRECTION: 
                    return new WindDirectionInterpretation(forecast).Interpret();
                case WeatherType.WIND_GUST: 
                    return new WindGustInterpretation(forecast).Interpret();
                case WeatherType.WIND_SPEED: 
                    return new WindSpeedInterpretation(forecast).Interpret();

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
    

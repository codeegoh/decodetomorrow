using System;
using System.Collections.Generic;

namespace Digify
{
	public class TotalCloudCoverInterpretation: WeatherInterpretation
	{
        private double totalCloudCover;

        public TotalCloudCoverInterpretation(Forecast forecast) : base(forecast)
        {
            this.totalCloudCover = forecast.TotalCloudCover;
        }

        public override string Weather()
        {
            return totalCloudCover + " Percent";
        }

        public override string Interpret()
        {
            string interpretation = "";

            if (totalCloudCover > 75)
                interpretation = "Overcast";
            else if (totalCloudCover > 50)
                interpretation = "Mostly Cloudy";
            else if (totalCloudCover > 30)
                interpretation = "Partly Cloudy";
            else
                interpretation = "Clear";

            return interpretation;          
        }
    }
}
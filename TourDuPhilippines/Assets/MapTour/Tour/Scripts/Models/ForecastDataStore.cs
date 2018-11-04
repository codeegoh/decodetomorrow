using System;
using System.Collections.Generic;

namespace Digify
{
	public class ForecastDataStore : BaseDataStore<Forecast>, IDataStore
	{
		List<Forecast> forecasts = new List<Forecast>();
        public Location Location {get; set;}

        public StoreType Type()
		{
            return StoreType.FORECAST;
		}
		
		public List<Forecast> All()
		{	
			return forecasts;
		}

		public override void Clear()
		{
			forecasts.Clear();
		}
		
		public override void Add(Forecast forecast)
		{
            forecasts.Add(forecast);
		}

		public override void Remove(Forecast forecast)
		{
			forecasts.Remove(forecast);
		}

        public override long Size()
		{
			return forecasts.Count;
		}
	}
}
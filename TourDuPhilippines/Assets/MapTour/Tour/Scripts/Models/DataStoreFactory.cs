using System;
using System.Collections.Generic;

namespace Digify
{
    public enum StoreType { STATION, FORECAST };
    
	public class DataStoreFactory
	{
        public IDataStore Create (StoreType type)
        {
            switch(type)
            {
                case StoreType.STATION: 
                    return new StationDataStore();
                case StoreType.FORECAST:
                    return new ForecastDataStore();
                default:
                    throw new InvalidOperationException();
            }
        }
	}
}
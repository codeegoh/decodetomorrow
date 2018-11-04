using System;
using System.Collections.Generic;

namespace Digify
{
	public class StationDataStore : BaseDataStore<Station>, IDataStore
	{
		Dictionary <int, Station> stations = new Dictionary<int, Station>();

		public StoreType Type()
		{
            return StoreType.STATION;
		}

		public Station Get(int id)
		{
			return stations[id];
		}

		public Dictionary<int, Station> All()
		{	
			return stations;
		}

		public IEnumerable<int> Ids()
		{
			return stations.Keys;
		}

		public override void Clear()
		{
			stations.Clear();
		}
		
		public override void Add(Station station)
		{
			stations.Add(station.Id, station);
		}

		public override void Remove(Station station)
		{
			stations.Remove(station.Id);
		}

		public override long Size()
		{
			return stations.Count;
		}
	}
}
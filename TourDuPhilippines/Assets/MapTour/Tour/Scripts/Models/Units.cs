

namespace Digify
{
	public class Units
	{
		public string time {get; set;}
		public string temperature {get; set;}
		public string windSpeed {get; set;}
		public string solarRadiation {get; set;}
		public string meanSeaLevelPressure {get; set;}
		public string rain {get; set;}
		public string dewpoint {get; set;}
		public string windGust {get; set;}
		public string windDirection {get; set;}
		public string heatIndex {get; set;}
		public string totalCloudCover {get; set;}
		public string rainProbability {get; set;}
        private static Units Instance = new Units();

        private Units() {}

		public static void Set(string time, string temperature, string windSpeed, string solarRadiation,
                string meanSeaLevelPressure, string rain, string dewpoint, string windGust,
                string windDirection, string heatIndex, string totalCloudCover, string rainProbability)
		{
			Instance.time = time;
            Instance.temperature = temperature;
            Instance.windSpeed = windSpeed;
            Instance.solarRadiation = solarRadiation;
            Instance.meanSeaLevelPressure = meanSeaLevelPressure;
            Instance.rain = rain;
            Instance.dewpoint = dewpoint;
            Instance.windGust = windGust;
            Instance.windDirection = windDirection;
            Instance.heatIndex = heatIndex;
            Instance.totalCloudCover = totalCloudCover;
            Instance.rainProbability = rainProbability;    
        }

        public static Units GetInstance()
        {
            return Instance;
        }
	}
}
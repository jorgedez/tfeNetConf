using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather
{

    public class Forecast
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public object PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public Temperature Temperature { get; set; }
        public Realfeeltemperature RealFeelTemperature { get; set; }
        public RealfeeltemperatureShade RealFeelTemperatureSahde {get; set; }

    
    public class Maximum2
    {
        public Metric27 Metric { get; set; }
        public Imperial27 Imperial { get; set; }
    }

    public class Metric27
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Imperial27
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

}

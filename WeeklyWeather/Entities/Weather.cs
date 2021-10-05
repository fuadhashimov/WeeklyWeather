using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeeklyWeather.Entities
{
    public class WeatherForecast
    {
        [JsonProperty("Daily")]
        public List<DailyWeatherForecast> DailyWeatherForecast { get; set; }
    }
}

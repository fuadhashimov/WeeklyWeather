using Newtonsoft.Json;

namespace WeeklyWeather.Entities
{
    public class DailyWeatherForecast
    {
        [JsonProperty("dt")]
        public int UnixDate { get; set; }

        [JsonProperty("temp")]
        public Temperature Temperature { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }
    }
}

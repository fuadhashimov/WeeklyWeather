using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WeeklyWeather.Entities;

namespace WeeklyWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                var response = httpClient.GetAsync($"data/2.5/onecall?lat=35.1264&lon=33.4299&exclude=weekly&units=metric&appid={apiKey}").Result;
                var result = response.Content.ReadAsStringAsync().Result;

                var weatherForecast = JsonConvert.DeserializeObject<WeatherForecast>(result);

                foreach (var dailyWeatherForecast in weatherForecast.DailyWeatherForecast)
                {
                    var date = UnixTimeStampToDateTime(dailyWeatherForecast.UnixDate);

                    var content = new StringBuilder();
                    content.Append($"Temperature: {Convert.ToInt32(dailyWeatherForecast.Temperature.Day) } \t\t");
                    content.Append($"Pressure: {dailyWeatherForecast.Pressure } \t\t");
                    content.Append($"Humidity: {dailyWeatherForecast.Humidity }");

                    Console.WriteLine(date.ToString("dd/MM/yyyy"));
                    Console.WriteLine(content.ToString());
                    Console.WriteLine();
                }
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTime;
        }
    }
}

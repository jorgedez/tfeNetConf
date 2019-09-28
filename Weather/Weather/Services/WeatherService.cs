using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using static Weather.Weather;

namespace Weather.Services
{
    public class WeatherService : WeatherBase
    {
        private IMemoryCache _cache;

        public WeatherService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public override Task<WeatherResponse> GetWeather(Empty request, ServerCallContext context)
        {
            return Task.FromResult(GetCurrentWeatherResponse(_cache.Get<Forecast>()));
        }

        public override async Task GetWeatherStream(Empty request, IServerStreamWriter<WeatherResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var cachedForecast = _cache.Get<Forecast>("WeatherCache");
                await responseStream.WriteAsync(GetCurrentWeatherResponse(cachedForecast));
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        public static WeatherResponse GetCurrentWeatherResponse(Forecast forecast)
        {
            return new WeatherResponse()
            {
                WeatherText = forecast.WeatherText,
                IsDayTime = forecast.IsDayTime,
                Pressure = forecast.Pressure.Imperial.Value,
                RelativeHumidity = forecast.RealativeHumidity,
                RetrievedTime = Timestamp.FromDateTime(DateTime.UtcNow),
                Temperature = forecast.Temperature.Imperial.Value,
                UVIndex = forecast.UVIndex,
                WeatherIcon = forecast.WeatherIcon,
                WeatherUri = $"https://developer.accuweather.com/sites/default/",
                WindSpeed = forecast.Wind.Speed.Imperial.Value,
                WindDirection = forecast.Wind.Direction.English,
                Past6HourMax = forecast.TemperatureSummary,Past6HourRange.Maximun.Value,
                Past6HourMin = forecast.RealFeelTemperatureSummary.Past6HourRange.Minimun.Value,


            }
        }
    }
}

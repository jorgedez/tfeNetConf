using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using static Weather.Weather;

namespace ClientConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5005");
            
            var client = new WeatherClient(channel);

            var forecast = await client.GetWeatherAsync(new Empty());

            Console.WriteLine($" Location -> {forecast.Location}");
            Console.WriteLine($" Temperature -> {forecast.Temperature}");
            Console.WriteLine($" Past6HourMax -> {forecast.Past6HourMax}");
            Console.WriteLine($" Past6HourMin -> {forecast.Past6HourMin}");
            Console.WriteLine($" Pressure -> {forecast.Pressure}");
            Console.WriteLine($" RelativeHumidity -> {forecast.RelativeHumidity}");
            Console.WriteLine($" RetrievedTime -> {forecast.RetrievedTime}");
            Console.WriteLine($" UvIndex -> {forecast.UvIndex}");
            Console.WriteLine($" WeatherText -> {forecast.WeatherText}");
            Console.WriteLine($" WindDirection -> {forecast.WindDirection}");
            Console.WriteLine($" WindSpeed -> {forecast.WindSpeed}");
            Console.WriteLine($" WeartherUri -> {forecast.WeartherUri}");
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

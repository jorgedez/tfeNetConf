using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using static Weather.Weather;

namespace ClientConsoleStream
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5005");

            var client = new WeatherClient(channel);

            using var streamForecast = client.GetWeatherStream(new Empty());

            var responseProcessing = Task.Run(async () =>
            {
                try
                {
                    await foreach (var forecast in streamForecast.ResponseStream.ReadAllAsync())
                    {
                        Console.WriteLine($"{forecast.Location} | {forecast.Temperature} C | {forecast.Past6HourMax} C | { forecast.Past6HourMin} C | {forecast.Pressure} | {forecast.WindDirection} | {forecast.WindSpeed} km/h");
                    }
                }
                catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
                {
                    Console.WriteLine("Stream cancelled.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading response: " + ex);
                }
            });

            Console.WriteLine("Completing request stream");
            Console.WriteLine("Request stream completed");

            await responseProcessing;

            Console.WriteLine("Read all responses");
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}

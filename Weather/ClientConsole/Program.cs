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
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            
            var client = new WeatherClient(channel);

            var forecast = await client.GetWeatherAsync(new Empty());

            Console.WriteLine($"{forecast.Temperature}");
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

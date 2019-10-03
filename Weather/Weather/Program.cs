using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Weather
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddKeyPerFile("/etc/token", true);
                })
                .ConfigureWebHostDefaults((webBuilder) =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel((ctx, options) =>
                    {
                        //In production we have a proxy that takes care of certificates for us
                        //so we don't have one in the container to bind to. However, in dev we
                        //want our clients to not have any switches to not use certificates so
                        //we switch the server around.
                        if (ctx.HostingEnvironment.IsProduction())
                        {
                            options.Listen(IPAddress.Any, 5050, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http2;
                            });
                        }
                        else
                        {
                            options.Listen(IPAddress.Any, 5050, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http2;
                            });
                            options.Listen(IPAddress.Any, 5051, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1;
                            });
                        }
                    });
                });
    }
}

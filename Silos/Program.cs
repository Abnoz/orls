﻿using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;

try
{
    using IHost host = await StartSiloAsync();
    Console.WriteLine("\n\n Press Enter to terminate...\n\n");
    Console.ReadLine();

    await host.StopAsync();

    return 0;
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    return 1;
}

static async Task<IHost> StartSiloAsync()
{
    IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("/Users/zozza/RiderProjects/OrleansDemo/Silos/appsettings.json", false, true)
        .Build();

    string sqlServerConnectionString = configuration["ConnectionStrings:SqlServerConnectionString"] ?? string.Empty;

    var builder = Host
        .CreateDefaultBuilder()
        .UseOrleans((context, silo) =>
        {
            silo
                .UseAdoNetClustering(options =>
                {
                    options.ConnectionString = sqlServerConnectionString;
                    options.Invariant = "System.Data.SqlClient";
                })

                .AddAdoNetGrainStorage("demoGrainStorage", options =>
                {
                    options.ConnectionString = sqlServerConnectionString;
                    options.Invariant = "System.Data.SqlClient";
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "zozzo";

                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Configure<EndpointOptions>(options =>
                {
                    options.SiloListeningEndpoint = new IPEndPoint(IPAddress.Loopback, 11111);
                    options.GatewayListeningEndpoint = new IPEndPoint(IPAddress.Loopback, 30000);
                    options.AdvertisedIPAddress = IPAddress.Loopback;
                    options.SiloPort = 11111;
                    options.GatewayPort = 30000;
                })
                .UseDashboard(options =>
                {
                    options.Username = "username";
                    options.Password = "password";
                    options.Host = "localhost";
                    options.Port = 9000;
                    options.HostSelf = true;
                    options.CounterUpdateIntervalMs = 1000;
                });
        });
    var host = builder.Build();
    await host.StartAsync();
    return host;
}
 


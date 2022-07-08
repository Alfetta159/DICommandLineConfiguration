// See https://aka.ms/new-console-template for more information
using DiClParameterConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

using IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, configuration) =>
        configuration.AddInMemoryCollection(
            new Dictionary<string, string>
            {
                ["First"] = args[0],
                ["Second"] = args[1],
                ["Third"] = args[2],
                ["Fourth"] = args[3],
                ["Fifth"] = args[4]
            }))
        .ConfigureServices((_, services) =>
            services
                .AddScoped<TestClass>()
    )
    .Build();

var test = host.Services.GetService<TestClass>();

test!.Run();
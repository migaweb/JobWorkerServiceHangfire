using Microsoft.AspNetCore;
using JobWorkerService;
using Hangfire;
using Hangfire.SqlServer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
      services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage("Server=localhost;Database=HangfireDB;Integrated Security=True;", new SqlServerStorageOptions
        {
          CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
          SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
          QueuePollInterval = TimeSpan.Zero,
          UseRecommendedIsolationLevel = true,
          DisableGlobalLocks = true
        }));

      services.AddHangfireServer();
      services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

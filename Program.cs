using IHostService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<FileWatcherService>();
    })
    .Build();

host.Run();

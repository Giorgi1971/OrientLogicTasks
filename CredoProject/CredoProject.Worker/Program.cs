using CredoProject.Worker;
using CredoProject.Core.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        //services.AddHostedService<ICardRepository, CardRepository>();
        //services.AddSingleton<ICardRepository, CardRepository>();
    })
    .Build();

host.Run();


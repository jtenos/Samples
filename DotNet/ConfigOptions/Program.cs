using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<Foo>(context.Configuration.GetSection(nameof(Foo)));
        services.AddHostedService<Program>();
    })
    .Build();

host.Run();

partial class Program(IOptions<Foo> foo) : IHostedService
{
    Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(foo.Value);
        return Task.CompletedTask;
    }

    Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class Foo
{
    public required string Message { get; set; }
    public required int Count { get; set; }
    public required DateTime StartDate { get; set; }
    public required Item[] Items { get; set; }
    public required string[] Words { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions
    {
        WriteIndented = true
    });
}

public class Item
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions
    {
        WriteIndented = true
    });
}

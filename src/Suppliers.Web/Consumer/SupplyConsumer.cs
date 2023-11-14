using System.Text.Json;
using Confluent.Kafka;
using Suppliers.Web.Interfaces.DomainServices;
using Suppliers.Web.Model.Dto;

namespace Suppliers.Web.Consumer;

public class SupplyConsumer : BackgroundService
{
    
    //private const string BootstrapServers = "localhost:29094";
    private const string BootstrapServers = "kafka:9092";
    private const string GroupId = "SupplyConsumer";
    private const string Topic = "mp3-request-restock";
    
    private readonly IServiceProvider _serviceProvider;
    
    public SupplyConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
        
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        var config = new ConsumerConfig
        {
            GroupId = GroupId,
            BootstrapServers = BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            AllowAutoCreateTopics = true
        };

        using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            var cancelToken = new CancellationTokenSource();

            consumerBuilder.Subscribe(Topic);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = consumerBuilder.Consume(cancelToken.Token);
                    var jsonObj = consumeResult.Message.Value;

                    using var scope = _serviceProvider.CreateScope();
                    var supplierService = scope.ServiceProvider.GetRequiredService<ISupplierService>();
                
                    try
                    {
                        var supplies = JsonSerializer.Deserialize<SupplyDto>(jsonObj);
                        Console.WriteLine(supplies);

                        if (supplies != null )
                        {
                            await supplierService.CreateSupplyAsync(supplies);
                        }
                    }
                    catch (JsonException ex)
                    {
                        // Handle JSON deserialization errors
                        Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                consumerBuilder.Close();
            }
        }
    }
}

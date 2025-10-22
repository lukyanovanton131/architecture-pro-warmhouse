using Confluent.Kafka;
using Microsoft.Extensions.Options;
using TelemetryCollectorApp;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services
    .AddOptionsWithValidateOnStart<KafkaOptions>()
    .BindConfiguration(KafkaOptions.SectionTitle);


builder.Services.AddSingleton<IProducer<string, TelemetryDataMessage>>(sp =>
{
    var options = sp.GetRequiredService<IOptions<KafkaOptions>>().Value;
    var producerConfig = new ProducerConfig()
    {
        BootstrapServers = options.BootstrapServers,
        MessageTimeoutMs = 10000,
        SocketTimeoutMs = 5000,
        ApiVersionRequestTimeoutMs = 5000,
        ReconnectBackoffMs = 10000,
        ReconnectBackoffMaxMs = 10000,
        RetryBackoffMs = 1000,
        QueueBufferingMaxMessages = 10,
        QueueBufferingMaxKbytes = 1024,
        LingerMs = 0,
    };
		
    var builder = new ProducerBuilder<string, TelemetryDataMessage>(producerConfig);
    builder.SetValueSerializer(new MessageSerializer<TelemetryDataMessage>());
    return builder.Build();
});


var host = builder.Build();
host.Run();
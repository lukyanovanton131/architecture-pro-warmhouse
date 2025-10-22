using Confluent.Kafka;

namespace TelemetryCollectorApp;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IProducer<string, TelemetryDataMessage> _producer;

    public Worker(ILogger<Worker> logger, IProducer<string, TelemetryDataMessage> producer)
    {
        _logger = logger;
        _producer = producer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(500, stoppingToken);
            var sensorId = Guid.NewGuid().ToString();
            var message = new Message<string, TelemetryDataMessage> {Key = sensorId, Value = new TelemetryDataMessage()
            {
                SensorId = sensorId,
                Value = new Random().NextDouble(),
                Unit = "°C"
            }};
            await _producer.ProduceAsync("telemetry.data", message, stoppingToken);
        }
    }
}
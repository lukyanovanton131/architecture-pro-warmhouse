namespace TelemetryCollectorApp;

public class KafkaOptions
{
    public const string SectionTitle = "KafkaIntegration";
    
    public string BootstrapServers { get; set; }
    public string GroupId { get; set; }
    public string TelemetryDataTopic { get; set; }
}
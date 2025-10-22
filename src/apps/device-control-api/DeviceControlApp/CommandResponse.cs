namespace DeviceControlApp;

public class CommandResponse
{
    public string Status { get; set; }
    public string RelayId { get; set; }
    public double Value { get; set; }
    public DateTime Timestamp { get; set; }
}
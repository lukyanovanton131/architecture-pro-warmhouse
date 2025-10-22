namespace ArchitecturePro;

public static class LocationNameProvider
{
    public static string GetLocationName(string location, string sensorID)
    {
            switch (sensorID) {
                case "1":
                    return "Living Room";
                case "2":
                    return "Bedroom";
                case "3":
                    return "Kitchen";
                default:
                    return "Unknown";
            }
    }
}
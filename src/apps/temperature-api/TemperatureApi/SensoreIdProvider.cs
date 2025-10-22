namespace ArchitecturePro;

public static class SensoreIdProvider
{
    public static string Get(string location)
    {
        switch (location) {
            case "Living Room":
                return "1";
            case "Bedroom":
                return "2";
            case "Kitchen":
                return "3";
            default:
                return "0";
        }
    }
}
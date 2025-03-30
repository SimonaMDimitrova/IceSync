namespace IceSync.Configurations;

public class ExternalAPIs
{
    private ExternalAPIs() { }

    public static ExternalAPIs Instance { get; private set; } = new();

    public string UniversalLoaderAPI { get; set; }
}

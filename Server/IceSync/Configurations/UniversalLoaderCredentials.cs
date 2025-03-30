namespace IceSync.Configurations;

public class UniversalLoaderCredentials
{
    private UniversalLoaderCredentials() {}

    public static UniversalLoaderCredentials Instance { get; private set; } = new();

    public string CompanyId { get; set; }

    public string UserId { get; set; }

    public string UserSecret { get; set; }
}

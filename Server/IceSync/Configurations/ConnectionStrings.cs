namespace IceSync.Configurations;

public class ConnectionStrings
{
    private ConnectionStrings() { }

    public static ConnectionStrings Instance { get; private set; } = new();

    public string IceSyncConnectionString { get; set; }
}

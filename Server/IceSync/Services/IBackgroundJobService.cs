namespace IceSync.Services;

public interface IBackgroundJobService
{
    Task SyncWorkflowsAsync();
}

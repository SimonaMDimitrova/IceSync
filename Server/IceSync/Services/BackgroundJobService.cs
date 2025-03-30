namespace IceSync.Services;

public class BackgroundJobService : IBackgroundJobService
{
    private readonly IWorkflowsService _workflowsService;

    public BackgroundJobService(IWorkflowsService workflowsService)
    {
        this._workflowsService = workflowsService;
    }

    public async Task SyncWorkflowsAsync()
    {
        await this._workflowsService.SyncWorkflowsAsync();
    }
}

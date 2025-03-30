namespace IceSync.Services;

using DTOs;

public interface IWorkflowsService
{
    Task SyncWorkflowsAsync();

    List<WorkflowDTO> GetAll();

    Task RunAsync(int id);
}

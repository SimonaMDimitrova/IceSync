namespace IceSync.ExternalAPIs;

using DTOs;

public interface IUniversalLoaderClient
{
    Task<List<WorkflowDTO>> GetWorkflowsAync();

    Task RunWorkflowAsync(int workflowId);
}

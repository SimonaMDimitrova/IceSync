namespace IceSync.Services;

using Data;
using Data.Entities;
using DTOs;
using External = ExternalAPIs.DTOs;
using ExternalAPIs;

using Microsoft.EntityFrameworkCore;

public class WorkflowsService : IWorkflowsService
{
    private readonly IUniversalLoaderClient _universalLoaderClient;
    private readonly IceSyncContext _iceSyncContext;

    public WorkflowsService(IUniversalLoaderClient universalLoaderClient,
                            IceSyncContext iceSyncContext)
    {
        this._universalLoaderClient = universalLoaderClient;
        this._iceSyncContext = iceSyncContext;
    }

    public async Task SyncWorkflowsAsync()
    {
        List<External.WorkflowDTO> extrernalWorkflows = await _universalLoaderClient.GetWorkflowsAync();
        DbSet<Workflow> dbWorkflows = this._iceSyncContext.Workflows;

        IEnumerable<int> externalWorkflowIds = extrernalWorkflows.Select(w => w.Id);
        dbWorkflows.RemoveRange(dbWorkflows.Where(db => !externalWorkflowIds.Contains(db.WorkflowId)));

        foreach (var workflowDTO in extrernalWorkflows)
        {
            var dbWorkflow = dbWorkflows.FirstOrDefault(db => db.WorkflowId == workflowDTO.Id);
            if (dbWorkflow == null)
            {
                await dbWorkflows.AddAsync(new Workflow
                {
                    WorkflowId = workflowDTO.Id,
                    WorkflowName = workflowDTO.Name,
                    IsActive = workflowDTO.IsActive,
                    MultiExecBehavior = workflowDTO.MultiExecBehavior
                });
            }
            else
            {
                dbWorkflow.WorkflowName = workflowDTO.Name;
                dbWorkflow.IsActive = workflowDTO.IsActive;
                dbWorkflow.MultiExecBehavior = workflowDTO.MultiExecBehavior;
            }
        }

        await this._iceSyncContext.SaveChangesAsync();
    }

    public List<WorkflowDTO> GetAll()
    {
        List<WorkflowDTO> workflowDTOs = this._iceSyncContext.Workflows
                                                             .Select(x => new WorkflowDTO()
                                                             {
                                                                Id = x.WorkflowId,
                                                                Name = x.WorkflowName,
                                                                IsActive = x.IsActive,
                                                                MultiExecBehavior = x.MultiExecBehavior,
                                                             })
                                                             .OrderByDescending(x => x.IsActive).ToList();

        return workflowDTOs;
    }

    public async Task RunAsync(int id)
    {
        var workflow = _iceSyncContext.Workflows.Single(x => x.WorkflowId == id);
        if (!workflow.IsActive)
        {
            throw new ArgumentException("Cannot run inactive workflow!");
        }

        await this._universalLoaderClient.RunWorkflowAsync(id);
    }
}

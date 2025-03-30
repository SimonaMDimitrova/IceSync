namespace IceSync.Controllers;

using DTOs;
using Services;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("workflows")]
public class WorkflowsController : ControllerBase
{
    private readonly IWorkflowsService _workflowsService;

    public WorkflowsController(IWorkflowsService workflowsService)
    {
        this._workflowsService = workflowsService;
    }

    [HttpGet]
    public List<WorkflowDTO> GetAll()
    {
        List<WorkflowDTO> workflowDTOs = this._workflowsService.GetAll();

        return workflowDTOs;
    }

    [HttpPost]
    [Route("{workflowId}/run")]
    public async Task Run([FromRoute] int workflowId)
    {
        await this._workflowsService.RunAsync(workflowId);
    }
}

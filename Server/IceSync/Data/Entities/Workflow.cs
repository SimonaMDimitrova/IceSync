namespace IceSync.Data.Entities;

using System.ComponentModel.DataAnnotations;

public class Workflow
{
    [Key]
    public int WorkflowId { get; set; }

    public string WorkflowName { get; set; }

    public bool IsActive { get; set; }

    public string MultiExecBehavior { get; set; }
}

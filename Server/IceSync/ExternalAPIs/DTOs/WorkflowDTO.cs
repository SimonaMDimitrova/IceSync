namespace IceSync.ExternalAPIs.DTOs;

using System.Text.Json.Serialization;

public class WorkflowDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("versionName")]
    public string VersionName { get; set; }

    [JsonPropertyName("versionNotes")]
    public string VersionNotes { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("creationDateTime")]
    public DateTime CreationDateTime { get; set; }

    [JsonPropertyName("creationUserId")]
    public int CreationUserId { get; set; }

    [JsonPropertyName("ownerUserId")]
    public int OwnerUserId { get; set; }

    [JsonPropertyName("multiExecBehavior")]
    public string MultiExecBehavior { get; set; }

    [JsonPropertyName("executionRetriesCount")]
    public int? ExecutionRetriesCount { get; set; }

    [JsonPropertyName("executionRetriesPeriod")]
    public int? ExecutionRetriesPeriod { get; set; }

    [JsonPropertyName("executionRetriesPeriodTimeUnit")]
    public string ExecutionRetriesPeriodTimeUnit { get; set; }

    [JsonPropertyName("workflowGroupId")]
    public int WorkflowGroupId { get; set; }

    [JsonPropertyName("canStoreSuccessExecutionData")]
    public bool CanStoreSuccessExecutionData { get; set; }

    [JsonPropertyName("successExecutionDataRetentionPeriodDays")]
    public int? SuccessExecutionDataRetentionPeriodDays { get; set; }

    [JsonPropertyName("canStoreWarningExecutionData")]
    public bool CanStoreWarningExecutionData { get; set; }

    [JsonPropertyName("warningExecutionDataRetentionPeriodDays")]
    public int? WarningExecutionDataRetentionPeriodDays { get; set; }

    [JsonPropertyName("canStoreFailureExecutionData")]
    public bool CanStoreFailureExecutionData { get; set; }

    [JsonPropertyName("failureExecutionDataRetentionPeriodDays")]
    public int? FailureExecutionDataRetentionPeriodDays { get; set; }

    [JsonPropertyName("connectorLogLevel")]
    public string ConnectorLogLevel { get; set; }
}

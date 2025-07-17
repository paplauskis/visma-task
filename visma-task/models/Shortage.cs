using System.Text.Json.Serialization;

namespace visma_task.models;

public class Shortage
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("room")]
    public Room Room { get; set; }
    
    [JsonPropertyName("category")]
    public Category Category { get; set; }
    
    [JsonPropertyName("priority")]
    public int Priority { get; set; }
    
    [JsonPropertyName("created_by")]
    public string CreatedBy { get; set; }
    
    [JsonPropertyName("created_on")]
    public DateOnly CreatedOn { get; set; }
}
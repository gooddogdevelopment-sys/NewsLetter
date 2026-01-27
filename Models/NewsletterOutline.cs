using Newtonsoft.Json;

namespace NewsLetter.Models;

public class NewsletterOutline
{
    [JsonProperty("code_snippet")]
    public string? CodeSnippet { get; set; }
    public required string Tip { get; set; }
    public required string Title { get; set; }
    public required string Overview { get; set; }
    public required string Subject { get; set; }
    
    
}
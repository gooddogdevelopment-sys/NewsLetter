using System.ComponentModel.DataAnnotations;

namespace NewsLetter.Models;

public class Newsletter
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(60)]
    public required string Title { get; set; }
    public string? CodeSnippet { get; set; }
    public required string Overview { get; set; }
    public required string Subject { get; set; }
    public required DateTime SendDate { get; set; }
    public required string AiProvider { get; set; }
    public required string AiModel { get; set; }
}
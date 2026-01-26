namespace Inferno.src.Core.Domain.Entities;

public class OutBoxEvent
{
    public Guid OutBoxEventId { get; set; }
    public string Type { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }
    public int Attempts { get; set; } = 0;
    public string? Error { get; set; }

    public OutBoxEvent(string type, string content)
    {
        Type = type;
        Content = content;
    }
}

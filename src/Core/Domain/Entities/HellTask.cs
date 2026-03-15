using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Core.Domain.Entities;

public class HellTask
{
    public Guid HellTaskId { get; private set; }
    public string? Title { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime DeadLine { get; set; }
    public HellTaskStatus Status { get; private set; } = HellTaskStatus.Awaiting;
    public int Progress { get; private set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public HellTaskPriority Priority { get; set; }

    //FK
    public virtual Demon? Demon { get; private set; }
    public Guid DemonId { get; private set; }

    public HellTask() { }

    public HellTask(string title, string description, Guid demonId, HellTaskPriority priority)
    {
        HellTaskId = Guid.NewGuid();
        Title = title;
        Description = description;
        Priority = priority;
        CreatedAt = DateTime.UtcNow;
        DeadLine = CreatedAt.AddDays(30);
        DemonId = demonId;
    }

    public void UpdateProgress(int newProgress)
    {
        if (newProgress < 0 || newProgress > 100)
            throw new InvalidOperationException("Invalid progress provided");
        Progress = newProgress;
        if (Progress == 100)
            Status = HellTaskStatus.Submit;
        else if (Progress > 0 && Progress < 100)
            Status = HellTaskStatus.Review;
        else
            Status = HellTaskStatus.Awaiting;
        UpdatedAt = DateTime.UtcNow;
    }
}

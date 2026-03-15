using Inferno.src.Adapters.Inbound.Controllers.HellTask;
using Inferno.src.Adapters.Outbound.Persistence.HellTask;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.UseCases.HellTask;

public class HellTaskUseCase(IHellTaskRepository repository, ILogger<HellTaskUseCase> logger)
    : IHellTaskUseCase
{
    private readonly IHellTaskRepository _repository = repository;
    private readonly ILogger<HellTaskUseCase> _logger = logger;

    public async Task<(HellTaskResponse response, string message)> CreateAsync(
        HellTaskInput input,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Creating new HellTask");
        _logger.LogDebug(
            "CreateAsync input: Title='{Title}', DemonId={DemonId}",
            input.Title,
            input.DemonId
        );
        var task = new Entity.HellTask(
            input.Title ?? string.Empty,
            input.Description,
            input.DemonId,
            input.Priority
        );
        await _repository.CreateAsync(task, cancellationToken);
        _logger.LogInformation("Created HellTask {HellTaskId}", task.HellTaskId);
        var response = new HellTaskResponse(
            task.HellTaskId,
            task.Title,
            task.Description,
            task.CreatedAt,
            task.DeadLine,
            task.Status,
            task.Progress,
            task.Priority,
            task.UpdatedAt,
            task.CompletedAt
        );
        return (response, $"Succesfully created task {task.HellTaskId}");
    }

    public async Task<(List<HellTaskResponse> response, string message)> CreateManyAsync(
        List<HellTaskInput> input,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Creating {Count} HellTasks", input?.Count ?? 0);
        if (input is not null)
            _logger.LogDebug(
                "CreateManyAsync demonIds: {DemonIds}",
                string.Join(',', input.Select(i => i.DemonId))
            );
        var task = input
            .Select(task => new Entity.HellTask(
                task.Title ?? string.Empty,
                task.Description,
                task.DemonId,
                task.Priority
            ))
            .ToList();
        await _repository.CreateManyAsync(task, cancellationToken);
        _logger.LogInformation("Created {Count} HellTasks", task.Count);
        var response = task.Select(task => new HellTaskResponse(
                task.HellTaskId,
                task.Title,
                task.Description,
                task.CreatedAt,
                task.DeadLine,
                task.Status,
                task.Progress,
                task.Priority,
                task.UpdatedAt,
                task.CompletedAt
            ))
            .ToList();
        return (response, $"Succesfully created tasks");
    }

    public async Task<(Guid id, string message)> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("DeleteAsync called with empty Guid");
            throw new InvalidOperationException("Invalid guid provided");
        }
        _logger.LogInformation("Deleting HellTask {Id}", id);
        await _repository.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("Deleted HellTask {Id}", id);
        return (id, "Task deleted succesfully");
    }

    public async Task<(List<HellTaskResponse> response, string message)> GetAllByDemonIdAsync(
        int? pageSize,
        int? pageNumber,
        Guid id,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Retrieving HellTasks for id {Id}", id);
        var tasks = await _repository.GetAllByDemonIdAsync(
            pageSize,
            pageNumber,
            id,
            cancellationToken
        );
        if (tasks is null or { Count: 0 })
        {
            _logger.LogInformation("No tasks found for id {Id}", id);
            return (new List<HellTaskResponse>(), $"No tasks found for id {id}");
        }
        _logger.LogInformation("Retrieved {Count} tasks for id {Id}", tasks.Count, id);
        var response = tasks
            .Select(x => new HellTaskResponse(
                x.HellTaskId,
                x.Title,
                x.Description,
                x.CreatedAt,
                x.DeadLine,
                x.Status,
                x.Progress,
                x.Priority,
                x.UpdatedAt,
                x.CompletedAt
            ))
            .ToList();
        return (response, $"Succesfully retrieved tasks for id {id}");
    }

    public async Task<(HellTaskResponse response, string message)> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var task = await _repository.GetByIdAsync(id, cancellationToken);
        if (task == null)
            return (null, $"No task found for id {id}");
        var response = new HellTaskResponse(
            task.HellTaskId,
            task.Title,
            task.Description,
            task.CreatedAt,
            task.DeadLine,
            task.Status,
            task.Progress,
            task.Priority,
            task.UpdatedAt,
            task.CompletedAt
        );
        return (response, $"Succesfully retrieved task for id {id}");
    }
}

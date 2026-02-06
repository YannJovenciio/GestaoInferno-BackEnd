using Inferno.src.Core.Application.Utils.Validation;

namespace Inferno.src.Core.Application.Utils;

public record Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSucces, Error error)
    {
        IsSuccess = isSucces;
        Error = error;
    }

    public static Result Success() => new(true, null);

    public static Result Failure(Error error) =>
        new(false, error ?? throw new ArgumentException(nameof(error)));

    public static implicit operator Result(Error error) => Failure(error);
}

public record Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base(true, null) => Value = value;
    private Result(Error error) : base(false, error) { }

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Error error) => new(error);
}

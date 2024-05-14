namespace Shared.Response;
public class Result<TValue> : Result
{
    private readonly TValue _value;

    public Result(TValue value) : base()
    {
        _value = value;
    }

    public Result(Error error): base(error) { }

    public TValue Value => IsSuccess ? _value :
        throw new ArgumentException("Value can not be accessed", nameof(_value));

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> success,
        Func<Error, TResult> failure) =>
        IsSuccess ? success(_value) : failure(Error);
}

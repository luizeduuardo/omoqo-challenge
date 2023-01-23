namespace Omoqo.Challenge.Api.Core.Models.Base;

public class Result
{
    public bool Success { get; set; } = true;
    public List<string> Errors { get; set; } = new List<string>();

    public Result()
    {
    }

    public Result(string errorMessage)
    {
        Success = false;
        Errors = new List<string> { errorMessage };
    }

    public Result(List<string> errors)
    {
        Success = false;
        Errors = errors;
    }

    public Result(Exception exception)
    {
        Success = false;
        Errors = new List<string> { exception.Message };
    }
}

public class Result<TValue> : Result
{
    public TValue? Value { get; set; }

    public Result(TValue value)
    {
        Value = value;
    }

    public Result(string errorMessage)
    {
        Success = false;
        Errors = new List<string> { errorMessage };
    }

    public Result(List<string> errors)
    {
        Success = false;
        Errors = errors;
    }

    public Result(Exception exception)
    {
        Success = false;
        Errors = new List<string> { exception.Message };
    }
}
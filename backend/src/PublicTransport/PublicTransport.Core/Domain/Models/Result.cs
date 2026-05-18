namespace PublicTransport.Core.Domain.Models;

public class Result
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = "";
    public ResultCode ResultCode { get; set; }

    protected Result()
    {
    }

    public static Result CreateFailedResult(string error, ResultCode code)
    {
        return new Result
        {
            Succeeded = false,
            Message = error,
            ResultCode = code
        };
    }

    public static Result CreateSuccessResult(string message = "", ResultCode code = ResultCode.Ok)
    {
        return new Result
        {
            Succeeded = true,
            Message = message,
            ResultCode = code
        };
    }
}

public class Result<T> : Result
{
    public T? Data { get; set; }
    
    public static Result CreateSuccessResult(string message = "", ResultCode code = ResultCode.Ok, T? data = default)
    {
        return new Result<T>
        {
            Succeeded = true,
            Message = message,
            ResultCode = code,
            Data = data
        };
    }
}
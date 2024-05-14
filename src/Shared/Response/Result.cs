using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Response;
public class Result
{
    public Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    public Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; set; }

    public static Result Success => new();
    public static Result Failure(Error error) => new(error);
}

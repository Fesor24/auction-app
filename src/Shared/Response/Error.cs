namespace Shared.Response;
public class Error
{
    public static Error None => new();

    public Error()
    {
        Code = string.Empty;
        Message = string.Empty;
    }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; set; }
    public string Message { get; set; }
}

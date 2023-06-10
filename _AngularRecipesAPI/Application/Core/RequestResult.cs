namespace Application.Core;

public class RequestResult<T>
{
    public bool IsSuccess { get; set; }
    public T Value { get; set; }

    public string Error { get; set; }

    public static RequestResult<T> Success(T value) => new RequestResult<T>{IsSuccess = true, Value = value};

    public static RequestResult<T> Failure(string error) => new RequestResult<T>{IsSuccess = false, Error = error};
}

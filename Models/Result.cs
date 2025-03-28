namespace Contacts.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string? ErrorField{ get; set; }
        public static Result<T> Success(T value) =>
            new Result<T> { IsSuccess = true, Value = value };

        public static Result<T> Failure(string errorField ,string errorMessage) =>
            new Result<T> { IsSuccess = false, ErrorField = errorField, ErrorMessage = errorMessage };
    }
}

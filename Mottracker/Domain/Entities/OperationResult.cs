namespace Mottracker.Application.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }
        public int StatusCode { get; private set; }

        private OperationResult(bool isSuccess, T? data, string? errorMessage, int statusCode = 200)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public static OperationResult<T> Success(T data, int statusCode = 200)
        {
            return new OperationResult<T>(true, data, null, statusCode);
        }

        public static OperationResult<T> Failure(string errorMessage, int statusCode = 400)
        {
            return new OperationResult<T>(false, default(T), errorMessage, statusCode);
        }
    }
}

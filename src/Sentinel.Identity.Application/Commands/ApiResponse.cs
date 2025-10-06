namespace Sentinel.Identity.Application.Commands;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public string? ErrorCode { get; set; }
    public Dictionary<string, List<string>>? ValidationErrors { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string RequestId { get; set; } = Guid.NewGuid().ToString();

    public static ApiResponse<T> SuccessResponse(T data, string message = "Operation completed successfully") => new()
    {
        Success = true,
        Message = message,
        Data = data
    };

    public static ApiResponse<T> ErrorResponse(string message, string errorCode = "ERROR") => new()
    {
        Success = false,
        Message = message,
        ErrorCode = errorCode
    };

    public static ApiResponse<T> ValidationErrorResponse(Dictionary<string, List<string>> validationErrors) => new()
    {
        Success = false,
        Message = "Validation errors occurred",
        ErrorCode = "VALIDATION_ERROR",
        ValidationErrors = validationErrors
    };
}
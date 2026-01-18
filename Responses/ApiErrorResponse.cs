namespace Products.Api.Responses;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string TraceId { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }
}

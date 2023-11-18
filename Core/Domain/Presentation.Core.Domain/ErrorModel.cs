namespace Presentation.Core.Domain;

public class ErrorModel {
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorMessageKey { get; set; }
    public IDictionary<string, string>? ErrorMessageParam { get; set; }
}
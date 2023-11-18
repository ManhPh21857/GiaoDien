namespace Presentation.Core.Domain;

public class ResponseBaseModel<T> {
    public bool Status { get; set; }
    public T? Data { get; set; }
}
namespace Presentation.Core.Domain.Authentication;

public record Token(string Value) {
    public string Value { get; private set; } = Validate(Value);

    private static string Validate(string value) => value;
}
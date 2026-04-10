using ErrorOr;

namespace Domain.Tests.Utils;

public record ErrorBundle<T>(T Value, List<Error> ExpectedErrors)
{
    public override string ToString() =>
        $"Value: '{(Value is null ? "null" : Value.ToString())}' ({ExpectedErrors.Count} errors)";
}

public record StringErrorBundle(string Value, List<Error> ExpectedErrors)
    : ErrorBundle<string>(Value, ExpectedErrors);
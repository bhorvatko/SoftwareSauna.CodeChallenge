using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class FakeTurnException
    : Exception
{
    internal FakeTurnException(MapField fakeTurnField)
        : base($"A fake turn has been detected at coordinates {fakeTurnField.Coordinates}")
    {
    }
}

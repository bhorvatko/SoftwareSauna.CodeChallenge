namespace SoftwareSauna.CodeChallenge.MapFields;

public struct FieldCoordinates
{
    public uint X { get; init; }
    public uint Y { get; init; }

    public override string ToString() => $"{X}:{Y}";
}

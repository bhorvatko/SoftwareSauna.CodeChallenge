﻿using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.MapFields;

public class EndMapField
    : MapField
{
    public static readonly IEnumerable<char> Identifiers = new[] { 'x' };

    internal EndMapField(FieldCoordinates coordinates, MapField? fieldToTheLeft, MapField? fieldAbove)
        : base(Identifiers.Single(), coordinates, fieldToTheLeft, fieldAbove)
    {
    }

    internal override bool CanBeTraversedThroughFromDirection(Direction direction) => true;
}

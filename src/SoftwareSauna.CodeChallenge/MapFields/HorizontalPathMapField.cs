﻿using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.MapFields;

public class HorizontalPathMapField
    : MapField
{
    public static readonly IEnumerable<char> Identifiers = new[] { '-' };

    internal HorizontalPathMapField(FieldCoordinates coordinates, MapField? fieldToTheLeft, MapField? fieldAbove)
        : base(Identifiers.Single(), coordinates, fieldToTheLeft, fieldAbove)
    {
    }

    internal override bool CanBeTraversedThroughFromDirection(Direction direction) =>
        direction is Direction.Right or Direction.Left
            || GetNextField(direction)?.CanBeTraversedThroughFromDirection(direction) == true;
}

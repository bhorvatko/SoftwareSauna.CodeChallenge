﻿using SoftwareSauna.CodeChallenge.Core.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class MultiplePathsException
    : DomainArgumentException
{
    internal MultiplePathsException(
        MapField field,
        IEnumerable<FieldCoordinates> potentialPathCoordinates)
        : base($"Multiple possible paths for field with character '{field.Value}' at {field.Coordinates} have been detected " +
            $"at coordinates {string.Join(',', potentialPathCoordinates.Select(c => c.ToString()))}")
    {
    }
}

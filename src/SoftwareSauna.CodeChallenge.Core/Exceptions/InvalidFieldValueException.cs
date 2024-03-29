﻿using SoftwareSauna.CodeChallenge.Core.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class InvalidFieldValueException
    : DomainArgumentException
{
    internal InvalidFieldValueException(char fieldValue, FieldCoordinates coordinates)
        : base($"'{fieldValue}' is not a valid field value (located at coordinates {coordinates}).")
    {
    }
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace ReactWebApiSample2.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
[ExcludeFromCodeCoverage]
public sealed class AsStringAttribute
    : Attribute
{
}

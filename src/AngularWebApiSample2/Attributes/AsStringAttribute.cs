using System;
using System.Diagnostics.CodeAnalysis;

namespace AngularWebApiSample2.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
[ExcludeFromCodeCoverage]
public sealed class AsStringAttribute
    : Attribute
{
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace ReactWebApiSample.Attributes
{
    [AttributeUsage(AttributeTargets.Enum)]
    [ExcludeFromCodeCoverage]
    public sealed class AsStringAttribute
        : Attribute
    {
    }
}

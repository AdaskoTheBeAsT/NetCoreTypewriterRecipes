using System;
using System.Diagnostics.CodeAnalysis;

namespace AngularWebApiSample.Attributes
{
    [AttributeUsage(AttributeTargets.Enum)]
    [ExcludeFromCodeCoverage]
    public sealed class AsStringAttribute
        : Attribute
    {
    }
}

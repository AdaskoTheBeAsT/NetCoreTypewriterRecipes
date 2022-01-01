using System;
using System.Diagnostics.CodeAnalysis;

namespace AngularWebApiSample.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
[ExcludeFromCodeCoverage]
public sealed class CustomNameAttribute : Attribute
{
    public CustomNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace AngularWebApiSample2.Attributes;

[AttributeUsage(AttributeTargets.Field)]
[ExcludeFromCodeCoverage]
public sealed class LabelForEnumAttribute
    : Attribute
{
    public LabelForEnumAttribute(string label)
    {
        Label = label;
    }

    public string Label { get; }
}

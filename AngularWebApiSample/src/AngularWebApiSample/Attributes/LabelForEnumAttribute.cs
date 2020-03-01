using System;
using System.Diagnostics.CodeAnalysis;

namespace AngularWebApiSample.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    [ExcludeFromCodeCoverage]
    public class LabelForEnumAttribute
        : Attribute
    {
        public LabelForEnumAttribute(string label)
        {
            Label = label;
        }

        public string Label { get; }
    }
}

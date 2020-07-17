using System;
using System.Diagnostics.CodeAnalysis;

namespace AngularWebApiSample.Attributes
{
    [AttributeUsage(AttributeTargets.Enum)]
    [ExcludeFromCodeCoverage]
    public class AsStringAttribute
        : Attribute
    {
        public AsStringAttribute()
        {
        }
    }
}

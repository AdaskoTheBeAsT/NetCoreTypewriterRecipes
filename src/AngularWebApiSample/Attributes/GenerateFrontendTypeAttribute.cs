using System;
using System.Diagnostics.CodeAnalysis;

namespace AngularWebApiSample.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, Inherited = false)]
    [ExcludeFromCodeCoverage]
    public sealed class GenerateFrontendTypeAttribute : Attribute
    {
        public GenerateFrontendTypeAttribute()
        {
        }

        public GenerateFrontendTypeAttribute(string dllName)
        {
            DllName = dllName;
        }

        public string? DllName { get; }
    }
}

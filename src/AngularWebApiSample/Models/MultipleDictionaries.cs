using System;
using System.Collections.Generic;
using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models;

[GenerateFrontendType]
public class MultipleDictionaries
{
    public IDictionary<string, string> Dictionary1 { get; set; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public IDictionary<string, string>? Dictionary2 { get; set; }

    public IDictionary<string, string?> Dictionary3 { get; set; } =
        new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

    public IDictionary<string, string?>? Dictionary4 { get; set; }
}

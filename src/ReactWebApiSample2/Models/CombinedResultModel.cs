using System.Collections.Generic;
using ReactWebApiSample2.Attributes;

namespace ReactWebApiSample2.Models;

[GenerateFrontendType]
public class CombinedResultModel
{
    public int Id { get; set; }

#pragma warning disable MA0016 // Prefer return collection abstraction instead of implementation
    public List<ComplexBaseModel> SampleList { get; internal set; } = new List<ComplexBaseModel>();
#pragma warning restore MA0016 // Prefer return collection abstraction instead of implementation
}

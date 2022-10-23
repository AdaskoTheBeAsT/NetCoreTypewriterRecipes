using AngularWebApiSample2.Attributes;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexCModel : ComplexBaseModel
{
    public bool IsActive { get; set; }
}

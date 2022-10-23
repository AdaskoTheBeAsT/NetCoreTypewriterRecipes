using AngularWebApiSample2.Attributes;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexBModel : ComplexBaseModel
{
    public bool IsActive { get; set; }
}

using ReactWebApiSample2.Attributes;

namespace ReactWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexBModel : ComplexBaseModel
{
    public bool IsActive { get; set; }
}

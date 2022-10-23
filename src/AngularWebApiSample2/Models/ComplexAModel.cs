using AngularWebApiSample2.Attributes;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexAModel : ComplexBaseModel
{
    public string? Text { get; set; }
}

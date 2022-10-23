using ReactWebApiSample2.Attributes;

namespace ReactWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexAModel : ComplexBaseModel
{
    public string? Text { get; set; }
}

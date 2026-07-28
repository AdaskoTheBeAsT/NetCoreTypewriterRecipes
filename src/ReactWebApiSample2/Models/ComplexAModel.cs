using AdaskoTheBeAsT.Typewriter.Annotations;

namespace ReactWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexAModel : ComplexBaseModel
{
    public string? Text { get; set; }
}

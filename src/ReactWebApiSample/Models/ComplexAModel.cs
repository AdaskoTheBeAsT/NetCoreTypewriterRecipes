using AdaskoTheBeAsT.Typewriter.Annotations;

namespace ReactWebApiSample.Models;

[GenerateFrontendType]
public class ComplexAModel : ComplexBaseModel
{
    public string? Text { get; set; }
}

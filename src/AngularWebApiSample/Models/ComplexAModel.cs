using AdaskoTheBeAsT.Typewriter.Annotations;

namespace AngularWebApiSample.Models;

[GenerateFrontendType]
public class ComplexAModel : ComplexBaseModel
{
    public string? Text { get; set; }
}

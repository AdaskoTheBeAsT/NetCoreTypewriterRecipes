using AdaskoTheBeAsT.Typewriter.Annotations;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexAModel : ComplexBaseModel
{
    public string? Text { get; set; }
}

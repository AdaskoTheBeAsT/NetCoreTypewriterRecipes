using AdaskoTheBeAsT.Typewriter.Annotations;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexCModel : ComplexBaseModel
{
    public bool IsActive { get; set; }
}

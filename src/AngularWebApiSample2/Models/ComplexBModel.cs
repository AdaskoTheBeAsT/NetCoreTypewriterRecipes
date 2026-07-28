using AdaskoTheBeAsT.Typewriter.Annotations;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
public class ComplexBModel : ComplexBaseModel
{
    public bool IsActive { get; set; }
}

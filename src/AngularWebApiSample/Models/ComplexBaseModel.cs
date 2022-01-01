using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models;

[GenerateFrontendType]
public class ComplexBaseModel
{
    public int Id { get; set; }
}

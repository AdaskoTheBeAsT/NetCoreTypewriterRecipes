using System.Text.Json.Serialization;
using AngularWebApiSample2.Attributes;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$case")]
[JsonDerivedType(typeof(ComplexAModel), nameof(ComplexAModel))]
[JsonDerivedType(typeof(ComplexBModel), "BModel")]
[JsonDerivedType(typeof(ComplexCModel), 3)]
public class ComplexBaseModel
{
    public int Id { get; set; }
}

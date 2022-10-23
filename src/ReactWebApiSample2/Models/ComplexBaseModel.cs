using System.Text.Json.Serialization;
using ReactWebApiSample2.Attributes;

namespace ReactWebApiSample2.Models;

[GenerateFrontendType]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$case")]
[JsonDerivedType(typeof(ComplexAModel), nameof(ComplexAModel))]
[JsonDerivedType(typeof(ComplexBModel), "BModel")]
[JsonDerivedType(typeof(ComplexCModel), 3)]
public class ComplexBaseModel
{
    public int Id { get; set; }
}

using System.Text.Json.Serialization;
using AngularWebApiSample2.Attributes;

namespace AngularWebApiSample2.Models;

[AsString]
[JsonConverter(typeof(JsonStringEnumConverter))]
[GenerateFrontendType]
public enum SecondSet
{
    ValA,
    ValB,

    [LabelForEnum("ValC")]
    SomeOtherVal,
}

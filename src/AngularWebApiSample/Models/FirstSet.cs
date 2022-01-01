using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models;

[GenerateFrontendType]
public enum FirstSet
{
    ValA,
    ValB,

    [LabelForEnum("ValC")]
    SomeOtherVal,
}

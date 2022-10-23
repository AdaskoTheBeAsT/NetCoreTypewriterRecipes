using ReactWebApiSample2.Attributes;

namespace ReactWebApiSample2.Models;

[AsString]
[GenerateFrontendType]
public enum SecondSet
{
    ValA,
    ValB,

    [LabelForEnum("ValC")]
    SomeOtherVal,
}

using AdaskoTheBeAsT.Typewriter.Annotations;

namespace ReactWebApiSample.Models;

[AsString]
[GenerateFrontendType]
public enum SecondSet
{
    ValA,
    ValB,

    [LabelForEnum("ValC")]
    SomeOtherVal,
}

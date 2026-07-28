using AdaskoTheBeAsT.Typewriter.Annotations;

namespace AngularWebApiSample.Models;

[AsString]
[GenerateFrontendType]
public enum SecondSet
{
    ValA,
    ValB,

    [LabelForEnum("ValC")]
    SomeOtherVal,
}

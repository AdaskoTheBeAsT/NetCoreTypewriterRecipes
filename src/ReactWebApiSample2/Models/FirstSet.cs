using AdaskoTheBeAsT.Typewriter.Annotations;

namespace ReactWebApiSample2.Models;

[GenerateFrontendType]
public enum FirstSet
{
    ValA,
    ValB,

    [LabelForEnum("ValC")]
    SomeOtherVal,
}

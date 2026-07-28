using AdaskoTheBeAsT.Typewriter.Annotations;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
public enum FirstSet
{
    ValA,
    ValB,

    [LabelForEnum("ValC")]
    SomeOtherVal,
}

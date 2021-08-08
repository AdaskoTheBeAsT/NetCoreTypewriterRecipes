using ReactWebApiSample.Attributes;

namespace ReactWebApiSample.Models
{
    [GenerateFrontendType]
    public enum FirstSet
    {
        ValA,
        ValB,

        [LabelForEnum("ValC")]
        SomeOtherVal,
    }
}

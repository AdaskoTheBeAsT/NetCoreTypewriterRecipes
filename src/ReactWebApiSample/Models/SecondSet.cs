using ReactWebApiSample.Attributes;

namespace ReactWebApiSample.Models
{
    [AsString]
    [GenerateFrontendType]
    public enum SecondSet
    {
        ValA,
        ValB,

        [LabelForEnum("ValC")]
        SomeOtherVal,
    }
}

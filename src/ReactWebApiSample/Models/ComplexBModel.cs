using ReactWebApiSample.Attributes;

namespace ReactWebApiSample.Models
{
    [GenerateFrontendType]
    public class ComplexBModel : ComplexBaseModel
    {
        public bool IsActive { get; set; }
    }
}

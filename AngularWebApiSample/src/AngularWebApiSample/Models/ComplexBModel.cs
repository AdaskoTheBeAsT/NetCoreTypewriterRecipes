using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models
{
    [GenerateFrontendType]
    public class ComplexBModel : ComplexBaseModel
    {
        public bool IsActive { get; set; }
    }
}

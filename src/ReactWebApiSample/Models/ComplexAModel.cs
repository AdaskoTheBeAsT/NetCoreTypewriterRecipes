using ReactWebApiSample.Attributes;

namespace ReactWebApiSample.Models
{
    [GenerateFrontendType]
    public class ComplexAModel : ComplexBaseModel
    {
        public string? Text { get; set; }
    }
}

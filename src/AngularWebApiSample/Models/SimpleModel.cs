using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models
{
    [GenerateFrontendType]
    public class SimpleModel
    {
        public int Num { get; set; }

        public string? Text { get; set; }

        public bool IsOk { get; set; }
    }
}

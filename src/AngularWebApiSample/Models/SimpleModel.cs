using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models
{
    [GenerateFrontendType]
    public record SimpleModel(
        int Num,
        string? Text,
        bool IsOk);
}

using AngularWebApiSample2.Attributes;

namespace AngularWebApiSample2.Models;

[GenerateFrontendType]
public record SimpleModel(
    int Num,
    string? Text,
    bool IsOk,
    FirstSet FirstSet,
    SecondSet SecondSet);

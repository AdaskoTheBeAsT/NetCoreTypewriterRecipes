using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models;

[GenerateFrontendType]
public static class KnownContactTypes
{
    public const string Owner = "Owner";
    public const string Pharmacy = "Pharmacy";
    public const string Billing = "Billing";
    public const string Manager = "Retail Manager";
    public const string Bookkeeper = "Bookkeeper";
}

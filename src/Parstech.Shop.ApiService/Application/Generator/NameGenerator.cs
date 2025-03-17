namespace Parstech.Shop.ApiService.Application.Generator;

public static class NameGenerator
{
    public static string GenerateUniqCode()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}
using Parstech.Shop.ApiService.Application.Convertor;

namespace Parstech.Shop.ApiService.Application.Generator;

public static class CodeGenerator
{
    public static string GenerateUniqCode()
    {
        string? code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        return $"PT-{code}";
    }

    public static string GenerateOrderCode()
    {
        DateTime time = DateTime.Now;
        string? Shamsi = time.ToShamsi();
        string[] Date = Shamsi.Split('/');

        string? code = $"{Date[0]}{Date[1]}{Date[2]}{time.Minute}{time.Millisecond}";

        return $"PT-{code}";
    }
}
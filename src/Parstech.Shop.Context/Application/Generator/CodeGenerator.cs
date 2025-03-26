using Parstech.Shop.Context.Application.Convertor;

namespace Parstech.Shop.Context.Application.Generator;

public static class CodeGenerator
{
    public static string GenerateUniqCode()
    {
        var code= Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        return $"PT-{code}";
    }
    public static string GenerateOrderCode()
    {
        var time = DateTime.Now;
        var Shamsi=time.ToShamsi();
        string[] Date = Shamsi.Split('/');
            
        var code = $"{Date[0]}{Date[1]}{Date[2]}{time.Minute}{time.Millisecond}";
            
        return $"PT-{code}";
    }
}
namespace Parstech.Shop.ApiService.Application.Calculator;

public static class PersentCalculator
{
    public static long PersentCalculatorByPrice(long price, int Persent)
    {
        double X = (double)(price / 100);
        double y = (double)(X * Persent);
        long Result = (long)Math.Ceiling(y);

        return Result;
    }

    public static long BaseCalculatorByPrice(long price)
    {
        double X = price / 110;
        long XResult = (long)Math.Ceiling(X);
        double y = (double)(XResult * 100);
        long Result = (long)Math.Ceiling(y);

        return Result;
    }

    public static long RahkaranCalvulatorByPrice(long price)
    {
        double X = (double)(price / 0.10);
        //long XResult = (long)Math.Ceiling(X);
        //var y = (double)(XResult * 100);
        long Result = (long)Math.Ceiling(X);
        //var withoutMath = string.Format("{0:n0}", X);
        return Result;
    }
}
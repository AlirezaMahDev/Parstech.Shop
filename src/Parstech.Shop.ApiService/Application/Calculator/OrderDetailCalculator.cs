namespace Parstech.Shop.ApiService.Application.Calculator;

public static class OrderDetailCalculator
{
    public static long GetDetailSum(long Price, int Count)
    {
        return Price * Count;
    }

    public static long GetTotal(long sum, long Tax, long Discount)
    {
        return sum + Tax - Discount;
    }
}
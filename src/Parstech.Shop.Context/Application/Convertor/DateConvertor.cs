using System.Globalization;

namespace Parstech.Shop.Context.Application.Convertor;

public static class DateConvertor
{
    public static string ToShamsi(this DateTime value)
    {
        PersianCalendar pc=new();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00");
    }
}
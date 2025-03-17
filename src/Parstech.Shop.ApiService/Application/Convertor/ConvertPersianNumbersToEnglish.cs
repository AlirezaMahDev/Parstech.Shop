namespace Parstech.Shop.ApiService.Application.Convertor;

public static class ConvertPersianNumbersToEnglish
{
    public static string ToEnglishNumber(string input)
    {
        string englishNumbers = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
            {
                englishNumbers += char.GetNumericValue(input, i);
            }
            else
            {
                englishNumbers += input[i].ToString();
            }
        }

        return englishNumbers;
    }
}
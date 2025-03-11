using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Convertor
{
    public static class ConvertPersianNumbersToEnglish
    {
        public static string ToEnglishNumber(string input)
        {
            string englishNumbers = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
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
}

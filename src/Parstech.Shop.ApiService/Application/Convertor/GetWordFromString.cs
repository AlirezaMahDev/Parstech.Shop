using System.Text.RegularExpressions;

namespace Parstech.Shop.ApiService.Application.Convertor;

public static class GetWordFromString
{
    public static string[] GetWords(string input)
    {
        MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

        IEnumerable<string> words = from m in matches.Cast<Match>()
            where !string.IsNullOrEmpty(m.Value)
            select TrimSuffix(m.Value);

        return words.ToArray();
    }

    public static string TrimSuffix(string word)
    {
        int apostropheLocation = word.IndexOf('\'');
        if (apostropheLocation != -1)
        {
            word = word.Substring(0, apostropheLocation);
        }

        return word;
    }
}
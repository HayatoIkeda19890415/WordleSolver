// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System.Text.RegularExpressions;

const string PATH_DICT = @".\Dictionary\";
Dictionary<char, String[]> dictionaries = new Dictionary<char, String[]>();
List<char> ng_Chars = new List<char>();

string word = guessWord(string.Empty, string.Empty);
Console.WriteLine(word);
string? result = Console.ReadLine();
while (result != "22222")
{
    word = guessWord(result, word);
    Console.WriteLine(word);
    result = Console.ReadLine();
}

string guessWord(string? input, string latestWord)
{
    if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(latestWord) || !Regex.Match(input, @"[0-9]{5}").Success)
    {
        return pickRandomWord();
    }
    return searchWord(input, latestWord);
}

string pickRandomWord()
{
    var rand = new Random();
    var randInt = rand.Next('a', ('z' + 1));
    var randChar = (char)randInt;
    string[]? lines;
    if (!dictionaries.ContainsKey(randChar))
    {
        var reader = new StreamReader(File.OpenRead(PATH_DICT + randChar + ".txt"));
        lines = reader.ReadToEnd().Split(Environment.NewLine);
        dictionaries.Add(randChar, lines);
    }
    else
    {
        lines = dictionaries.GetValueOrDefault(randChar);
    }
    if (lines == null)
    {
        throw new Exception("Coudn't find " + randChar + ".txt dictionary");
    }
    var onlyAlphabet = @"[a-z]{5}";
    var randLineNum = new Random().Next(0, lines.Count());
    var match = Regex.Match(lines[randLineNum], onlyAlphabet);
    while (!match.Success)
    {
        randLineNum = new Random().Next(0, lines.Count());
        match = Regex.Match(lines[randLineNum], onlyAlphabet);
    }
    return lines[randLineNum];
}

const char FAILURE = '0';
const char CHARONLY = '1';
const char SUCCESS = '2';
string searchWord(string input, string latestWord)
{
    char[] resultArray = input.ToCharArray();
    char[] wordArray = latestWord.ToCharArray();
    foreach (char c in resultArray)
    {
        if (c == FAILURE)
        {
            dictionaries.Remove(c);
            ng_Chars.Add(c);
        }
        else if (c == CHARONLY)
        {

        }
        else if (c == SUCCESS)
        {

        }
    }
    return pickRandomWord();
}
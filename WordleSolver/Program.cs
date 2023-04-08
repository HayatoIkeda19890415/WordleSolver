// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System.Text.RegularExpressions;

const string PATH_DICT = @".\Dictionary\words.txt";
List<char> mustChars = new List<char>();
var initCharCondition = @"[abcdifghijklmnopqrstuvwxyz]";
var char1cond = initCharCondition;
var char2cond = initCharCondition;
var char3cond = initCharCondition;
var char4cond = initCharCondition;
var char5cond = initCharCondition;
var searchcond = @"^" + char1cond + char2cond + char3cond + char4cond + char5cond + "$";
var reader = new StreamReader(File.OpenRead(PATH_DICT));
string[]? lines = reader.ReadToEnd().Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
if (lines == null)
{
    throw new Exception("Coudn't find " + PATH_DICT);
}

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
    var randLineNum = new Random().Next(0, lines.Count());
    var match = Regex.Match(lines[randLineNum], searchcond);
    while (!match.Success)
    {
        randLineNum = new Random().Next(0, lines.Count());
        match = Regex.Match(lines[randLineNum], searchcond);
    }
    return lines[randLineNum];
}

string searchWord(string input, string latestWord)
{
    char[] resultArray = input.ToCharArray();
    char[] wordArray = latestWord.ToCharArray();
    searchcond = modSearchCondition(resultArray, wordArray);

    foreach (var item in lines)
    {
        if (Regex.Match(item, searchcond).Success)
        {
            var mustChar_isContained = true;
            foreach (var mustchar in mustChars)
            {
                if (!item.Contains(mustchar))
                {
                    mustChar_isContained = false;
                    break;
                }
            }
            if (mustChar_isContained)
            {
                return item;
            }
        }
    }
    throw new Exception("Coudn't find the word.");
}

string modSearchCondition(char[] resultArray, char[] wordArray)
{
    const char FAILURE = '0';
    const char CHARONLY = '1';
    const char SUCCESS = '2';

    for (int i = 0; i < resultArray.Length; i++)
    {
        var charString = wordArray[i].ToString();
        char c = resultArray[i];
        if (c == FAILURE)
        {
            char1cond = char1cond.Replace(charString, "");
            char2cond = char2cond.Replace(charString, "");
            char3cond = char3cond.Replace(charString, "");
            char4cond = char4cond.Replace(charString, "");
            char5cond = char5cond.Replace(charString, "");
        }
        else if (c == CHARONLY)
        {
            mustChars.Add(wordArray[i]);
            if (i == 0)
            {
                char1cond = char1cond.Replace(charString, "");
            }
            else if (i == 1)
            {
                char2cond = char2cond.Replace(charString, "");
            }
            else if (i == 2)
            {
                char3cond = char3cond.Replace(charString, "");
            }
            else if (i == 3)
            {
                char4cond = char3cond.Replace(charString, "");
            }
            else if (i == 4)
            {
                char5cond = char3cond.Replace(charString, "");
            }
        }
        else if (c == SUCCESS)
        {
            if (i == 0)
            {
                char1cond = charString;
            }
            else if (i == 1)
            {
                char2cond = charString;
            }
            else if (i == 2)
            {
                char3cond = charString;
            }
            else if (i == 3)
            {
                char4cond = charString;
            }
            else if (i == 4)
            {
                char5cond = charString;
            }
        }
    }
    return @"^" + char1cond + char2cond + char3cond + char4cond + char5cond + "$";
}
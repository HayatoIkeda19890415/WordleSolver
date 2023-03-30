// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

const string PATH_DICT = @".\Dictionary\";
Dictionary<char, String[]> dictionaries = new Dictionary<char, String[]>();
string word = guessWord(null);
Console.WriteLine(word);
string? result = Console.ReadLine();
while (result != "22222")
{
    word = guessWord(result);
    Console.WriteLine(word);
    result = Console.ReadLine();
}

string guessWord(string? input)
{
    if (input == null)
    {
        return pickRandomWord();
    }
    return pickRandomWord();
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
    var match = System.Text.RegularExpressions.Regex.Match(lines[randLineNum], onlyAlphabet);
    while (!match.Success)
    {
        randLineNum = new Random().Next(0, lines.Count());
        match = System.Text.RegularExpressions.Regex.Match(lines[randLineNum], onlyAlphabet);
    }
    return lines[randLineNum];
}
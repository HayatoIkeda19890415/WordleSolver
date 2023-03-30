// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


string? input = null;
if (args.Length != 0)
{
    input = args[0];
}

string word;
if (input == null)
{
    word = suggestWord();
}
else
{
    word = guessWord(input);
}

Console.WriteLine(word);

string suggestWord()
{
    return string.Empty;
}

string guessWord(string input)
{
    return string.Empty;
}
// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


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
    return "wordle";
}

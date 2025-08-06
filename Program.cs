// ***
// CeleraChallenge - Test Example
// ***
using CelaraChallenge;


string path = "c:/temp";
string matrixFileName = Path.Combine(path, "charMatrix.txt");
string wordStreamFileName = Path.Combine(path, "wordStream.txt");


var matrix = File.ReadLines(matrixFileName);
var wordStream = File.ReadLines(wordStreamFileName);

var wordFinder = new WordFinder(matrix);
var result = wordFinder.Find(wordStream);

if (result.Count() > 0)
{
    Console.WriteLine("The most repeted words of the stream are: ");
    foreach (var word in result)
        Console.WriteLine(word);
}
else
{
    Console.WriteLine("No words found on the matrix");
}

Console.WriteLine("Hello, World!");

var fileLines = FileReader.FileReader.FileToArray(@"C:\Users\beulu\source\repos\Day3\day3RealInput.txt");

var alphabetTable = CreateAlphabetTable();
var sameLettersPart1 = FindSameLetters(fileLines);

Console.WriteLine($"Total score {CalcPriorities(sameLettersPart1, alphabetTable)}");

var sameLettersPart2 = FindSameLettersGroup(fileLines);

Console.WriteLine($"Total score {CalcPriorities(sameLettersPart2, alphabetTable)}");
Console.ReadLine();

public static partial class Program
{
    public static List<char> FindSameLettersGroup(string[] fileLines)
    {
        var skip = 0;
        var take = 3;
        var count = 0;

        var sameLetters = new List<char>();

        while (count != fileLines.Length)
        {
            var group = fileLines.Skip(skip).Take(take).ToArray();
            var intersection = group[0].Intersect(group[1]).Intersect(group[2]).First();
            sameLetters.Add(intersection);

            skip += take;
            count += take;
        }
        return sameLetters;
    }

    public static List<char> FindSameLetters(string[] fileLines)
    {
        var sameLetters = new List<char>();

        foreach (var line in fileLines)
        {
            var firstHalf = line.Substring(0, line.Length / 2);
            var secondHalf = line.Substring(firstHalf.Length);
            var intersection = firstHalf.Intersect(secondHalf).First();
            sameLetters.Add(intersection);
        }
        return sameLetters;
    }
    public static Dictionary<char, int> CreateAlphabetTable()
    {
        char[] lowercaseAlphabet = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();
        char[] uppercaseAlphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

        var alphabet = lowercaseAlphabet.Concat(uppercaseAlphabet).ToArray();
        var numbers = Enumerable.Range(1, 52).ToArray();

        var result = alphabet.Zip(numbers, (letter, number) => new { letter, number })
            .ToDictionary(val => val.letter, val => val.number);

        return result;
    }

    public static int CalcPriorities(List<char> letters, Dictionary<char, int> alphabet)
    {
        var result = letters.Sum(x => alphabet[x]);
        return result;
    }
}
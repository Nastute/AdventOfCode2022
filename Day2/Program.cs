var fileLines = FileReader.FileReader.FileToArray(@"C:\Users\beulu\source\repos\Day2\day2RealInput.txt");

var result = CalcPart1(fileLines);

Console.WriteLine($"Total score {result.Sum()}");

result = CalcPart2(fileLines);

Console.WriteLine($"Total score {result.Sum()}");
Console.ReadLine();

public enum OppRPS
{
    A = 1, //Rock
    B = 2, //Paper
    C = 3, //Scissors
}

public enum YourRPS
{
    X = 0, //Rock
    Y = 2, //Paper
    Z = 3, //Scissors
}

public enum Result
{
    X = 0, //Lose
    Y = 3, //Draw
    Z = 6, //win
}

public static partial class Program
{
    public const int Draw = 3;
    public const int Win = 6;
    public const int Lose = 0;
    public static List<int> CalcPart1(string[] fileLines)
    {
        var rounds = new List<int>();

        foreach (var line in fileLines)
        {
            var values = line.Split(' ');
            var opponents = (int)Enum.Parse(typeof(OppRPS), values[0]);
            var yours = (int)Enum.Parse(typeof(YourRPS), values[1]);
            var diff = yours - opponents;

            if (yours == opponents)
            {
                rounds.Add(yours + Draw);
                continue;
            }

            if (diff == 1 || diff == -2)
            {
                rounds.Add(yours + Win);
                continue;
            }

            rounds.Add(yours + Lose);
        }
        return rounds;
    }

    //Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock.
    // 1 3, 3 2, 2 1
    public static List<int> CalcPart2(string[] fileLines)
    {
        var rounds = new List<int>();

        foreach (var line in fileLines)
        {
            var values = line.Split(' ');
            var opponents = (int)Enum.Parse(typeof(OppRPS), values[0]);
            var result = (int)Enum.Parse(typeof(Result), values[1]);

            if (result == Draw)
            {
                rounds.Add(opponents + Draw);
                continue;
            }

            if (result == Win)
            {
                var temp = (1 + opponents) % 3;
                if (temp == 0)
                {
                    temp = 3;
                }
                rounds.Add(temp + Win);
                continue;
            }

            if (result == Lose)
            {
                switch (opponents)
                {
                    case 1: rounds.Add(3 + Lose); break;
                    case 2: rounds.Add(1 + Lose); break;
                    case 3: rounds.Add(2 + Lose); break;
                    default:
                        break;
                }
                continue;
            }

        }
        return rounds;
    }
}
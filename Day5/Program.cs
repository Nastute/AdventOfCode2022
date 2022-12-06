using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;

var assignments = FileReader.FileReader.FileToArray(@"..\..\..\day5Example.txt").ToList();

var stackedBoxes = assignments.Take(assignments.FindIndex(x => string.IsNullOrEmpty(x))).ToList();
var stacks = ParseStackedBoxes(stackedBoxes);

var instructions = assignments.TakeLast(assignments.Count - stackedBoxes.Count() - 2);

//var finalStack = MoveCrates9000(instructions, stacks);

//foreach (var stack in stacks)
//{
//    stack.TryPeek(out var crate);
//    Console.WriteLine($"{crate}");
//}

var finalStack = MoveCrates9001(instructions, stacks);

foreach (var stack in stacks)
{
    stack.TryPeek(out var crate);
    Console.WriteLine($"{crate}");
}
Console.ReadLine();

public static partial class Program
{
    public static List<Stack<string>> MoveCrates9000(IEnumerable<string> instructions, List<Stack<string>> stacks)
    {
        foreach (var instruction in instructions)
        {
            var moveableCrateNumber = GetMoveableNumber(instruction);
            var sourceStackNumber = GetSourceStackNumber(instruction);
            var destinationStackNumber = GetDestinationStackNumber(instruction);

            for (int i = 0; i < moveableCrateNumber; i++)
            {
                stacks[sourceStackNumber - 1].TryPop(out var crate);
                if (string.IsNullOrEmpty(crate))
                {
                    continue;
                }
                stacks[destinationStackNumber - 1].Push(crate);
            }
        }
        return stacks;
    }

    public static List<Stack<string>> MoveCrates9001(IEnumerable<string> instructions, List<Stack<string>> stacks)
    {
        foreach (var instruction in instructions)
        {
            var moveableCrateNumber = GetMoveableNumber(instruction);
            var sourceStackNumber = GetSourceStackNumber(instruction);
            var destinationStackNumber = GetDestinationStackNumber(instruction);
            var tempList = new List<string>();
            for (int i = 0; i < moveableCrateNumber; i++)
            {
                stacks[sourceStackNumber - 1].TryPop(out var crate);
                if (string.IsNullOrEmpty(crate))
                {
                    continue;
                }
                tempList.Add(crate);
            }
            tempList.Reverse();
            tempList.ForEach(x => stacks[destinationStackNumber - 1].Push(x));
        }
        return stacks;
    }

    public static List<Stack<string>> ParseStackedBoxes(List<string> stackedBoxes)
    {
        var listofStacks = new List<Stack<string>>();
        stackedBoxes.Reverse();
        var numberOfStacks = Convert.ToInt32(Regex.Split(stackedBoxes.First().Trim(), @"\D+").Last());
        listofStacks = CreateListOfStacks(numberOfStacks);
        stackedBoxes.RemoveAt(0);
        foreach (var line in stackedBoxes)
        {
            var stack = new Stack<string>();
            var values = line.Chunk(4).ToArray();
            for (int i = 0; i < values.Count(); i++)
            {
                var temp = values[i];
                var letter = string.Join("", temp).Trim().Where(x => Char.IsLetter(x));
                if (!letter.Any())
                {
                    continue;
                }
                listofStacks[i].Push(letter.First().ToString());
            }
        }
        return listofStacks;
    }

    public static int GetMoveableNumber(string instruction)
    {
        var temp = instruction.Split("move ").Last();
        var number = temp.IndexOf(" ");
        return Convert.ToInt32(temp.Substring(0, number));
    }

    public static int GetSourceStackNumber(string instruction)
    {
        var temp = instruction.Split("from ").Last();
        var number = temp.IndexOf(" ");
        return Convert.ToInt32(temp.Substring(0, number));
    }

    public static int GetDestinationStackNumber(string instruction)
    {
        var temp = instruction.Split("to ").Last();
        return Convert.ToInt32(temp.Substring(0));
    }

    public static List<Stack<string>> CreateListOfStacks(int numberOfStacks)
    {
        var list = new List<Stack<string>>();
        for (int i = 0; i < numberOfStacks; i++)
        {
            var stack = new Stack<string>();
            list.Add(stack);
        }
        return list;
    }
}
using static Program;

var fileLines = FileReader.FileReader.FileToArray(@"..\..\..\day11RealInput.txt").ToList();

var monkeys = new List<Monkey>();
while (fileLines.Any())
{
    var monkey = ParseMonkey(fileLines.Take(6).ToList());
    monkeys.Add(monkey);

    if (fileLines.Count() < 7)
    {
        fileLines.RemoveRange(0, 6);
        break;
    }
    fileLines.RemoveRange(0, 7);
}

for (int i = 0; i < 20; i++)
{
    SimulateRound(monkeys,1);
}

var temp = monkeys.OrderByDescending(x => x.inspectedCount).ToList();
long result = (long)temp[0].inspectedCount * (long)temp[1].inspectedCount;
Console.WriteLine(result);

for (int i = 1; i < 10001; i++)
{
    SimulateRound(monkeys,2);
}

Console.WriteLine($"ROUND ");
Console.WriteLine($"Monkey 0 inspected items {monkeys[0].inspectedCount} times.");
Console.WriteLine($"Monkey 1 inspected items {monkeys[1].inspectedCount} times.");
Console.WriteLine($"Monkey 2 inspected items {monkeys[2].inspectedCount} times.");
Console.WriteLine($"Monkey 3 inspected items {monkeys[3].inspectedCount} times.");

temp = monkeys.OrderByDescending(x => x.inspectedCount).ToList();
result = (long)temp[0].inspectedCount * (long)temp[1].inspectedCount;
Console.WriteLine(result);
Console.ReadLine();

public static partial class Program
{

    public static void SimulateRound(List<Monkey> monkeys, int part)
    {
        var factor = monkeys.Aggregate(1L, (f, m) => f * m.test);
        var count = 0;
        foreach (var monkey in monkeys)
        {
            var startNum = monkey.items.Count();
            foreach (var item in monkey.items)
            {
                monkey.inspectedCount++;

                long worry = GetWorry(item, monkey.operation, monkey.operationOperand);
                long temp;
                if (part == 1)
                {
                    temp = (long)Math.Round(Convert.ToDecimal(worry) / 3, MidpointRounding.ToZero);
                }
                else
                {
                    temp = worry % factor;
                }

                var dividable = temp % (long)monkey.test == 0;
                if (dividable)
                {
                    monkeys[monkey.ifTestTrue].items.Add(temp);
                }
                else
                {
                    monkeys[monkey.ifTestFalse].items.Add(temp);
                }
            }
            monkey.items = monkey.items.Skip(startNum).ToList();
            count++;
        }
    }

    public static long GetWorry(long worry, char operation, long operand)
    {
        long temp = 0;
        switch (operation)
        {
            case '+':
                if (operand == 0)
                {
                    temp = worry + worry;
                }
                else
                {
                    temp = worry + operand;
                }
                break;
            case '*':
                if (operand == 0)
                {
                    temp = worry * worry;
                }
                else
                {
                    temp = worry * operand;
                }
                break;
            default: break;
        }
        return temp;
    }

    public static Monkey ParseMonkey(List<string> monkeyLines)
    {
        var monkeyItems = monkeyLines[1].Substring("Starting items: ".Length + 2);
        var tempOperation = monkeyLines[2].Substring("Operation: new = old ".Length + 2);
        var operation = tempOperation[0];
        long operationOperand = tempOperation.Substring(2) == "old" ? 0 : long.Parse(tempOperation.Substring(2));
        var test = monkeyLines[3].Substring("Test: divisible by ".Length + 2);
        var ifTrue = monkeyLines[4].Last();
        var ifFalse = monkeyLines[5].Last();

        var monkey = new Monkey(monkeyItems, operation, operationOperand, int.Parse(test.ToString()), int.Parse(ifTrue.ToString()), int.Parse(ifFalse.ToString()));

        return monkey;
    }

    public class Monkey
    {
        public List<long> items;
        public char operation;
        public long operationOperand;
        public int test;
        public int ifTestTrue;
        public int ifTestFalse;
        public int inspectedCount;

        public Monkey(string items, char operation, long operationOperand, int test, int ifTestTrue, int ifTestFalse)
        {
            this.items = ParseItems(items);
            this.operation = operation;
            this.operationOperand = operationOperand;
            this.test = test;
            this.ifTestTrue = ifTestTrue;
            this.ifTestFalse = ifTestFalse;
        }

        private List<long> ParseItems(string items)
        {
            var temp = items.Split(',').Select(x => long.Parse(x));
            return new List<long>(temp.ToList());
        }
    }
}
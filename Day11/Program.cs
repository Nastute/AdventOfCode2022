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
    SimulateRound(monkeys);
}

var temp = monkeys.OrderByDescending(x => x.inspectedCount).ToList();
Console.WriteLine(temp[0].inspectedCount * temp[1].inspectedCount);
Console.ReadLine();

public static partial class Program
{

    public static void SimulateRound(List<Monkey> monkeys)
    {
        foreach (var monkey in monkeys)
        {
            var starItemsCount = monkey.items.Count();
            foreach (var item in monkey.items)
            {
                monkey.inspectedCount++;

                var worry = GetWorry(item, monkey.operation, monkey.operationOperand);
                var temp = Math.Round(Convert.ToDecimal(worry) / 3, MidpointRounding.ToZero);

                if (temp % monkey.test == 0)
                {
                    monkeys[monkey.ifTestTrue].items.Add(Convert.ToInt32(temp));
                }
                else
                {
                    monkeys[monkey.ifTestFalse].items.Add(Convert.ToInt32(temp));
                }
            }
            monkey.items.RemoveRange(0, starItemsCount);
        }
    }

    public static int GetWorry(int worry, char operation, int operand)
    {
        int temp = 0;
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
        var operationOperand = tempOperation.Substring(2) == "old" ? 0 : int.Parse(tempOperation.Substring(2));
        var test = monkeyLines[3].Substring("Test: divisible by ".Length + 2);
        var ifTrue = monkeyLines[4].Last();
        var ifFalse = monkeyLines[5].Last();

        var monkey = new Monkey(monkeyItems, operation, operationOperand, int.Parse(test.ToString()), int.Parse(ifTrue.ToString()), int.Parse(ifFalse.ToString()));

        return monkey;
    }

    public class Monkey
    {
        public List<int> items;
        public char operation;
        public int operationOperand;
        public int test;
        public int ifTestTrue;
        public int ifTestFalse;
        public int inspectedCount;

        public Monkey(string items, char operation, int operationOperand, int test, int ifTestTrue, int ifTestFalse)
        {
            this.items = ParseItems(items);
            this.operation = operation;
            this.operationOperand = operationOperand;
            this.test = test;
            this.ifTestTrue = ifTestTrue;
            this.ifTestFalse = ifTestFalse;
        }

        private List<int> ParseItems(string items)
        {
            var temp = items.Split(',').Select(x => int.Parse(x));
            return new List<int>(temp.ToList());
        }
    }
}
var fileLines = FileReader.FileReader.FileToArray(@"..\..\..\day21Example.txt").ToList();

var numberMonkeys = new Dictionary<string, long>();
var opMonkeys = new Dictionary<string, string>();

foreach (var line in fileLines)
{
    var monkeyName = line.Split(":")[0];
    if (long.TryParse(line.Split(":")[1], out var number))
    {
        numberMonkeys.Add(monkeyName, number);
    }
    else
    {
        opMonkeys.Add(monkeyName, line.Split(":")[1].Substring(1));
    }
}

GetOpMonkeyNumbers(numberMonkeys, opMonkeys);
Console.WriteLine($"root {numberMonkeys["root"]}");

void GetOpMonkeyNumbers(Dictionary<string, long> numberMonkeys, Dictionary<string, string> opMonkeys)
{
    while (opMonkeys.Any())
    {
        for (int i = 0; i < opMonkeys.Count; i++)
        {
            var splittedOp = opMonkeys.ElementAt(i).Value.Split(" ");
            long newValue = 0;
            var leftMonkey = splittedOp[0];
            var rightMonkey = splittedOp[2];
            var leftOp = numberMonkeys.ContainsKey(leftMonkey);
            var rightOp = numberMonkeys.ContainsKey(rightMonkey);

            if (leftOp && rightOp)
            {
                switch (splittedOp[1])
                {
                    case "+":
                        newValue = long.Parse(numberMonkeys[leftMonkey].ToString()) + long.Parse(numberMonkeys[rightMonkey].ToString());
                        break;
                    case "-":
                        newValue = long.Parse(numberMonkeys[leftMonkey].ToString()) - long.Parse(numberMonkeys[rightMonkey].ToString());
                        break;
                    case "*":
                        newValue = long.Parse(numberMonkeys[leftMonkey].ToString()) * long.Parse(numberMonkeys[rightMonkey].ToString());
                        break;
                    case "/":
                        newValue = long.Parse(numberMonkeys[leftMonkey].ToString()) / long.Parse(numberMonkeys[rightMonkey].ToString());
                        break;
                    default:
                        break;
                }

                numberMonkeys.Add(opMonkeys.ElementAt(i).Key, newValue);
                opMonkeys.Remove(opMonkeys.ElementAt(i).Key);
            }
        }
    }
}

public static partial class Program
{

}
Console.WriteLine("Hello, World!");
var elfsCalories = new List<int>();

var fileLines = File.ReadAllLines(@"C:\Users\beulu\source\repos\AdventOfCode\day1RealInput.txt");
var count = 0;
var calories = 0;
elfsCalories.Add(0);

foreach (var line in fileLines)
{
    if (string.IsNullOrEmpty(line))
    {
        elfsCalories.Add(0);
        count++;
        continue;
    }
    var success = int.TryParse(line, out calories);
    if (!success)
    {
        throw new Exception("Invalid calories provided");
    }
    elfsCalories[count] += calories;
}


Console.WriteLine($"Max calories has elf with {elfsCalories.OrderByDescending(x => x).Take(3).Sum()} calories");
Console.ReadLine();
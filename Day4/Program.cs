var assignments = FileReader.FileReader.FileToArray(@"..\..\..\day4RealInput.txt");

var pairRanges = ParsePairRanges(ParseAssignmentPairs(assignments));

var fullyContainedRanges = FindContainedRangesCount(pairRanges);

Console.WriteLine($"Total fully contained ranges count is {fullyContainedRanges}");

var overlappedRanges = FindOverlappingRangesCount(pairRanges);

Console.WriteLine($"Total overlapping ranges count is {overlappedRanges}");
Console.ReadLine();
public static partial class Program
{
    public static int FindOverlappingRangesCount(List<List<HashSet<int>>> pairRanges)
    {
        int count = 0;
        foreach (var pair in pairRanges)
        {
            var firstElve = pair.First();
            var secondElve = pair.Last();

            if (firstElve.Overlaps(secondElve))
            {
                count++;
            }
        }
        return count;
    }
    public static int FindContainedRangesCount(List<List<HashSet<int>>> pairRanges)
    {
        int count = 0;
        foreach (var pair in pairRanges)
        {
            var firstElve = pair.First();
            var secondElve = pair.Last();

            if (firstElve.IsSubsetOf(secondElve) || secondElve.IsSubsetOf(firstElve))
            {
                count++;
            }
        }
        return count;
    }

    public static List<string[]> ParseAssignmentPairs(string[] assignments)
    {
        var assignmentPairs = new List<string[]>();
        foreach (var assignmentPair in assignments)
        {
            var pair = assignmentPair.Split(',');
            assignmentPairs.Add(pair);
        }
        return assignmentPairs;
    }

    public static List<List<HashSet<int>>> ParsePairRanges(List<string[]> assignmentPairs)
    {
        var pairRanges = new List<List<HashSet<int>>>();
        foreach (var assignmentPair in assignmentPairs)
        {
            var pair = new List<HashSet<int>>();
            var firstElveRange = GetRangesList(assignmentPair.First().Split("-"));
            var secondElveRange = GetRangesList(assignmentPair.Last().Split("-"));

            pair.Add(firstElveRange);
            pair.Add(secondElveRange);
            pairRanges.Add(pair);
        }
        return pairRanges;
    }

    public static HashSet<int> GetRangesList(string[] rangeBounds)
    {
        var lowerBound = int.Parse(rangeBounds[0]);
        var upperBound = int.Parse(rangeBounds[1]);

        return Enumerable.Range(lowerBound, upperBound - lowerBound + 1).ToHashSet();
    }
}
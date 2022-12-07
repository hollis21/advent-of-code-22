class Day1 : IDay
{
    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        return Task.FromResult(parseCalories(inputLines).Max().ToString());
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var result = parseCalories(inputLines).OrderDescending().Take(3).Sum().ToString();
        return Task.FromResult(result);
    }

    private List<int> parseCalories(IEnumerable<string> inputLines) {
        List<int> acc = new();
        int curr = 0;
        foreach (var line in inputLines)
        {
            if (line == String.Empty) {
                acc.Add(curr);
                curr = 0;
            } else {
                curr += int.Parse(line);
            }
        }
        return acc;
    }
}
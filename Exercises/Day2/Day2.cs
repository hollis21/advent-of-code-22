public class Day2 : IDay
{
    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        var result = inputLines.Sum(line => GetScoreForLineA(line)).ToString();
        return Task.FromResult(result);
    }

    Dictionary<string, int> guessValueA = new() {
        {"X", 1},
        {"Y", 2},
        {"Z", 3}
    };
    Dictionary<string, Dictionary<string, int>> resultValueA = new() {
        {
            "X", new() { {"A", 3}, {"B", 0}, {"C", 6}}
        },
        {
            "Y", new() { {"A", 6}, {"B", 3}, {"C", 0}}
        },
        {
            "Z", new() { {"A", 0}, {"B", 6}, {"C", 3}}
        }
    };
    private int GetScoreForLineA(string line)
    {
        string p1 = line.Split(' ')[0];
        string p2 = line.Split(' ')[1];
        return guessValueA[p2] + resultValueA[p2][p1];
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var result = inputLines.Sum(line => GetScoreForLineB(line)).ToString();
        return Task.FromResult(result);
    }
    Dictionary<string, int> guessValueB = new() {
        {"X", 0},
        {"Y", 3},
        {"Z", 6}
    };
    Dictionary<string, Dictionary<string, int>> resultValueB = new() {
        {
            "X", new() { {"A", 3}, {"B", 1}, {"C", 2}}
        },
        {
            "Y", new() { {"A", 1}, {"B", 2}, {"C", 3}}
        },
        {
            "Z", new() { {"A", 2}, {"B", 3}, {"C", 1}}
        }
    };
    private int GetScoreForLineB(string line)
    {
        string p1 = line.Split(' ')[0];
        string p2 = line.Split(' ')[1];
        return guessValueB[p2] + resultValueB[p2][p1];
    }
}
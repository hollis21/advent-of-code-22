public class Day4 : IDay
{
    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        return Task.FromResult(inputLines.Count(testBoundsA).ToString());
    }
    private bool testBoundsA(string line) {
        int[] sp = line.Split(',','-').Select(int.Parse).ToArray();
        if (sp.Length != 4) {
            throw new ArgumentException($"Invalid formatted line: {line}");
        }
        return (sp[0] >= sp[2] && sp[1] <= sp[3]) || (sp[0] <= sp[2] && sp[1] >= sp[3]);
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        return Task.FromResult(inputLines.Count(testBoundsB).ToString());
    }
    private bool testBoundsB(string line) {
        int[] sp = line.Split(',','-').Select(int.Parse).ToArray();
        if (sp.Length != 4) {
            throw new ArgumentException($"Invalid formatted line: {line}");
        }
        return !(sp[0] > sp[3] || sp[2] > sp[1]);
    }

}
public class Day3 : IDay
{
    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        return Task.FromResult(
            inputLines.Select(line => splitLine(line))
                      .Select(s => convertCharToValue(findDupe(s.left, s.right)))
                      .Sum()
                      .ToString());
    }

    private char findDupe(string input1, string input2) {
        var hash1 = input1.ToHashSet();
        return input2.First(c => hash1.Contains(c));
    }

    private (string left, string right) splitLine(string line) {
        return (line.Substring(0, line.Length / 2), line.Substring(line.Length / 2));
    }

    private int convertCharToValue(char item) {
        if (item >= 'a' && item <= 'z') {
            return item - 'a' + 1;
        } else if (item >= 'A' && item <= 'Z') {
            return item - 'A' + 27;
        } else {
            throw new ArgumentException("Invalid char", nameof(item));
        }
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var sum = 0;
        var enumerator = inputLines.GetEnumerator();
        while (enumerator.MoveNext()) {
            var lines = new string[3];
            lines[0] = enumerator.Current;
            enumerator.MoveNext();
            lines[1] = enumerator.Current;
            enumerator.MoveNext();
            lines[2] = enumerator.Current;
            sum += convertCharToValue(findTrip(lines));
        }
        return Task.FromResult(sum.ToString());
    }

    private char findTrip(IEnumerable<string> inputs) {
        Dictionary<char, int> map = new();
        foreach (var line in inputs) {
            foreach(char c in line.ToHashSet()) {
                if (!map.ContainsKey(c)) {
                    map[c] = 0;
                }
                map[c]++;
                if (map[c] == 3) {
                    return c;
                }
            }
        }
        throw new ArgumentException($"{nameof(inputs)} doesn't contain a value shared between 3 lines");
    }
}
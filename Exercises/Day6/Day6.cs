public class Day6 : IDay
{
    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        string input = string.Join("", inputLines.ToArray());
        for (int i = 3; i < input.Length; i++)
        {
            if (input[i] != input[i - 1] &&
                input[i] != input[i - 2] &&
                input[i] != input[i - 3] &&
                input[i - 1] != input[i - 2] &&
                input[i - 1] != input[i - 3] &&
                input[i - 2] != input[i - 3]
                )
            {
                return Task.FromResult((i + 1).ToString());
            }
        }
        throw new ArgumentException("Input invalid");
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        string input = string.Join("", inputLines.ToArray());
        int[] counts = Enumerable.Repeat(0, ('z' - 'a' + 1)).ToArray();
        int dups = 0;
        // initialize counts with first 14 characters
        for (int i = 0; i < 14; i++) {
            counts[input[i] - 'a']++;
            if (counts[input[i] - 'a'] == 2) {
                dups++;
            }
        }
        if (dups == 0) {
            return Task.FromResult("14");
        }
        for (int i = 14; i < input.Length; i++) {
            counts[input[i - 14] - 'a']--;
            if (counts[input[i - 14] - 'a'] == 1) {
                dups--;
            }
            counts[input[i] - 'a']++;
            if (counts[input[i] - 'a'] == 2) {
                dups++;
            }
            if (dups == 0) {
                return Task.FromResult((i + 1).ToString());
            }
        }
        throw new ArgumentException("Invalid Argument");
    }
}
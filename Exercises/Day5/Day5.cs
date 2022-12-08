using System.Text.RegularExpressions;

public class Day5 : IDay
{
    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        List<string> stackLines = new();
        var enumerator = inputLines.GetEnumerator();
        while (enumerator.MoveNext() && enumerator.Current.Trim() != String.Empty)
        {
            stackLines.Add(enumerator.Current);
        }
        var stacks = ParseStacks(stackLines);
        List<Movement> movements = new();
        while (enumerator.MoveNext())
        {
            movements.Add(ParseMovement(enumerator.Current));
        }
        foreach (var move in movements)
        {
            for (int i = 0; i < move.Amount; i++)
            {
                stacks[move.Target - 1].Push(stacks[move.Source - 1].Pop());
            }
        }
        return Task.FromResult(
            string.Join(string.Empty,
                stacks
                    .ToList()
                    .OrderBy(kvp => kvp.Key)
                    .Select(kvp => kvp.Value.Peek())
            )
        );
    }


    Regex rx = new Regex(@"\[(\w)\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Dictionary<int, Stack<char>> ParseStacks(IEnumerable<string> inputLines)
    {
        Dictionary<int, Stack<char>> stacks = new();
        MatchCollection? matches = null;
        foreach (var line in inputLines.Reverse())
        {
            matches = rx.Matches(line);
            foreach (Match match in matches)
            {
                int stackNum = match.Groups[1].Index / 4; // Watch out, integer division to floor the value
                if (!stacks.ContainsKey(stackNum))
                {
                    stacks[stackNum] = new Stack<char>();
                }
                stacks[stackNum].Push(match.Groups[1].Value[0]);
            }
        }
        return stacks;
    }

    struct Movement
    {
        public readonly int Amount { get; }
        public readonly int Source { get; }
        public readonly int Target { get; }
        public Movement(int amount, int source, int target)
        {
            Amount = amount;
            Source = source;
            Target = target;
        }
    }
    Regex movementRegex = new Regex(@"move (?<amt>\d+) from (?<from>\d+) to (?<to>\d+)");
    private Movement ParseMovement(string line)
    {
        MatchCollection matches = movementRegex.Matches(line);
        return new Movement(int.Parse(matches[0].Groups["amt"].Value), int.Parse(matches[0].Groups["from"].Value), int.Parse(matches[0].Groups["to"].Value));
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        List<string> stackLines = new();
        var enumerator = inputLines.GetEnumerator();
        while (enumerator.MoveNext() && enumerator.Current.Trim() != String.Empty)
        {
            stackLines.Add(enumerator.Current);
        }
        var stacks = ParseStacks(stackLines);
        List<Movement> movements = new();
        while (enumerator.MoveNext())
        {
            movements.Add(ParseMovement(enumerator.Current));
        }
        foreach (var move in movements)
        {
            Stack<char> tmpStack = new();
            for (int i = 0; i < move.Amount; i++)
            {
                tmpStack.Push(stacks[move.Source - 1].Pop());
            }
            for (int i = 0; i < move.Amount; i++)
            {
                stacks[move.Target - 1].Push(tmpStack.Pop());
            }
        }
        return Task.FromResult(
            string.Join(string.Empty,
                stacks
                    .ToList()
                    .OrderBy(kvp => kvp.Key)
                    .Select(kvp => kvp.Value.Peek())
            )
        );
    }
}
public interface IDay {
    Task<string> SolutionA(IEnumerable<string> inputLines);
    Task<string> SolutionB(IEnumerable<string> inputLines);
}
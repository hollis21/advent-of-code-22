var days = new List<Type>() {
    typeof(Day1),
    typeof(Day2),
    typeof(Day3),
    typeof(Day4),
    typeof(Day5),
    typeof(Day6)
};

Console.WriteLine("Welcome to Mike's 2022 Advent of Code");
Console.WriteLine("-Available Days-");
for (int i = 0; i < days.Count; i++)
{
    Type? dayType = days[i];
    Console.WriteLine($"{i + 1}: {dayType.Name}");
}
Console.Write("Select a day:");
var response = Console.ReadLine();
if (!int.TryParse(response, out int selectedDay) || selectedDay > days.Count || selectedDay < 1) {
    Console.WriteLine();
    Console.WriteLine("Invalid entry. Exiting.");
    return;
}

IDay? day = Activator.CreateInstance(days[selectedDay - 1]) as IDay;
if (day == null) {
    Console.WriteLine();
    Console.WriteLine("Invalid entry. Exiting.");
    return;
}

Console.Write("Solution A or B?");
response = Console.ReadLine();
if (response == null || (response.ToUpper() != "A" && response.ToUpper() != "B")) {
    Console.WriteLine();
    Console.WriteLine("Invalid entry. Exiting.");
    return;
}

using (var sr = new StreamReader($"Exercises/{days[selectedDay - 1].Name}/input.txt")) {
    string? result = null;
    var lines = sr.ToIEnumerable();
    if (response.ToUpper() == "A") {
        result = await day.SolutionA(lines);
    } else {
        result = await day.SolutionB(lines);
    }
    Console.WriteLine();
    Console.WriteLine("-Solution-");
    Console.WriteLine(result);
}

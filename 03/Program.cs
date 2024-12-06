using System.Text.RegularExpressions;

string path = Directory.GetCurrentDirectory().Split("bin")[0];

path = Path.Combine(path, "input", "input.txt");

List<string> inputList = new List<string>(File.ReadAllLines(path));
string inputString = File.ReadAllText(path);

bool debug = false;

ulong Part1(List<string> strings)
{
    List<string> correctPart1SubStr = new();

    ulong result = 0;

    Regex r = new Regex(@"mul\(\d{1,3},\d{1,3}\)");

    foreach (string s in strings)
    {
        foreach (Match match in r.Matches(s))
        {
            correctPart1SubStr.Add(match.Value[4..^1]);
            var temp = Array.ConvertAll(match.Value[4..^1].Split(','), ulong.Parse);
            result += temp[0] * temp[1];
            if (debug) Console.WriteLine($"Result: {temp[0],3} * {temp[1],3} = {(temp[0] * temp[1]),7} -> Total: {result,10}");
        }
    }

    return result;
}

ulong Part2(string input)
{
    List<string> correctPart2SubStr = new();
    List<string> doSubStr = new(input.Split("do()"));

    foreach (string segment in doSubStr)
    {
        correctPart2SubStr.Add(segment.Split("don't()")[0]);
    }

    return Part1(correctPart2SubStr);
}

Console.WriteLine($"Part1: {Part1(inputList)}");
Console.WriteLine($"Part2: {Part2(inputString)}");

// 213490358 too high, deleted first set and some don'ts are in...
//  88880807 too high ...
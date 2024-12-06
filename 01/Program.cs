string path = Directory.GetCurrentDirectory();
path = Path.Combine(path, "input", "input.txt");

string[] readText = File.ReadAllLines(path);

List<int> first = new();
List<int> second = new();

foreach (string s in readText)
{
    first.Add(Int32.Parse(s.Split("   ")[0]));
    second.Add(Int32.Parse(s.Split("   ")[1]));
}

first.Sort();
second.Sort();



//Part 1
int sum = 0;

for (int i = 0; i < first.Count(); i++) sum += Math.Abs(first[i] - second[i]);



// Part 2
int similarity = 0;
int times;
int start = 0;

for (int i = 0; i < first.Count(); i++)
{
    times = 0;
    for (int j = start; j < second.Count(); j++)
    {
        if (first[i] > second[j]) start = j + 1;
        else if (first[i] == second[j]) times++;
        else if (first[i] < second[j]) break;
    }
    similarity += (first[i] * times);

    if (start == first.Count()) break;
}

Console.WriteLine($"\nPart 1 - Sum:{sum,17}");
Console.WriteLine($"Part 2 - Similarity:{similarity,10}");
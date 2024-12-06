string path = Directory.GetCurrentDirectory().Split("bin")[0];
string inputpath = Path.Combine(path, "input", "input.txt");
//string inputpath = Path.Combine(path, "input", "testinput.txt");

List<string> inputList = new(File.ReadAllLines(inputpath));

DateTime startingTime = DateTime.Now;

HashSet<(Pos, Pos)> beenTo = new();
HashSet<Pos> part1result = new();

HashSet<Pos> walls = new();
HashSet<Pos> loops = new();

Pos extrawallPart1 = new Pos(-1, -1);
Pos start = new Pos(0, 0);
Pos startdir = new Pos(0, -1);
Pos player = start;
Pos dir = startdir;
Pos area = new Pos(inputList[0].Length - 1, inputList.Count() - 1);

LoadElements();

StartPart1Loop(); // Part1

// Setup where extra walls could be.
HashSet<Pos> extrawalls2 = new();

foreach ((Pos check, Pos direction) in beenTo)
{
    var recent = check - direction;
    if (start == check) continue;

    Pos newdir = SwapDirection(direction);
    while (true)
    {
        recent += newdir;
        if (walls.Contains(recent))
        {
            extrawalls2.Add(check);
            break;
        }
        else if (recent.Outside(area)) break;
    }
}

foreach (var extrawall in extrawalls2)
{
    StartPart2Loop(extrawall);
}

Pos SwapDirection(Pos current)
{
    if (current == (0, -1)) return new Pos(1, 0);
    else if (current == (0, 1)) return new Pos(-1, 0);
    else if (current == (1, 0)) return new Pos(0, 1);
    return new Pos(0, -1);
}

void LoadElements()
{
    for (int y = 0; y < inputList.Count(); y++)
    {
        for (int x = 0; x < inputList[y].Length; x++)
        {
            var current = inputList[y][x];
            Pos newpos = new Pos(x, y);

            if (current == '#') walls.Add(newpos);
            else if (current == '^')
            {
                start = newpos;
                player = newpos;
                beenTo.Add((newpos, dir));
            }
        }
    }
}

void StartPart2Loop(Pos extrawall)
{
    ResetGame();
    while (true)
    {
        Pos next = player + dir;

        if (next.Outside(area)) break;

        if (walls.Contains(next) || next == extrawall)
        {
            dir = SwapDirection(dir);
            continue;
        }

        if (!beenTo.Add((next, dir)))
        {
            loops.Add(extrawall);
            break;
        }

        player = next;
    }
}

void StartPart1Loop()
{
    ResetGame();
    while (true)
    {
        Pos next = player + dir;

        if (next.Outside(area)) break;

        if (walls.Contains(next))
        {
            dir = SwapDirection(dir);
            continue;
        }

        part1result.Add(next);

        if (!beenTo.Add((next, dir))) break;

        player = next;
    }
}

void ResetGame()
{
    beenTo = new HashSet<(Pos, Pos)>();
    player = start;
    dir = startdir;
}

Console.WriteLine($"Part1 result: {part1result.Count()}");
Console.WriteLine($"Part2 result: {loops.Count()}");

Console.WriteLine($"Ended: {startingTime}");
DateTime endingTime = DateTime.Now;
Console.WriteLine($"Ended: {endingTime}");
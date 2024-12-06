string path = Directory.GetCurrentDirectory().Split("bin")[0];

path = Path.Combine(path, "input", "input.txt");
//path = Path.Combine(path, "input", "test.txt");
//path = Path.Combine(path, "input", "test2.txt");

List<string> inputList = new List<string>(File.ReadAllLines(path));
List<(int X, int Y)> correct = new();
List<(int X, int Y)> correct2 = new();

string xmas = "XMAS";
int correct1 = 0;

var directions = new List<(int X, int Y)>
                 {
                    (-1,  0),
                    (-1, -1),
                    ( 0, -1),
                    ( 1, -1),
                    ( 1,  0),
                    ( 1,  1),
                    ( 0,  1),
                    (-1,  1)
                 };

Part1();
Part2();
Print();

void Part1()
{
    for (int y = 0; y < inputList.Count(); y++)
    {
        for (int x = 0; x < inputList[y].Length; x++)
        {
            if (inputList[y][x] == 'X')
            {
                foreach (var dir in directions)
                {
                    try
                    {
                        if (CheckDirection(x, y, dir, 1))
                        {
                            correct.Add((x, y));
                            correct1++;
                        }
                    }
                    catch { }
                }
            }
        }
    }
}

bool CheckDirection(int x, int y, (int X, int Y) dir, int index)
{
    bool result = false;
    var nextX = x + dir.X;
    var nextY = y + dir.Y;
    var checkChar = inputList[nextY][nextX];

    if (checkChar == xmas[index] && index == xmas.Length - 1) result = true;
    else if (checkChar == xmas[index]) result = CheckDirection(nextX, nextY, dir, index + 1);

    if (result) correct.Add((nextX, nextY));
    return result;
}

void Part2()
{
    for (int y = 0; y < inputList.Count(); y++)
    {
        for (int x = 0; x < inputList[y].Length; x++)
        {
            if (inputList[y][x] == 'A')
            {
                try
                {
                    char a1 = inputList[y - 1][x - 1];
                    char a2 = inputList[y + 1][x + 1];
                    char b1 = inputList[y + 1][x - 1];
                    char b2 = inputList[y - 1][x + 1];

                    if (((a1 == 'M' && a2 == 'S') || (a1 == 'S' && a2 == 'M')) &&
                        ((b1 == 'M' && b2 == 'S') || (b1 == 'S' && b2 == 'M'))) correct2.Add((x, y));
                }
                catch { }
            }
        }
    }
}

void Print()
{
    foreach (string line in File.ReadAllLines(path))
    {
        Console.WriteLine(line);
    }
    correct.Reverse();
    foreach ((int x, int y) dir in correct)
    {
        if (dir.x < Console.BufferWidth && dir.y < Console.BufferHeight)
        {
            Console.SetCursorPosition(dir.x, dir.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(inputList[dir.y][dir.x]);
        }
    }
    foreach ((int x, int y) dir in correct2)
    {
        if (dir.x < Console.BufferWidth && dir.y < Console.BufferHeight)
        {
            (int, int)[] nodes = new[] {
                (-1, -1),
                (-1, +1),
                ( 0,  0),
                (+1, +1),
                (+1, -1)
            };

            foreach ((int x, int y) node in nodes)
            {
                if ((dir.x + node.x) < Console.BufferWidth && (dir.y + node.y) < Console.BufferHeight)
                {
                    Console.SetCursorPosition(dir.x + node.x, dir.y + node.y);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(inputList[dir.y + node.y][dir.x + node.x]);
                }
            }
        }
    }
    Console.SetCursorPosition(0, Console.BufferHeight - 3);
}

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine($"Part1: {correct1}");
Console.WriteLine($"Part2: {correct2.Count()}");
Console.ResetColor();

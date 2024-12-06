string path = Directory.GetCurrentDirectory().Split("bin")[0];
path = Path.Combine(path, "input", "input.txt");
//path = Path.Combine(path, "input", test.txt");

string[] inputarray = File.ReadAllLines(path);

bool desc;
int diff;

int safe1 = 0;
int safe2 = 0;

foreach (string line in inputarray)
{
    int unstable = 0;

    string message = string.Empty;
    string error = string.Empty;

    int[] current = Array.ConvertAll(line.Split(' '), int.Parse);

    int descInt = 0;
    int ascInt = 0;
    int sameInt = 0;

    bool lastFailed = false;

    for (int i = 1; i < current.Count(); i++)
    {
        if (current[i - 1] > current[i]) descInt++;
        else if (current[i - 1] < current[i]) ascInt++;
        else sameInt++;
    }

    error += $"({descInt},{ascInt},{sameInt}) -> ";

    bool allVariants = (sameInt > 0 && ascInt > 0 && descInt > 0);

    if (allVariants || (ascInt > 1 && descInt > 1) || sameInt > 1)
    {
        unstable = 2;
        error += $"{line}".PadRight(69);
    }
    else
    {
        desc = (ascInt < descInt);
        message += ($"Desc: {desc}, Line: {line}".PadRight(50));

        for (int i = 1; i < current.Count(); i++)
        {
            int b = current[i - 1];
            int a = current[i];

            diff = (desc) ? b - a : a - b;

            bool isNotOk = (diff < 1 || diff > 3);

            if ((lastFailed && isNotOk) || (lastFailed && !isNotOk && i > 2))
            {
                int c = current[i - 2];
                diff = (desc) ? c - a : a - c;
                isNotOk = (diff < 1 || diff > 3);
            }

            if (isNotOk)
            {
                error += ($"{a}, ");
                if (++unstable > 1) break;
                lastFailed = true;
            }
            else lastFailed = false;
        }
    }

    safe1 += (unstable == 0) ? 1 : 0;
    safe2 += (unstable <= 1) ? 1 : 0;

    if (unstable == 0)
    {
        message += "Safe 1 & 2";
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(error.PadRight(30));
        Console.WriteLine(message);
        Console.ResetColor();
    }
    else if (unstable == 1)
    {
        message += "Safe 2";
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write(error.PadRight(30));
        Console.WriteLine(message);
        Console.ResetColor();
    }
    else
    {
        message += "Failed both";
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(error.PadRight(30));
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

Console.WriteLine($"There are {safe1} safe reports in part 1.");
Console.WriteLine($"There are {safe2} safe reports in part 2.");
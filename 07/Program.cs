string path = Directory.GetCurrentDirectory().Split("bin")[0];
string inputpath = Path.Combine(path, "input", "input.txt");
//string inputpath = Path.Combine(path, "input", "testinput.txt");

List<string> inputList = new(File.ReadAllLines(inputpath));

Console.ReadLine();
Part1(true);
//Biggest(); // To see if int is enough

void Part1(bool print = false)
{
    Console.Clear();
    ulong part1result = 0;
    foreach (var line in inputList)
    {
        if (print) Console.Write("\n" + line.PadRight(48));

        ulong check = ulong.Parse(line.Split(':')[0]);
        ulong[] numbers = line.Split(':')[1].Trim().Split(' ').Select(ulong.Parse).ToArray();

        ulong max = numbers.Aggregate((ulong)1, (acc, val) => acc * val);
        ulong min = numbers.Aggregate((ulong)0, (acc, val) => acc + val);

        if (max == check)
        {
            part1result += check;
            if (print)
            {
                PrintWithColor($"max == check".PadRight(21), ConsoleColor.Green);
                Console.Write($" currSum:{part1result,16}");
            }
        }
        else if (min == check)
        {
            part1result += check;
            if (print)
            {
                PrintWithColor($"min == check".PadRight(21), ConsoleColor.Green, ConsoleColor.DarkGray);
                Console.Write($" currSum:{part1result,16}");
            }
        }
        else if (min > check)
        {
            if (print) PrintWithColor($"min > check", ConsoleColor.Red);
        }
        else if (max < check)
        {
            if (print) PrintWithColor($"max < check", ConsoleColor.Red);
        }
        else if (max > check)
        {
            if (print) PrintWithColor($"max > check", ConsoleColor.Blue);

            Queue<ulong> toCalc = new();
            toCalc.Enqueue(numbers[0]);

            Queue<string> toPrint = new();
            if (print) toPrint.Enqueue($"{numbers[0]}");

            for (int i = 1; i < numbers.Length; i++)
            {
                ulong currNum = numbers[i];
                Queue<ulong> tempCalc = new();
                Queue<string> tempPrint = new();
                while (toCalc.Count > 0)
                {
                    ulong currSum = toCalc.Dequeue();
                    string currPrint = toPrint.Dequeue();

                    tempCalc.Enqueue(currSum + currNum);
                    tempCalc.Enqueue(currSum * currNum);
                    if (print)
                    {
                        tempPrint.Enqueue($"{currPrint} + {currNum}");
                        tempPrint.Enqueue($"{currPrint} * {currNum}");
                    }
                }
                toCalc = tempCalc;
                if (print) toPrint = tempPrint;
            }

            while (toCalc.Count > 0)
            {
                var currCheck = toCalc.Dequeue();
                string currPrint = string.Empty;
                if (print) currPrint = toPrint.Dequeue();

                if (currCheck == check)
                {
                    part1result += check;

                    if (print)
                    {
                        PrintWithColor($" (true)".PadRight(10), ConsoleColor.Green);
                        Console.Write($" currSum: {part1result,15}");
                        Console.Write($" - {currPrint}");
                    }
                    break;
                }

                if (print && toCalc.Count == 0) PrintWithColor($" (false)".PadRight(10), ConsoleColor.Red);
            }
        }
    }
    Console.WriteLine($"\n\nPart1 result: {part1result}");
}

void PrintWithColor(string str, ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black)
{
    Console.BackgroundColor = background;
    Console.ForegroundColor = foreground;
    Console.Write(str);
    Console.ResetColor();
}

void Biggest()
{
    ulong sum = 0;
    foreach (var line in inputList)
    {
        sum += ulong.Parse(line.Split(':')[0]);
        Console.WriteLine($"Curr sum: {sum,20}");
    }
    Console.WriteLine($"\nSum of all: {sum}");
}

// try: 1 - 3193487579657 - too low.
// try: 2 - 3193487567837 - obv too low ... (min == check) was missing.

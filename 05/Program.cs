string path = Directory.GetCurrentDirectory().Split("bin")[0];
string inputpath = Path.Combine(path, "input", "input.txt");
string orderpath = Path.Combine(path, "input", "inputorder.txt");

bool test = false;
bool chatgpt = false;

if (test)
{
    inputpath = Path.Combine(path, "input", "testinput.txt");
    orderpath = Path.Combine(path, "input", "testinputorder.txt");
}

List<string> inputList = new(File.ReadAllLines(inputpath));
List<int[]> inputInts = inputList.ConvertAll(line => Array.ConvertAll(line.Split(','), int.Parse)).ToList();

List<string> orderList = new(File.ReadAllLines(orderpath));
List<int[]> failedList = new();


// Fixes a dictionary that is used to check if the order is correct.
Dictionary<int, List<int>> orders = new();
foreach (var order in orderList)
{
    var first = Int32.Parse(order.Split('|')[0]);
    if (!orders.ContainsKey(first))
    {
        orders.Add(first, new List<int>());
    }

    var second = Int32.Parse(order.Split('|')[1]);
    orders[first].Add(second);
}

PrintOrders();
void PrintOrders()
{
    foreach (var item in orders)
    {
        Console.WriteLine($"{item.Key} - {string.Join(", ", item.Value)}");
    }
}

int part1result;
int part2result;

if (chatgpt)
{
    part1result = checkListChatGPT(inputInts, true);
    sortListChatGPT(failedList);
    part2result = checkListChatGPT(failedList);
}
else
{
    part1result = checkList(inputInts, true);
    sortList(failedList);
    part2result = checkList(failedList);
}

int checkList(List<int[]> listToCheck, bool saveFails = false)
{
    int result = 0;
    foreach (var line in listToCheck)
    {
        bool hasFailed = false;

        for (int i = 0; i < line.Length; i++)
        {
            var currentDigit = line[i];

            for (int j = i + 1; j < line.Length; j++)
            {
                var checkDigit = line[j];

                if (orders.ContainsKey(checkDigit))
                {
                    if (orders[checkDigit].IndexOf(currentDigit) != -1)
                    {
                        if (saveFails) failedList.Add(line);
                        hasFailed = true;
                        break;
                    }
                }
            }
            if (hasFailed) break;
        }
        if (!hasFailed) result += line[line.Length / 2];
    }
    return result;
}

void sortList(List<int[]> listToSort)
{
    foreach (var line in listToSort)
    {
        Console.WriteLine($"unsorded: {string.Join(',', line)}");
        bool unsorted = true;
        while (unsorted)
        {
            bool swapped = false;
            for (int i = 0; i < line.Length; i++)
            {
                int a = line[i];
                for (int j = i + 1; j < line.Length; j++)
                {
                    int b = line[j];

                    if (orders.ContainsKey(b) && orders[b].Contains(a))
                    {
                        int temp = a;
                        line[i] = b;
                        line[j] = temp;
                        swapped = true;
                        break;
                    }
                }
                if (swapped) break;
            }
            if (!swapped)
            {
                unsorted = false;
                Console.WriteLine($"  sorded: {string.Join(',', line)}");
            }
        }
        Console.WriteLine(new string('-', Console.BufferWidth - 1));
    }
}



// Gave my methods to ChatGPT to ask for feedback
// This is ChatGPT's version without prints.

// This seems a bit to nestled to understand really compared to my version
int checkListChatGPT(List<int[]> listToCheck, bool saveFails = false)
{
    var validLines = listToCheck.Where(line =>
    {
        // Check if the line fails the conditions
        bool hasFailed = line
            .Select((currentDigit, i) => (currentDigit, i)) // Include indices
            .Any(pair =>
                line
                    .Skip(pair.i + 1)
                    .Any(checkDigit =>
                        orders.ContainsKey(checkDigit) && orders[checkDigit].Contains(pair.currentDigit))
            );

        if (hasFailed && saveFails) failedList.Add(line);
        return !hasFailed;
    });

    // Sum the middle elements of valid lines
    return validLines.Sum(line => line[line.Length / 2]);
}

// But this one is way nicer
// Good to know how to make custom IComparators :)
void sortListChatGPT(List<int[]> listToSort)
{
    foreach (var line in listToSort)
    {
        // Sort the array using a custom comparator
        Array.Sort(line, (a, b) =>
        {
            if (orders.ContainsKey(b) && orders[b].Contains(a)) return 1;
            if (orders.ContainsKey(a) && orders[a].Contains(b)) return -1;
            return 0;
        });
    }
}

Console.WriteLine($"\n\nPart 1 result: {part1result}");
Console.WriteLine($"Part 2 result: {part2result}");
using Lab5_Monitor;
using System.Globalization;

Console.Title = "Практична робота №5 - Варіант 14 - Функціонування курника";

CultureInfo cultureInfo = new("uk-UA", false);
Console.OutputEncoding = System.Text.Encoding.UTF8;

int chickCount = 10;

Random random = new();

Console.WriteLine("\tПочаток");

var chicken = new Chicken(chickCount, 10);
var tasks = new Task[chickCount];

for (int i = 0; i < chickCount; i++)
{
    var chick = new Chick("Курча" + (i + 1), chicken, 1, random.Next(3));
    tasks[i] = new Task(chick.Run);
}

tasks.ToList().ForEach(x => x.Start());
Task.WaitAll(tasks);

Console.WriteLine("\tКінець");

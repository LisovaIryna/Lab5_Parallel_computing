using Lab5_Example_Monitor;

int carCount = 5;

Random random = new();
Console.WriteLine("\tGO!!!");
var fuelStation = new FuelStationMonitor(2, 300);
var tasks = new Task[carCount];
for (int i = 0; i < carCount; i++)
{
    var car = new Car("car" + (i + 1), fuelStation, 100, random.Next(1, 5));
    tasks[i] = new Task(car.Run);
}
tasks.ToList().ForEach(x => x.Start());
Thread.Sleep(30000);
fuelStation.Fill(2000);
Task.WaitAll(tasks);
Console.WriteLine("\tFINISH!!!");
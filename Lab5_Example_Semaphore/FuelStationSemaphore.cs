namespace Lab5_Example_Semaphore
{
    class FuelStationSemaphore : AbstractFuelStation
    {
        private readonly Mutex mutex = new();
        private readonly SemaphoreSlim semaphore;

        public FuelStationSemaphore(int columnCount, int reserve = 1000) : base(columnCount, reserve)
        {
            semaphore = new SemaphoreSlim(ColumnCount, ColumnCount);
        }

        public override void Fill(int amount)
        {
            mutex.WaitOne();
            if (amount + Reserve > Capacity)
                Reserve = Capacity;
            else
                Reserve += Capacity;
            Console.WriteLine("STATION TANK REFUELD");
            mutex.ReleaseMutex();
        }

        public override bool TryRefuel(Car car, int volume)
        {
            semaphore.Wait();
            mutex.WaitOne();
            bool result = false;
            if (volume <= Reserve)
            {
                Console.WriteLine($"Fuels reserve is {Reserve}. {volume} liters fueling began for {car.Name}");
                Reserve -= volume;
                mutex.ReleaseMutex();
                Thread.Sleep(100 * volume);
                Console.WriteLine($"{car.Name} fueling finished");
                result = true;
            }
            else
            {
                Console.WriteLine($"Fuel reserve {Reserve} is insufficient for {car.Name}");
                mutex.ReleaseMutex();
            }
            semaphore.Release();
            return result;
        }
    }
}

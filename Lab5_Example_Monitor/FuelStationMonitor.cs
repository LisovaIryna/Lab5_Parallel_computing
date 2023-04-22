namespace Lab5_Example_Monitor
{
    class FuelStationMonitor : AbstractFuelStation
    {
        private readonly object lockObject;
        private int freeColumnCount;

        public FuelStationMonitor(int countColumn, int reserve) : base(countColumn, reserve) 
        { 
            lockObject = new object();
            freeColumnCount = countColumn;
        }

        public override void Fill(int amount)
        {
            lock (lockObject)
            {
                if (amount + Reserve > Capacity)
                    Reserve = Capacity;
                else
                    Reserve += Capacity;
                Console.WriteLine("STATION TANK REFUELD");
                Monitor.PulseAll(lockObject);
            }
        }

        public override bool TryRefuel(Car car, int volume)
        {
            bool result = false;
            lock(lockObject)
            {
                while (freeColumnCount < 0)
                    Monitor.Wait(lockObject);
                if (volume <= Reserve)
                {
                    freeColumnCount--;
                    Console.WriteLine($"Fuels reserve is {Reserve}. {volume} liters fueling began for {car.Name}");
                    Reserve -= volume;
                    result = true;
                }
                else
                    Console.WriteLine($"Fuel reserve {Reserve} is insufficient for {car.Name}");
            }
            if (result)
            {
                Thread.Sleep(100 * volume);
                Console.WriteLine($"{car.Name} fueling finished");
                lock(lockObject)
                {
                    freeColumnCount++;
                    Monitor.PulseAll(lockObject);
                }
            }
            return result;
        }
    }
}

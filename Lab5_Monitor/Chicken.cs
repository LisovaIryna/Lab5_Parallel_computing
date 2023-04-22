namespace Lab5_Monitor
{
    class Chicken : HenHouse
    {
        private readonly object lockObject;
        private int freePortionsCount;

        public Chicken(int chickCount, int reservePortionsFood = 10) : base(chickCount, reservePortionsFood)
        {
            lockObject = new object();
            freePortionsCount = reservePortionsFood;
        }

        public override void Fill(int amount)
        {
            lock (lockObject)
            {
                if (amount + ReservePortionsFood > PortionsFood)
                    ReservePortionsFood = PortionsFood;
                else
                    ReservePortionsFood += PortionsFood;

                Console.WriteLine($"Мама-квочка наповнила миску {PortionsFood} порціями їжі.");

                Monitor.PulseAll(lockObject);
            }
        }

        public override bool TryEat(Chick chick, int portion)
        {
            bool result = false;

            lock (lockObject)
            {
                while (freePortionsCount < 0)
                    Monitor.Wait(lockObject);
                if (portion <= ReservePortionsFood)
                {
                    freePortionsCount--;
                    Console.WriteLine($"В мисці є {ReservePortionsFood} порцій. {chick.Name} починає їсти 1 порцію.");
                    ReservePortionsFood -= portion;
                    result = true;
                }
                else
                    Fill(PortionsFood);
            }
            if (result)
            {
                Thread.Sleep(100 * portion);
                Console.WriteLine($"{chick.Name} закінчило їсти.");
                lock (lockObject)
                {
                    freePortionsCount++;
                    Monitor.PulseAll(lockObject);
                }
            }
            return result;
        }
    }
}

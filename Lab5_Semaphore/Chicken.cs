namespace Lab5_Semaphore
{
    class Chicken : HenHouse
    {
        private readonly Mutex mutex = new();
        private readonly SemaphoreSlim semaphore;

        public Chicken(int chickCount, int reservePortionsFood = 10) : base(chickCount, reservePortionsFood)
        {
            semaphore = new SemaphoreSlim(ChickCount, ChickCount);
        }

        public override void Fill(int amount)
        {
            mutex.WaitOne();

            if (amount + ReservePortionsFood > PortionsFood)
                ReservePortionsFood = PortionsFood;
            else
                ReservePortionsFood += PortionsFood;

            Console.WriteLine($"Мама-квочка наповнила миску {PortionsFood} порціями їжі.");
            mutex.ReleaseMutex();
        }

        public override bool TryEat(Chick chick, int portion)
        {
            semaphore.Wait();
            mutex.WaitOne();

            bool result = false;

            if (portion <= ReservePortionsFood)
            {
                Console.WriteLine($"В мисці є {ReservePortionsFood} порцій. {chick.Name} починає їсти 1 порцію.");

                ReservePortionsFood -= portion;
                mutex.ReleaseMutex();
                Thread.Sleep(1000);

                Console.WriteLine($"{chick.Name} закінчило їсти.");
                result = true;
            }
            else
            {
                Fill(PortionsFood);
                mutex.ReleaseMutex();
            }

            semaphore.Release();
            return result;
        }
    }
}

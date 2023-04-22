namespace Lab5_Monitor
{
    class Chick
    {
        private static readonly Random random = new();
        public string Name { get; set; }
        public HenHouse ChickenHouse { get; }
        public int Portion { get; } = 1;
        public int EatingCount { get; }

        public Chick(string name, HenHouse house, int portion, int eatingCount)
        {
            Name = name;
            ChickenHouse = house;
            Portion = portion;
            EatingCount = eatingCount;
        }

        public void Run()
        {
            Console.WriteLine($"{Name} йде їсти.");
            for (int i = 0; i < EatingCount; i++)
            {
                Console.WriteLine($"{Name} намагається з'їсти {Portion} порцію.");

                var portion = Portion;

                while (!ChickenHouse.TryEat(this, portion))
                {
                    Console.WriteLine($"{Name} чекає.");
                    Thread.Sleep(random.Next(1, 2) * 1000);
                }

                Thread.Sleep(random.Next(1, 2) * 1000);
                Console.WriteLine($"{Name} йде спати {i + 1} раз.");
            }
        }
    }
}

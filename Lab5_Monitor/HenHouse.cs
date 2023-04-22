namespace Lab5_Monitor
{
    abstract class HenHouse
    {
        public int ReservePortionsFood { get; protected set; }
        public int PortionsFood { get; } = 10;
        public int ChickCount { get; }

        public HenHouse(int chickCount, int reservePortionsFood = 10)
        {
            if (chickCount <= 0)
                throw new Exception("Неправильна кількість курчат");
            if (reservePortionsFood > PortionsFood)
                throw new Exception("Неправильна кількість наявних порцій їжі");

            ChickCount = chickCount;
            ReservePortionsFood = reservePortionsFood;
        }

        public abstract void Fill(int amount);

        public abstract bool TryEat(Chick chick, int portion);
    }
}

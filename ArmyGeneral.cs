namespace Berzeker_1
{
    internal sealed class ArmyGeneral
    {
        private Races.Race _race;
        private int _startingResources;

        public List<Unit> Army { get; set; } = new List<Unit>();
        public List<Unit> AliveUnits { get { return Army.Where(u => !u.IsDead).ToList(); } }
        public int Resources { get { return CalculateNumberOfResources(); } }

        private int CalculateNumberOfResources()
        {
            int amount = 0;
            foreach (var soldier in Army)
            {
                amount += soldier.Loot;
            }
            return amount;
        }

        internal Races.Race Race { get => _race; set => _race = value; }
        public string Name { get;}

        public ArmyGeneral(string name, int resources, Races.Race race)
        {
            Name = name;
            _startingResources = resources;
            Race = race;
            GenerateArmy();
        }

        public override string ToString()
        {
            return Name;
        }

        public Unit GetRandomUnit()
        {
            if(AliveUnits.Count == 0)
            {
                Console.WriteLine("Units are dead");
                return null;
            }
            Random random = new Random();
            int randomIndex = random.Next(AliveUnits.Count);
            try
            {
                return AliveUnits[randomIndex];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Could not get a living unit");
                throw;
            }
        }

        private void GenerateArmy()
        {
            uint armySize = GameLoop.ArmySize;
            for (int i = 0; i < armySize; i++)
            {
                CreateUnit();
            }
            DivvyResourcesAmongUnits();
        }

        private void CreateUnit()
        {
            var unitOfRace = Races.GetTypeFromRace(Race);
            if (unitOfRace == null)
            {
                Console.WriteLine("failed to create unit based on race");
                return;
            }
            // Don't print dice roll results and such for every unit
            TextWriter originalConsoleOut = Console.Out;
            Console.SetOut(TextWriter.Null);
            Dice damageDie = Dice.GenerateRandomDice();
            int hp = Dice.GenerateRandomDice().Roll();
            if(unitOfRace.Name == nameof(ElementalArcher))
            {
                int element = Random.Shared.Next(0, Enum.GetNames(typeof(ElementalArcher.Elements)).Length);
                object[] elemntalConstructorParameters = {damageDie, hp, (ElementalArcher.Elements)Enum.ToObject(typeof(ElementalArcher.Elements), element) };
                Unit archer = (Unit)Activator.CreateInstance(unitOfRace, elemntalConstructorParameters);
                if (archer == null) return;
                Army.Add(archer);
                Console.SetOut(originalConsoleOut);
                return;
            }
            object[] constructorParameters = { damageDie, hp };
            Unit soldier = (Unit)Activator.CreateInstance(unitOfRace, constructorParameters);
            if (soldier == null) return;
            Army.Add(soldier);
            Console.SetOut(originalConsoleOut);
        }

        private void DivvyResourcesAmongUnits()
        {
            for (int i = 0; i < Army.Count; i++)
            {
                Army[i].Loot = _startingResources / Army.Count;
            }
        }

        internal void ApplyWeatherEffect(Unit.Weather currentWeather)
        {
            foreach (Unit soldier in AliveUnits)
            {
                if (soldier.IsDead)
                    continue;
                soldier.WeatherEffect(currentWeather);
            }
        }
    }
}

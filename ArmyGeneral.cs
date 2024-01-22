namespace Berzeker_1
{
    internal sealed class ArmyGeneral
    {
        private Races.Race _race;
        private int _startingResources;

        public List<Unit>? Army { get; set; }
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
            Army = new List<Unit>();
            GenerateArmy();
        }

        public override string ToString()
        {
            return Name;
        }

        private void GenerateArmy()
        {
            uint armySize = GameLoop.ArmySize;
            for (int i = 0; i < armySize; i++)
            {
                CreateUnit();
            }
        }

        private void CreateUnit()
        {
            var unitOfRace = Races.GetTypeFromRace(Race);
            if (unitOfRace == null)
            {
                Console.WriteLine("failed to create unit based on race");
                return;
            }
            Dice damageDie = new Dice(2,3,4);
            int hp = 3;
            if(unitOfRace.Name == nameof(ElementalArcher))
            {
                int element = Random.Shared.Next(0, Enum.GetNames(typeof(ElementalArcher.Elements)).Length);
                object[] elemntalConstructorParameters = {damageDie, hp, (ElementalArcher.Elements)Enum.ToObject(typeof(ElementalArcher.Elements), element) };
                Console.WriteLine(elemntalConstructorParameters[2]);
                Army.Add((Unit)Activator.CreateInstance(unitOfRace, elemntalConstructorParameters));
                return;
            }
            object[] constructorParameters = { damageDie, hp };
            Unit soldier = (Unit)Activator.CreateInstance(unitOfRace, constructorParameters);
            Army.Add(soldier);
        }
    }
}

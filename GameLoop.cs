using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal static class GameLoop
    {
        static float _chanceOfWeatherChange = 0.1f;
        static uint _armySize;
        static List<Unit> _units = new List<Unit>();

        internal static List<Unit> Units { get => _units; private set => _units = value; }
        public static uint ArmySize { get => _armySize; private set => _armySize = value; }

        public static void AddToUnitList(Unit unit)
        {
            Units.Add(unit);
        }

        public static void StartGame()
        {
            Console.WriteLine($"Weather is now {ChangeWeather()}!");
            Console.WriteLine("How many units would you like to be in each army?");
            bool validArmySize = uint.TryParse(Console.ReadLine(), out _armySize);
            if(!validArmySize || ArmySize <= 0)
            {
                Console.WriteLine("Number of units must be an interger greater than 0, bozo");
                return;
            }
            Console.WriteLine("Type first general's name");
            string nameOfFirstGeneral = Console.ReadLine();
            Console.WriteLine("Type second general's name");
            string nameOfSecondGeneral = Console.ReadLine();
            int race1 = Random.Shared.Next(0, 3);
            int race2 = Random.Shared.Next(0, 3);
            int numberOfResources1 = Random.Shared.Next(10, 100);
            int numberOfResources2 = Random.Shared.Next(10, 100);
            ArmyGeneral general1 = new(nameOfFirstGeneral, numberOfResources1, (Races.Race)Enum.ToObject(typeof(Races.Race), race1));
            ArmyGeneral general2 = new(nameOfSecondGeneral, numberOfResources2, (Races.Race)Enum.ToObject(typeof(Races.Race), race2));

            if (general1.Army.Count == ArmySize)
                Console.WriteLine($"{general1}'s {general1.Race} army is ready for battle!\n" +
                    $"they hold {general1.Resources} resources!");
            else
            {
                Console.WriteLine("Could not generate armies, terminating program");
                return;
            }
            if (general2.Army.Count == ArmySize)
                Console.WriteLine($"{general2}'s {general2.Race} army is ready for battle!\n" +
                    $"they hold {general2.Resources} resources!");
            else
            {
                Console.WriteLine("Could not generate armies, terminating program");
                return;
            }
            MainLoop(general1, general2);
        }

        private static void MainLoop(ArmyGeneral general1, ArmyGeneral general2)
        {
            Console.WriteLine($"Weather has changed to {ChangeWeather()}!");
        }

        static bool IsArmyAlive(List<Unit> army)
        {
            foreach (Unit unit in army)
            {
                if (!unit.IsDead)
                    return true;
            }
            return false;
        }

        private static void Combat(List<Unit> blues, List<Unit> reds)
        {
            Unit blue;
            Unit red;
            (List<Unit>, bool) wasAttackSuccesfull;
            while (IsArmyAlive(blues) && IsArmyAlive(reds))
            {
                wasAttackSuccesfull = UnitAttacksEnemy(blues, reds, out blue, out red);
                if (!wasAttackSuccesfull.Item2)
                {
                    DeclareWinner(wasAttackSuccesfull.Item1);
                    break;
                }
                wasAttackSuccesfull = UnitAttacksEnemy(reds, blues, out red, out blue);
                if (!wasAttackSuccesfull.Item2)
                {
                    DeclareWinner(wasAttackSuccesfull.Item1);
                    break;
                }
            }
            //overload DeclareWinner with no parameters
        }

        private static (List<Unit>, bool) UnitAttacksEnemy(List<Unit> army1, List<Unit> army2, out Unit firstUnit, out Unit secondUnit)
        {
            firstUnit = GetAliveUnit(army1);
            secondUnit = GetAliveUnit(army2);
            //in case a unit died in a previous attack
            if (firstUnit == null)
                return (army2, false); 
            if (secondUnit == null)
                return (army1, false);
            firstUnit.Attack(secondUnit);
            return (army1, true);
        }

        private static void DeclareWinner(List<Unit> winners)
        {
            Console.WriteLine("print winner, number of resources looted");
        }

        private static Unit GetAliveUnit(List<Unit> blues)
        {
            List<Unit> aliveUnits;
            int randomIndex; 
            aliveUnits = blues.Where(unit => !unit.IsDead).ToList();
            Random random = new Random();
            randomIndex = random.Next(0, aliveUnits.Count);
            return aliveUnits[randomIndex];
        }

        private static Unit.Weather ChangeWeather()
        {
            Unit.Weather[] weathers = (Unit.Weather[])Enum.GetValues(typeof(Unit.Weather));
            return weathers[Random.Shared.Next(0, weathers.Length)];
        }
    }
}

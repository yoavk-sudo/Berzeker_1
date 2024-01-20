using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal static class GameLoop
    {
        static int _chanceOfWeatherChange = 10;
        static int _weatherLength = 3;
        static int _armySize;
        static Unit.Weather[] weathers = new Unit.Weather[4];
        static List<Unit> _units = new List<Unit>();

        internal static List<Unit> Units { get => _units; private set => _units = value; }
        public static int ArmySize { get => _armySize; private set => _armySize = value; }

        public static void AddToUnitList(Unit unit)
        {
            Units.Add(unit);
        }

        public static void StartGame()
        {
            Array.Clear(weathers);
            Console.WriteLine("How many units would you like to be in each army?");
            bool validArmySize = int.TryParse(Console.ReadLine(), out _armySize);
            if(!validArmySize || ArmySize <= 0)
            {
                Console.WriteLine("Number of units must be an interger greater than 0, bozo");
                return;
            }
            //Paladin paladin = new Paladin(3, 30);
            //Wizard wizard = new Wizard(1, 10);
            //Zombie zombie = new Zombie(2, 1);
            //ElementalArcher elementalArcher = new ElementalArcher(5, 15, ElementalArcher.Elements.fire);
            //Ghoul ghoul = new Ghoul(1, 1);
            //Barbarian barbarian = new Barbarian(1, 1);
            //ForestWarrior forestWarrior = new ForestWarrior(1, 1);
            //Rogue rogue = new Rogue(1, 1);
            //Vampire vampire = new Vampire(1, 1);
            //vampire.BackOff();
            //wizard.BackOff();
            //paladin.Attack(wizard);
            //zombie.Attack(elementalArcher);
            //forestWarrior.Attack(ghoul);
            //rogue.Attack(vampire);
            //vampire.Attack(barbarian);
            //wizard.Attack(paladin, _units);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal static class GameLoop
    {
        static List<Unit> _units = new List<Unit>();

        public static void AddToUnitList(Unit unit)
        {
            _units.Add(unit);
        }

        public static void StartGame()
        {
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
    }
}

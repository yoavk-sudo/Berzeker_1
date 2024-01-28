using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class ForestWarrior : MeleeUnit
    {
        public ForestWarrior(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            RaceOfUnit = Races.Race.elf;
        }
        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            Console.WriteLine($"{this} used hax and is attacking {enemy} once again!");
            base.Attack(enemy);
        }
    }
}

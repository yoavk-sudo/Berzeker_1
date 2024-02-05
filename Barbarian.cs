using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Barbarian : MeleeUnit
    {
        int _baseModifier = 0;
        public Barbarian(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hitChance, hp);
            RaceOfUnit = Races.Race.human;
            //Damage.SetModifier(0);
        }

        public override string ToString()
        {
            return "B" + base.ToString();
        }

        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            //Damage.SetModifier(++_baseModifier);
            Console.WriteLine("Barbarian is getting angrier!");
        }

        public override void Defend(Unit enemy, int dmg)
        {
            base.Defend(enemy, dmg);
            TakeDamage(1);
        }
    }
}

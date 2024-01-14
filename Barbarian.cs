using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Barbarian : MeleeUnit
    {
        public Barbarian(int damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Race.human;
        }

        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            Damage++;
            Console.WriteLine("Barbarian is getting angrier!");
        }

        public override void Defend(Unit enemy)
        {
            TakeDamage(enemy.Damage + 1);
        }
    }
}

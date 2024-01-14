using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Vampire : MagicUnit
    {
        float _evasionChance = 0.3f;
        public Vampire(int damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Race.undead;
        }
        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            HealthPoints++;
        }
        public override void Defend(Unit enemy)
        {
            if (EvasionAttempt())
            {
                Console.WriteLine("Vampire dematerialized, letting the attack pass through!");
                return;
            }
            base.Defend(enemy);
        }

        private bool EvasionAttempt()
        {
            Random random = new Random();
            random.NextDouble();
            return _evasionChance >= random.NextDouble();
        }
    }
}

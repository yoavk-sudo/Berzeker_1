using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal abstract class MeleeUnit : Unit
    {
        protected MeleeUnit(int damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
        }

        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            if (RaceOfUnit == Race.undead)
                HealthPoints++;
        }
    }
}

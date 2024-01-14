using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal class Ghoul : MagicUnit
    {
        public Ghoul(int damagePoints, int hp) : base(damagePoints, hp)
        {
            RaceOfUnit = Race.undead;
            Damage = damagePoints + hp;
            HealthPoints = 1;
        }
    }
}

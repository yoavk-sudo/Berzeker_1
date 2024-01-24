using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal class Ghoul : MagicUnit
    {
        public Ghoul(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Races.Race.undead;
            Damage.SetModifier(hp);
            HealthPoints = 1;
        }
    }
}

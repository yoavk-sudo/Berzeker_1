using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal class Ghoul : MagicUnit
    {
        public Ghoul(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hitChance, hp);
            RaceOfUnit = Races.Race.undead;
            Damage.ChangeRandomWeights(hp);
            HealthPoints = 5;
        }
    }
}

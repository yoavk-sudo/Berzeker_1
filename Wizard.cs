using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Wizard : MagicUnit
    {
        float _aoeChance = 0.1f;
        public Wizard(int damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Race.elf;
        }

        public void Attack(Unit enemy, List<Unit> units)
        {
            base.Attack(enemy);
            if (CalculateChance())
            {
                Console.WriteLine("Wizard summoned meteors and devestated the land!");
                foreach (var unit in units)
                {
                    unit.Defend(this);
                }
            }
        }

        private bool CalculateChance()
        {
            Random random = new Random();
            random.NextDouble();
            return _aoeChance >= random.NextDouble();
        }
    }
}

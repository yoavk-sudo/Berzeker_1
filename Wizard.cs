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
        
        public Wizard(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Race.elf;
        }

        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            if (CalculateChance())
            {
                List<Unit> units = GameLoop.Units;
                Console.WriteLine("Wizard summoned meteors and devestated the land!");
                foreach (var unit in units)
                {
                    unit.Defend(this);
                }
            }
        }

        protected override void WeatherEffect(Weather weather)
        {
            Damage.SetModifier(2);
        }

        private bool CalculateChance()
        {
            Random random = new Random();
            random.NextDouble();
            return _aoeChance >= random.NextDouble();
        }
    }
}

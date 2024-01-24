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
        float _increasedAoeChance = 0.3f;
        
        public Wizard(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Races.Race.elf;
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
                    unit.Defend(this, Damage.Roll());
                }
            }
        }

        protected override void WeatherEffect(Weather weather)
        {
            if(weather == Weather.ClearSkies)
            {
                _aoeChance = _increasedAoeChance;
            }
            else
            {
                _aoeChance = 0.1f;
            }
            base.WeatherEffect(weather);
        }

        private bool CalculateChance()
        {
            Random random = new Random();
            random.NextDouble();
            return _aoeChance >= random.NextDouble();
        }
    }
}

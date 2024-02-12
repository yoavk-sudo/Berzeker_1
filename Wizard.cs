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
        
        public Wizard(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hitChance, hp);
            RaceOfUnit = Races.Race.elf;
        }

        public override void Attack(Unit enemy)
        {
            if (!CalculateChance())
            {
                base.Attack(enemy);
                return;
            }
            Console.WriteLine("Wizard summoned meteors and devestated the land!");
            Console.WriteLine("Everyone on the battlefield is a target!");
            foreach (var unit in GameLoop.Units)
            {
                if (unit.IsDead)
                    continue;
                unit.Defend(this, Damage.GetRandomInt());
            }
        }

        public override void WeatherEffect(Weather weather)
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

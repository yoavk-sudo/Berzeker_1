using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Rogue : MeleeUnit
    {
        float _critChance = 0.5f;
        float _evasionChance = 0.1f;
        static Races.Race _race = Races.Race.human;

        public Rogue(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            HitChance.ChangeModifier(+1);
            RaceOfUnit = Races.Race.human;
        }

        public override void Attack(Unit enemy)
        {
            //int tempDamage = Damage;
            if (CalculateChance(_critChance))
            {
                //Damage *= 3;
                Console.WriteLine("Rogue stabbed the enemy through the heart!");
            }
            base.Attack(enemy);
            //Damage = tempDamage;
        }

        public override void Defend(Unit enemy, int dmg)
        {
            if (CalculateChance(_evasionChance))
            {
                Console.WriteLine("Rogue rolled out of the way!");
                return;
            }
            base.Defend(enemy, dmg);
        }

        protected override void WeatherEffect(Weather weather)
        {
            throw new NotImplementedException();
        }

        private bool CalculateChance(float chance)
        {
            Random random = new Random();
            random.NextDouble();
            return chance >= random.NextDouble();
        }
    }
}

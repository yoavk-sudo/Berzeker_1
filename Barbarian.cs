using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Barbarian : MeleeUnit
    {
        int _baseModifier = 0;
        public Barbarian(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Race.human;
            Damage.SetModifier(0);
        }

        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            Damage.SetModifier(++_baseModifier);
            Console.WriteLine("Barbarian is getting angrier!");
        }

        public override void Defend(Unit enemy)
        {
            base.Defend(enemy);
            TakeDamage(1);
        }

        protected override void WeatherEffect(Weather weather)
        {
            throw new NotImplementedException();
        }
    }
}

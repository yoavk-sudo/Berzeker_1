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
        int _critDamage = 3;
        float _evasionChance = 0.1f;

        public Rogue(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            HitChance.ChangeModifier(+1);
            RaceOfUnit = Races.Race.human;
        }

        public override void Attack(Unit enemy)
        {
            if (CalculateChance(_critChance))
            {
                int tempDamage = Damage.Modifier;
                Damage.SetModifier(tempDamage * _critDamage);
                Console.WriteLine($"{this} is aiming for {enemy}'s heart!");
                enemy.Defend(this, Damage.Roll());
                Damage.SetModifier(tempDamage);
                return;
            }
            base.Attack(enemy);
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

        private bool CalculateChance(float chance)
        {
            Random random = new Random();
            random.NextDouble();
            return chance >= random.NextDouble();
        }
    }
}

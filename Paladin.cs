using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Paladin : MeleeUnit
    {
        public Paladin(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hitChance, hp);
            RaceOfUnit = Races.Race.human;
        }

        public override void Attack(Unit enemy)
        {
            if(enemy is MagicUnit magicEnemy)
            {
                CloseDistanceAndAttack(enemy, magicEnemy);
                return;
            }
            base.Attack(enemy);
        }

        private void CloseDistanceAndAttack(Unit enemy, MagicUnit magicEnemy)
        {
            magicEnemy.CloseDistance();
            base.Attack(enemy);
            magicEnemy.BackOff();
        }

        public override void Defend(Unit enemy, int dmg)
        {
            if (dmg > HealthPoints)
                dmg = HealthPoints - 1;
            if (dmg == 0)
                dmg = 1;
            base.Defend(enemy, dmg);
            if (HealthPoints == 1)
            {
                HealthPoints = 1;
                Console.WriteLine($"{this} has barely hung on with {HealthPoints} health points!");
                return;
            }
        }
    }
}

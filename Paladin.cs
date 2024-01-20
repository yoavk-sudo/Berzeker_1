using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Paladin : MeleeUnit
    {
        public Paladin(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Race.human;
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

        public override void Defend(Unit enemy)
        {
            int damage = enemy.Damage.LastRollValue;
            if (damage >= HealthPoints)
            {
                HealthPoints = 1;
                return;
            }
            base.Defend(enemy);
        }

        protected override void WeatherEffect(Weather weather)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal abstract class MagicUnit : Unit
    {
        int _range = 1;
        const int RANGEMODIFIERAMOUNT = 3;

        public override int Damage { get { return _damage + Range; } 
            protected set 
            { 
                if (_damage < 0) _damage = 0; 
                else _damage = value; 
            } 
        }

        protected int Range { get => _range; set { if (_range < 0) _range = 0; else _range = value; } }

        public MagicUnit(int damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
        }

        public void BackOff()
        {
            IncreaseRange();
        }

        public void CloseDistance()
        {
            DecreaseRange();
        }

        private void IncreaseRange()
        {
            Range += RANGEMODIFIERAMOUNT;
        }

        private void DecreaseRange()
        {
            Range -= RANGEMODIFIERAMOUNT;
        }

        public override void Attack(Unit enemy)
        {
            int tempDamage = Damage;
            if (RaceOfUnit == Race.elf)  Damage *= 2;
            base.Attack(enemy);
            Damage = tempDamage;
        }

        public override void Defend(Unit enemy)
        {
            int enemyDamage = enemy.Damage;
            if (_range > 1) enemyDamage--;
            TakeDamage(enemyDamage);
        }
    }
}

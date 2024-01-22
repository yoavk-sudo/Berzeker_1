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

        public override Dice Damage { get { return _damage; } 
            protected set 
            { 
                _damage = value;
            } 
        }

        protected int Range { get => _range; set { if (_range < 0) _range = 0; else _range = value; } }

        public MagicUnit(Dice damagePoints, int hp) : base(damagePoints, hp)
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
            int tempDamage = Damage.Roll();
            //if (RaceOfUnit == Race.elf)  Damage.LastRollValue *= 2;
            base.Attack(enemy);
            //Damage = tempDamage;
        }

        public override void Defend(Unit enemy, int dmg)
        {
            int enemyDamage = dmg;
            if (_range > 1) enemyDamage--;
            TakeDamage(enemyDamage);
        }

        protected override void WeatherEffect(Weather weather)
        {
            switch (weather)
            {
                case Weather.ClearSkies:
                    if (RaceOfUnit == Races.Race.undead)
                        TakeDamage(1);
                    HitChance.ChangeModifier(+1);
                    if(this is Wizard)
                        Console.WriteLine();
                    break;
                case Weather.Cloudy:
                if (this is not Vampire)
                    Damage.ChangeModifier(-1);
                break;
            }
        }
    }
}

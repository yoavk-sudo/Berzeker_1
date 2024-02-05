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

        public override IRandomProvider Damage { get { return _damage; } 
            protected set 
            { 
                _damage = value;
            } 
        }

        protected int Range { get => _range; set { if (_range < 0) _range = 0; else _range = value; } }

        public MagicUnit(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hitChance, hp);
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
            //int tempDamage = Damage.Roll();
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

        public override void WeatherEffect(Weather weather)
        {
            switch (weather)
            {
                case Weather.ClearSkies:
                    if (RaceOfUnit == Races.Race.undead)
                    {
                        Console.WriteLine($"{this} feels the burning sunlight on their skin...");
                        TakeDamage(1);
                    }
                    //HitChance.ChangeModifier(+1);
                    break;
                case Weather.Cloudy:
                    if (this is not Vampire)
                    {
                        //HitChance.ChangeModifier(-1);
                    }
                break;
            }
        }
    }
}

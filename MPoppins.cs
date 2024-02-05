using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class MPoppins : Unit
    {
        private Bag _bag;

        private MPoppins(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
        }

        public MPoppins(Dice damagePoints, Dice hitChance, int hp, Bag bag) : this(damagePoints, hitChance, hp)
        {
            _bag = bag;
        }

        public override void Attack(Unit enemy)
        {
            if(IsDead) return;
            int dmg = _bag.GetAndRemoveRandomItemFromBag();
            Console.WriteLine("dmg is "+ dmg);
            enemy.Defend(this, dmg);
        }

        public override void Defend(Unit enemy, int dmg)
        {
            if(dmg <= _bag.GetAndRemoveRandomItemFromBag())
            {
                Console.WriteLine("Blocked");
                return;
            }
            base.Defend(enemy, dmg);
        }

        public override void WeatherEffect(Weather weather)
        {
            switch (weather)
            {
                case Weather.ClearSkies:
                    break;
                case Weather.Cloudy:
                    break;
                case Weather.Scorching:
                    break;
                case Weather.Hail:
                    break;
                default:
                    break;
            }
        }
    }
}

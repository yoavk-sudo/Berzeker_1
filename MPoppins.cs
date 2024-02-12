using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class MPoppins : MagicUnit
    {
        private Bag _bag;

        private MPoppins(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
            RaceOfUnit = Races.Race.elf;
        }

        public MPoppins(Dice damagePoints, Dice hitChance, int hp, Bag bag) : this(damagePoints, hitChance, hp)
        {
            _bag = bag;
        }

        public override void Attack(Unit enemy)
        {
            if(IsDead) return;
            int dmg = _bag.GetAndRemoveRandomItemFromBag();
            Console.WriteLine("Nanny's damage is "+ dmg);
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
    }
}

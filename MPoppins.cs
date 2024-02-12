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
            if(HitChance.GetAverageRandom() <= _bag.GetAverageRandom())
            {
                Console.WriteLine(this + " missed.");
                return;
            }
            int dmg = _bag.GetRandomInt();
            Console.WriteLine(this + "'s damage is "+ dmg);
            enemy.Defend(this, dmg);
        }

        public override void Defend(Unit enemy, int dmg)
        {
            int blockItem = _bag.GetRandomInt();
            if (dmg <= blockItem)
            {
                Console.WriteLine(this + " pulled a penguin weighing " + blockItem + " kilos and used it block");
                Console.WriteLine(this + "'s bag of cheats becomes even more unfair as she stuffs into it another fat penguin");
                _bag.ChangeRandomWeights(blockItem);
                return;
            }
            base.Defend(enemy, dmg);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class ElementalArcher : MagicUnit
    {
        Elements _element;

        private ElementalArcher(int damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Race.elf;
        }
        public ElementalArcher(int damagePoints, int hp, Elements element) : this(damagePoints, hp)
        {
            _element = element;
        }

        public override void Attack(Unit enemy)
        {
            int tempDamage = Damage;
            if (EnemyWeakToElement(enemy))
            {
                Damage *= 2;
                Console.WriteLine("Attack was super effective!");
            }
            base.Attack(enemy);
            Damage = tempDamage;
        }

        private bool EnemyWeakToElement(Unit enemy)
        {
            if(enemy.RaceOfUnit == Race.elf && _element == Elements.wind) return true;
            if(enemy.RaceOfUnit == Race.human && _element == Elements.ice) return true;
            if(enemy.RaceOfUnit == Race.undead && _element == Elements.fire) return true;
            return false;
        }

        public enum Elements
        {
            fire,
            ice,
            wind
        }
    }
}

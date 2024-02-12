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
        protected override int HealthPoints { get => base.HealthPoints; set => Math.Clamp(value, 0, 10); }

        private ElementalArcher(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hitChance, hp);
            RaceOfUnit = Races.Race.elf;
        }
        public ElementalArcher(Dice damagePoints, Dice hitChance, int hp, Elements element) : this(damagePoints, hitChance, hp)
        {
            _element = element;
        }

        public override void Attack(Unit enemy)
        {
            if (EnemyWeakToElement(enemy))
            {
                Damage.ChangeRandomWeights(+3);
                Console.WriteLine("Attack was super effective!");
                base.Attack(enemy);
                Damage.ChangeRandomWeights(-3);
            }
            base.Attack(enemy);
        }

        private bool EnemyWeakToElement(Unit enemy)
        {
            if(enemy.RaceOfUnit == Races.Race.undead && _element == Elements.fire) return true;
            if(enemy.RaceOfUnit == Races.Race.human && _element == Elements.ice) return true;
            if(enemy.RaceOfUnit == Races.Race.elf && _element == Elements.wind) return true;
            return false;
        }

        public override void WeatherEffect(Weather weather)
        {
            HitChance.ChangeRandomWeights(1);
            DefenseRating.ChangeRandomWeights(+1);
            switch (weather)
            {
                case Weather.Scorching:
                    if (_element != Elements.fire)
                    {
                        break;
                    }
                    Damage.ChangeRandomWeights(+1);
                    return;
                case Weather.Hail:
                    if (_element != Elements.ice)
                    {
                        break;
                    }
                    DefenseRating.ChangeRandomWeights(+1);
                    return;
                case Weather.Cloudy:
                    if (_element != Elements.wind)
                    {
                        break;
                    }
                    HitChance.ChangeRandomWeights(+1);
                    return;
            }
            //weather is clear or elements mismatch
            HitChance.ChangeRandomWeights(-1);
            DefenseRating.ChangeRandomWeights(-1);
            Damage.ChangeRandomWeights(-1);
        }

        public enum Elements
        {
            fire,
            ice,
            wind
        }
    }
}

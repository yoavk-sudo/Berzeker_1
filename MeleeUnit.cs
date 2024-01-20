using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal abstract class MeleeUnit : Unit
    {
        protected MeleeUnit(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
        }

        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            if (RaceOfUnit == Race.undead)
                HealthPoints++;
        }

        protected override void WeatherEffect(Weather weather)
        {
            switch(weather)
            {
                case Weather.ClearSkies:
                    if (RaceOfUnit == Race.undead)
                        DefenseRating.ChangeModifier(-3);
                    break;
                case Weather.Scorching:
                    if (RaceOfUnit == Race.elf)
                        TakeDamage(1);
                    break;
                case Weather.Hail:
                    if (RaceOfUnit == Race.human)
                        TakeDamage(1);
                    break;
                case Weather.Cloudy:
                    if (this is Rogue)
                        HitChance.ChangeModifier(+1);
                    break;
            }
        }
    }
}

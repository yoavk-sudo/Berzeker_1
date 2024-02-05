using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal abstract class MeleeUnit : Unit
    {
        protected MeleeUnit(Dice damagePoints, Dice hitChance, int hp) : base(damagePoints, hitChance, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hitChance, hp);
        }

        public Dice DamagePoints { get; }
        public int Hp { get; }

        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            if (RaceOfUnit == Races.Race.undead)
                HealthPoints++;
        }

        public override void WeatherEffect(Weather weather)
        {
            switch(weather)
            {
                case Weather.ClearSkies:
                    if (RaceOfUnit == Races.Race.undead)
                    {
                        //DefenseRating.ChangeModifier(-3);
                    }
                    break;
                case Weather.Scorching:
                    if (RaceOfUnit == Races.Race.elf)
                    {
                        Console.WriteLine("Elven warriors are not built for this heat...");
                        TakeDamage(1);
                    }
                    break;
                case Weather.Hail:
                    if (RaceOfUnit == Races.Race.human)
                    {
                        Console.WriteLine($"{this} is being buffeted by chunks of ice!");
                        TakeDamage(1);
                    }
                    break;
                case Weather.Cloudy:
                    if (this is Rogue)
                    {
                        //HitChance.ChangeModifier(+1);
                    }
                    break;
            }
        }
    }
}

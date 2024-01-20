﻿using System;
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

        private ElementalArcher(Dice damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            RaceOfUnit = Race.elf;
        }
        public ElementalArcher(Dice damagePoints, int hp, Elements element) : this(damagePoints, hp)
        {
            _element = element;
        }

        public override void Attack(Unit enemy)
        {
            if (EnemyWeakToElement(enemy))
            {
                Damage.AddDice(1);
                Console.WriteLine("Attack was super effective!");
                base.Attack(enemy);
                Damage.RemoveDice(1);
            }
            base.Attack(enemy);
        }

        private bool EnemyWeakToElement(Unit enemy)
        {
            if(enemy.RaceOfUnit == Race.undead && _element == Elements.fire) return true;
            if(enemy.RaceOfUnit == Race.human && _element == Elements.ice) return true;
            if(enemy.RaceOfUnit == Race.elf && _element == Elements.wind) return true;
            return false;
        }

        protected override void WeatherEffect(Weather weather)
        {
            HitChance.SetModifier(1);
            DefenseRating.SetModifier(1);
            switch(weather)
            {
                case Weather.Scorching:
                    if (_element == Elements.fire) Damage.ChangeModifier(+1);
                    break;
                case Weather.Hail:
                    if (_element == Elements.ice) DefenseRating.ChangeModifier(+1);
                    break;
                case Weather.Cloudy:
                    if(_element == Elements.wind) HitChance.ChangeModifier(+1);
                    break;
            }
            HitChance.SetModifier(-1);
            DefenseRating.SetModifier(-1);
            Damage.SetModifier(-1);
        }

        public enum Elements
        {
            fire,
            ice,
            wind
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal abstract partial class Unit
    {
        private char[] _projectName = "Berzerker_1.".ToCharArray();
        protected IRandomProvider _damage;
        protected int _loot;

        protected int CarryingCapacity { get; set; } = 2;
        public int Loot { get => _loot; set => _loot = Math.Clamp(value, 0, CarryingCapacity); }
        protected IRandomProvider HitChance { get; set; }
        protected IRandomProvider DefenseRating { get; set; }
        public virtual bool IsDead { get { return HealthPoints <= 0; } }
        protected virtual int HealthPoints { get; set; }
        public override string ToString()
        {
            return base.ToString().TrimStart(_projectName);
        }
        public virtual IRandomProvider Damage
        {
            get { return _damage; }
            protected set
            {
                _damage = value;
            }
        }
        public virtual Races.Race RaceOfUnit { get; protected set; }

        protected Unit(Dice damagePoints, Dice hitChance, int hp)
        {
            AssignBaseStatsToUnit(damagePoints, hitChance, hp);
            GameLoop.AddToUnitList(this);
        }

        protected void AssignBaseStatsToUnit(Dice damagePoints, Dice hitChance, int hp)
        {
            Damage = damagePoints;
            DefenseRating = Dice.GenerateRandomDice();
            HitChance = hitChance;
            //HitChance.ChangeModifier(-(int)RaceOfUnit);
            HealthPoints = hp;
            CarryingCapacity = Dice.GenerateRandomDice().Roll();
        }

        public virtual void Attack(Unit enemy)
        {
            if (IsDead || enemy.IsDead)
                return;
            Console.WriteLine(this + " is attempting to attack. Will it hit their target?");
            if (HitChance.GetRandomInt(0, 0) < 0)
            {
                Console.WriteLine(this + " missed!");
                return;
            }
            Console.WriteLine(this + " managed to hit! But for how much?");
            int dmg = Damage.GetRandomInt(0,0);
            if (dmg == 0) //until unit attacks again, this is their damage value
            {
                Console.WriteLine(this + " completely fumbled their attack!");
                return;
            }
            Console.WriteLine($"{this} rolled a whopping {dmg}! Will {enemy} manage to block this upcoming attack?");
            enemy.Defend(this, dmg);
        }

        public virtual void Defend(Unit enemy, int dmg)
        {
            if (DefenseRating.GetRandomInt(0,0) >= dmg)
            {
                Console.WriteLine(this + " succefully blocked " + enemy + "'s attack!");
                return;
            }
            Console.WriteLine("Defense roll failed. Ouch.");
            EnemyLootingUnit(enemy, dmg);
            TakeDamage(dmg);
        }

        private void EnemyLootingUnit(Unit enemy, int dmg)
        {
            int lootStolen = 0;
            for (int i = 0; i < dmg; i++)
            {
                if (enemy.Loot >= enemy.CarryingCapacity || Loot == 0)
                    break;
                enemy.Loot++;
                lootStolen++;
            }
            Loot -= lootStolen;
            Console.WriteLine($"{this} lost {lootStolen} loot.");
        }

        protected void TakeDamage(int damage)
        {
            if (damage <= 0) 
            {
                return;
            }
            HealthPoints -= damage;
            Console.WriteLine(this.ToString() + "'s HP is " + Math.Max(HealthPoints, 0));
            if (HealthPoints <= 0)
            {
                HealthPoints = 0;
                Console.WriteLine("Unit is dead!");
                if(this is Zombie)
                {
                    //revive if zombie has not died before
                    bool isDead = IsDead;
                }
            }
        }

        public abstract void WeatherEffect(Weather weather);

        public enum Weather
        {
            ClearSkies,
            Cloudy,
            Scorching,
            Hail
        }
    }
}

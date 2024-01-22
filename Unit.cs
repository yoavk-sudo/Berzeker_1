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
        protected Dice _damage;
        protected int _loot;

        protected int CarryingCapacity { get; set; }
        public int Loot { get => _loot; set => _loot = Math.Clamp(value, 0, CarryingCapacity); }
        protected Dice HitChance { get; set; }
        protected Dice DefenseRating { get; set; }
        public virtual bool IsDead { get { return HealthPoints <= 0; } }
        protected virtual int HealthPoints { get; set; }
        public override string ToString()
        {
            return base.ToString().TrimStart(_projectName);
        }
        public virtual Dice Damage
        {
            get { return _damage; }
            protected set
            {
                _damage = value;
            }
        }
        public virtual Races.Race RaceOfUnit { get; protected set; }

        protected Unit(Dice damagePoints, int hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            GameLoop.AddToUnitList(this);
        }

        protected void AssignBaseStatsToUnit(Dice damagePoints, int hp)
        {
            Damage = damagePoints;
            DefenseRating = damagePoints;
            HitChance = damagePoints;
            HitChance.ChangeModifier(-(int)RaceOfUnit);
            HealthPoints = hp;
        }

        public virtual void Attack(Unit enemy)
        {
            if (IsDead || enemy.IsDead)
                return;
            if(HitChance.Roll() <= 5)
            {
                Console.WriteLine(this + "missed!");
                return;
            }
            int dmg = Damage.Roll();
            if (dmg == 0) //until unit attacks again, this is their damage value
            {
                Console.WriteLine(this + " completely fumbled their attack!");
                return;
            }
            enemy.Defend(this, dmg);
        }

        public virtual void Defend(Unit enemy, int dmg)
        {
            if (DefenseRating.Roll() >= dmg)
            {
                Console.WriteLine(this + " succefully blocked " + enemy + "'s attack!");
                return;
            }
            Console.WriteLine("Defense roll failed.");
            EnemyLootingUnit(enemy);
            TakeDamage(dmg);
        }

        private void EnemyLootingUnit(Unit enemy)
        {
            int lootStolen = 0;
            for (int i = 0; i < enemy.Damage.LastRollValue; i++)
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
                    bool isDead = IsDead;
                }
            }
        }

        protected abstract void WeatherEffect(Weather weather);

        public enum Weather
        {
            ClearSkies,
            Cloudy,
            Scorching,
            Hail
        }
    }
}

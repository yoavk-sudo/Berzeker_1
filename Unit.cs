using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal abstract class Unit
    {
        private char[] _projectName = "Berzerker_1.".ToCharArray();
        protected int _damage;

        protected virtual bool IsDead { get { return HealthPoints <= 0; } }
        protected virtual int HealthPoints { get; set; }
        public override string ToString()
        {
            return base.ToString().TrimStart(_projectName);
        }
        public virtual int Damage
        {
            get { return _damage; }
            protected set
            {
                if (_damage < 0) _damage = 0;
                else _damage = value;
            }
        }
        public virtual Race RaceOfUnit { get; protected set; }

        protected Unit(int damagePoints, int hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            GameLoop.AddToUnitList(this);
        }

        protected void AssignBaseStatsToUnit(int damagePoints, int hp)
        {
            Damage = damagePoints;
            HealthPoints = hp;
        }

        public virtual void Attack(Unit enemy)
        {
            if (IsDead || enemy.IsDead) return;
            enemy.Defend(this);
        }

        public virtual void Defend(Unit enemy)
        {
            TakeDamage(enemy.Damage);
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

        public enum Race
        {
            elf,
            human,
            undead
        }
    }
}

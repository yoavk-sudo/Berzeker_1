using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class Zombie : MeleeUnit
    {
        bool _hasDied = false;
        int _hpRecorder;

        protected override bool IsDead { get 
            { 
                if(HealthPoints <= 0 && !_hasDied)
                {
                    _hasDied = true;
                    HealthPoints = _hpRecorder;
                    Console.WriteLine("Zombie was revived!");
                    return false;
                }
                return HealthPoints <= 0; 
            } 
        }

        public Zombie(int damagePoints, int hp) : base(damagePoints, hp)
        {
            AssignBaseStatsToUnit(damagePoints, hp);
            _hpRecorder = hp;
            RaceOfUnit = Race.undead;
        }
    }
}

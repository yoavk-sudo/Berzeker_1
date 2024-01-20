using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal sealed class ArmyGeneral
    {
        private Unit.Race _race;

        public List<Unit> Army { get; set; }
        public int Resources { get;}
        
        public ArmyGeneral(int resources, Unit.Race race)
        {
            Resources = resources;
            _race = race;
            GenerateArmy();
        }

        private void GenerateArmy()
        {
            int armySize = GameLoop.ArmySize;
            for (int i = 0; i < armySize; i++)
            {
                CreateUnit();
            }
        }

        private void CreateUnit()
        {
            //Army.Add(new Unit);
        }
    }
}

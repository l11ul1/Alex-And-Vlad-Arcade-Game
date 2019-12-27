using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GroupProjectAlexVlad.MenuLogic
{
    class SpaceShip
    {
        
        public Dictionary<string, int> SpaceShipStats { get; set; }

        public SpaceShip() => SpaceShipStats = new Dictionary<string, int>
        {
            ["ShipStrength"] = 100,
            ["ShipCapacity"] = 100,
            ["ShootSpeed"] = 40
        };
    }
}

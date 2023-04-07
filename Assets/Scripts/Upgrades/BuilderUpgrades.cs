using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Upgrades
{
    [Serializable]
    public class BuilderUpgrades
    {
        public float MovementSpeedBonus = 1f;
        public bool MovementSpeedBonusBought = false;
        public float BuildingSpeedMultiplier = 2f;
        public bool BuildingSpeedMultiplierBought = false;
        public bool InstantlyBuildBuildings = false;
        public bool InstantlyBuildBuildingsBought = false;
    }
}

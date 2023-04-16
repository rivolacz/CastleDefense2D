using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Upgrades
{
    [Serializable]
    public class KnightUpgrades
    {
        public bool KnightResearched = false;
        public DateTime KnightResearchStartTime = DateTime.MinValue;
        public float MovementSpeedBonus = 1;
        public bool MovementSpeedBonusBought = false;
        public float AttackSpeedBonus = .5f;
        public bool AttackSpeedBonusBought = false;
        public float AttackDamageBonus = 10;
        public bool AttackDamageBonusBought = false;
    }
}

using System;

namespace Project.Upgrades
{
    [Serializable]
    public class SwordsmanUpgrades
    {
        public bool Researched = false;
        public DateTime ResearchStartTime = DateTime.MinValue;
        public float MovementSpeedBonus = .5f;
        public bool MovementSpeedBonusBought = false;
        public float AttackSpeedBonus = .5f;
        public bool AttackSpeedBonusBought = false;
        public float AttackDamageBonus = 10;
        public bool AttackDamageBonusBought = false;
        public float HealthBonus = 10;
        public bool HealthBonusBought = false;
    }
}

using System;

namespace Project.Assets.Scripts.Upgrades
{
    [Serializable]
    public class SwordsmanUpgrades
    {
        public bool SwordsmanResearched = false;
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

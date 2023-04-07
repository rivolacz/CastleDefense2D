using System;

namespace Project.Upgrades
{
    [Serializable]
    public class FireballAbilityUpgrades
    {
        public float DamageBonus = 30;
        public bool DamageBonusBought = false;
        public bool InstantlyKillAllUnits = false;
        public bool InstantlyKillAllUnitsBought = false;
        public float ManaUsageReduction = 10;
        public bool ManaUsageReductionBought = false;
    }
}

using System;
namespace Project.Upgrades
{
    [Serializable]
    public class PoisonAbilityUpgrades
    {
        public float DamagePerSecondBonus = 1f;
        public bool DamagePerSecondBonusBought = false;
        public float EffectDurationBonus = 5f;
        public bool EffectDurationBonusBought = false;
        public float EffectRangeBonus = 2;
        public bool EffectRangeBonusBought = false;
    }
}

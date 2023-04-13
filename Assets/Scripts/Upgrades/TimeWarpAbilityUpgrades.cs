using System;

namespace Project.Upgrades
{
    [Serializable]
    public class TimeWarpAbilityUpgrades
    {
        public float MovementSlowDownBonus = .3f;
        public bool MovementSlowDownBonusBought = false;
        public float AttackSpeedSlowDownBonus = .3f;
        public bool AttackSpeedSlowDownBonusBought = false;
        public bool FreezeEnemies = false;
        public bool FreezeEnemiesBought = false;
        public float EffectDurationBonus = 5f;
        public bool EffectDurationBonusBought = false;
    }
}

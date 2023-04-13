using Project.Objects;
using Project.Upgrades;
using System;
using UnityEngine;

namespace Project.Abilities
{
    [System.Serializable]
    public class TimeWarpAbility : Ability
    {
        [SerializeField]
        private float ManaCost;
        [SerializeField]
        private GameObject timewarpPrefab;
        private Vector2 target;
        private const float Range = 10;
        private const float baseEffectDuration = 5;
        private const float baseAttackSpeedSlowDown = 0.3f;
        private const float baseMovementSpeedSlowDown = 0.4f;
        private TimeWarpAbilityUpgrades TimeWarpAbilityUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.TimeWarpAbilityUpgrades;
            }
        }

        public override void Cast()
        {
            GameObject poison = UnityEngine.Object.Instantiate(timewarpPrefab);
            TimeWarp timewarp = poison.GetComponent<TimeWarp>();
            timewarp.SetData(target, GetRange(),GetMovementSlowDown(), GetAttackSpeedSlowDown(), GetEffectDuration());
        }

        public override float GetManaCost()
        {
            return ManaCost;
        }

        public override void SetTarget(Vector2 target)
        {
            this.target = target;
        }

        public float GetRange()
        {
            return Range;
        }

        public float GetEffectDuration()
        {
            float effectDurationBonus = 0;
            if (TimeWarpAbilityUpgrades.EffectDurationBonusBought)
            {
                effectDurationBonus = TimeWarpAbilityUpgrades.EffectDurationBonus;
            }
            return baseEffectDuration + effectDurationBonus;
        }

        public float GetAttackSpeedSlowDown()
        {
            float bonus = 0;
            if (TimeWarpAbilityUpgrades.AttackSpeedSlowDownBonusBought)
            {
                bonus = TimeWarpAbilityUpgrades.AttackSpeedSlowDownBonus;
            }
            return baseAttackSpeedSlowDown + bonus;
        }

        public float GetMovementSlowDown()
        {
            float bonus = 0;
            if (TimeWarpAbilityUpgrades.MovementSlowDownBonusBought)
            {
                bonus += TimeWarpAbilityUpgrades.MovementSlowDownBonus;
            }
            if (TimeWarpAbilityUpgrades.FreezeEnemiesBought)
            {
                bonus += 20;
            }
            return baseMovementSpeedSlowDown + bonus;
        }
    }
}

using Project.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Abilities
{
    [System.Serializable]
    public class PoisonAbility : Ability
    {
        [SerializeField]
        private float ManaCost = 75;
        [SerializeField]
        private GameObject poisonPrefab;

        private Vector2 target;
        public const float baseRange = 10;
        public const float baseDamagePerSecond = 3;
        public const float baseEffectDuration = 10;
        private PoisonAbilityUpgrades PoisonAbilityUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.PoisonAbilityUpgrades;
            }
        }

        public override void Cast()
        {
            GameObject poison = UnityEngine.Object.Instantiate(poisonPrefab);
            PoisonArea PoisonArea = poison.GetComponent<PoisonArea>();
            PoisonArea.SetData(target, GetRange(), GetDamagePerSecond(), GetEffectDuration());
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
            float bonusRange = 0;
            if (UpgradesManager.Upgrades.PoisonAbilityUpgrades.EffectRangeBonusBought)
            {
                bonusRange = UpgradesManager.Upgrades.PoisonAbilityUpgrades.EffectRangeBonus;
            }
            return baseRange + bonusRange;
        }

        public float GetDamagePerSecond()
        {
            float damageBonus = 0;
            if (PoisonAbilityUpgrades.DamagePerSecondBonusBought)
            {
                damageBonus = PoisonAbilityUpgrades.DamagePerSecondBonus;
            }
            return damageBonus + baseDamagePerSecond;
        }

        public float GetEffectDuration()
        {
            float effectDurationBonus = 0;
            if (PoisonAbilityUpgrades.EffectDurationBonusBought)
            {
                effectDurationBonus = PoisonAbilityUpgrades.EffectDurationBonus;
            }
            return baseEffectDuration + effectDurationBonus;
        }
    }
}

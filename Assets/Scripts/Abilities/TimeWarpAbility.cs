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
        private TimeWarpAbilityUpgrades TimeWarpAbilityUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.TimeWarpAbilityUpgrades;
            }
        }

        public override void Cast()
        {
            throw new NotImplementedException();
        }

        public override float GetManaCost()
        {
            return ManaCost;
        }

        public override void SetTarget(Vector2 target)
        {
            throw new NotImplementedException();
        }
    }
}

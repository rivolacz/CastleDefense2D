using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Units;
using Project.Upgrades;

namespace Project
{
    public class Swordsman : PlayerUnit
    {
        private SwordsmanUpgrades upgrades{
            get
            {
                return UpgradesManager.Upgrades.SwordsmanUpgrades;
            }
        }

        private void Awake()
        {
            if (upgrades.MovementSpeedBonusBought)
            {
                StateMachine.SetMovementSpeedBonus(upgrades.MovementSpeedBonus);
            }
            if (upgrades.AttackDamageBonusBought)
            {
                damageBonus = upgrades.AttackDamageBonus;
            }
            if (upgrades.AttackSpeedBonusBought)
            {
                StateMachine.unitAnimatorValuesSetter.SetAnimatorSpeed(1 + upgrades.AttackSpeedBonus);
            }
            if (upgrades.HealthBonusBought)
            {
                SetHealthBonus(upgrades.HealthBonus);
            }
        }
    }
}

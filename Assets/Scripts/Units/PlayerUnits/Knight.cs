using Project.Upgrades;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Knight : AttackUnit
    {
        private KnightUpgrades upgrades
        {
            get
            {
                return UpgradesManager.Upgrades.KnightUpgrades;
            }
        }

        private void OnEnable()
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
        }
    }
}

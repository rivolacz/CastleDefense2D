using Project;
using Project.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeman : PlayerUnit
{
    private PikemanUpgrades upgrades;
    private void OnEnable()
    {
        upgrades = UpgradesManager.Upgrades.PikemanUpgrades;
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

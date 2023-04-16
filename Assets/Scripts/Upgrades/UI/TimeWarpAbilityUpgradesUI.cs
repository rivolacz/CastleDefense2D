using Project.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Project.Upgrades.UI
{
    public class TimeWarpAbilityUpgradesUI : UpgradesUI
    {
        TimeWarpAbilityUpgrades TimeWarpAbilityUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.TimeWarpAbilityUpgrades;
            }
        }

        [SerializeField] private TMP_Text movementSlowDownCostText;
        [SerializeField] private GameObject movementSlowDownButtonGameObject;
        private float movementSlowDownCost = 200;

        [SerializeField] private TMP_Text attackSpeedSlowDownCostText;
        [SerializeField] private GameObject attackSpeedSlowDownButtonGameObject;
        private float attackSpeedSlowDownCost = 200;

        [SerializeField] private TMP_Text freezeEnemiesCostText;
        [SerializeField] private GameObject freezeEnemiesButtonGameObject;
        private float freezeEnemiesCost = 1000;

        [SerializeField] private TMP_Text effectLengthCostText;
        [SerializeField] private GameObject effectLengthButtonGameObject;
        private float effectLengthCost = 150;

        private void OnEnable()
        {
            CheckBoughtUpgrades();
        }

        private void Start()
        {
            CheckBoughtUpgrades();
        }

        private void CheckBoughtUpgrades()
        {
            float currentMoney = UpgradesManager.Upgrades.Coins;
            if (TimeWarpAbilityUpgrades == null) return;
            UpdateMoneyText();

            CheckForUpgrade(currentMoney, TimeWarpAbilityUpgrades.MovementSlowDownBonusBought, movementSlowDownCost, movementSlowDownCostText, movementSlowDownButtonGameObject);
            CheckForUpgrade(currentMoney, TimeWarpAbilityUpgrades.AttackSpeedSlowDownBonusBought, attackSpeedSlowDownCost, attackSpeedSlowDownCostText, attackSpeedSlowDownButtonGameObject);
            CheckForUpgrade(currentMoney, TimeWarpAbilityUpgrades.FreezeEnemiesBought, freezeEnemiesCost, freezeEnemiesCostText, freezeEnemiesButtonGameObject);
            CheckForUpgrade(currentMoney, TimeWarpAbilityUpgrades.EffectDurationBonusBought, effectLengthCost, effectLengthCostText, effectLengthButtonGameObject);
        }

        public void BuyMovementSlowDownBonus()
        {
            bool bought = UpgradesManager.Buy(movementSlowDownCost);
            if (bought)
            {
                TimeWarpAbilityUpgrades.MovementSlowDownBonusBought = true;
                UpgradesManager.SendDataToAnalytics("TimeWarp-movementSlowDown");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyAttackSpeedSlowDownBonus()
        {
            bool bought = UpgradesManager.Buy(attackSpeedSlowDownCost);
            if (bought)
            {
                TimeWarpAbilityUpgrades.AttackSpeedSlowDownBonusBought = true;
                UpgradesManager.SendDataToAnalytics("TimeWarp-attackSlowDown");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyFreezeEnemies()
        {
            bool bought = UpgradesManager.Buy(freezeEnemiesCost);
            if (bought)
            {
                TimeWarpAbilityUpgrades.FreezeEnemiesBought = true;
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyEffectLengthBonus()
        {
            bool bought = UpgradesManager.Buy(effectLengthCost);
            if (bought)
            {
                TimeWarpAbilityUpgrades.EffectDurationBonusBought = true;
                UpgradesManager.SendDataToAnalytics("TimeWarp-effectLength");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }
    }
}

using Project.Upgrades;
using Project.Upgrades.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project
{
    public class ArcherTurretUpgradesUI : UpgradesUI
    {
        ArcherTurretUpgrades ArcherTurretUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.ArcherTurretUpgrades;
            }
        }

        [SerializeField]
        private TMP_Text attackSpeedCostText;
        [SerializeField]
        private GameObject attackSpeedButtonGameObject;
        private float attackSpeedCost = 100;

        [SerializeField]
        private TMP_Text healthCostText;
        [SerializeField]
        private GameObject healthButtonGameObject;
        private float healthCost = 100;

        [SerializeField]
        private TMP_Text attackRangeCostText;
        [SerializeField]
        private GameObject attackRangeButtonGameObject;
        private float attackRangeCost = 100;

        private void OnEnable()
        {
            CheckBoughtUpgrades();
        }

        private void CheckBoughtUpgrades()
        {
            float currentMoney = UpgradesManager.Upgrades.Coins;
            if (ArcherTurretUpgrades == null) return;
            UpdateMoneyText();

            CheckForUpgrade(currentMoney, ArcherTurretUpgrades.AttackSpeedBonusBought, attackSpeedCost, attackSpeedCostText, attackSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, ArcherTurretUpgrades.HealthBonusBought, healthCost, healthCostText, healthButtonGameObject);
            CheckForUpgrade(currentMoney, ArcherTurretUpgrades.AttackRangeBonusBought, attackRangeCost, attackRangeCostText, attackRangeButtonGameObject);
        }

        public void BuyAttackSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(attackSpeedCost);
            if (bought)
            {
                ArcherTurretUpgrades.AttackSpeedBonusBought = true;
                UpgradesManager.SendDataToAnalytics("ArcherTurret-attackSpeed");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyHealthBonus()
        {
            bool bought = UpgradesManager.Buy(healthCost);
            if (bought)
            {
                ArcherTurretUpgrades.HealthBonusBought = true;
                UpgradesManager.SaveUpgrades();
                UpgradesManager.SendDataToAnalytics("ArcherTurret-health");

            }
            CheckBoughtUpgrades();
        }

        public void BuyAttackRangeBonus()
        {
            bool bought = UpgradesManager.Buy(attackRangeCost);
            if (bought)
            {
                ArcherTurretUpgrades.AttackRangeBonusBought = true;
            UpgradesManager.SaveUpgrades();
                UpgradesManager.SendDataToAnalytics("ArcherTurret-attackRange");

            }
            CheckBoughtUpgrades();
        }
    }
}

using Project.Assets.Scripts.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Project.Upgrades.UI
{
    public class SwordsmanUpgradesUI : UpgradesUI
    {
        private SwordsmanUpgrades swordsmanUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.SwordsmanUpgrades;
            }
        }

        [SerializeField] private TMP_Text movementSpeedCostText;
        [SerializeField] private GameObject movementSpeedButtonGameObject;
        private float movementSpeedCost = 100;

        [SerializeField] private TMP_Text attackSpeedCostText;
        [SerializeField] private GameObject attackSpeedButtonGameObject;
        private float attackSpeedCost = 100;

        [SerializeField] private TMP_Text attackDamageCostText;
        [SerializeField] private GameObject attackDamageButtonGameObject;
        private float attackDamageCost = 100;

        [SerializeField] private TMP_Text healthCostText;
        [SerializeField] private GameObject healthButtonGameObject;
        private float healthCost = 100;

        private void OnEnable()
        {
            CheckBoughtUpgrades();
        }

        private void CheckBoughtUpgrades()
        {
            float currentMoney = UpgradesManager.Upgrades.Money;

            CheckForUpgrade(currentMoney, swordsmanUpgrades.MovementSpeedBonusBought, movementSpeedCost, movementSpeedCostText, movementSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, swordsmanUpgrades.AttackSpeedBonusBought, attackSpeedCost, attackSpeedCostText, attackSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, swordsmanUpgrades.AttackDamageBonusBought, attackDamageCost, attackDamageCostText, attackDamageButtonGameObject);
            CheckForUpgrade(currentMoney, swordsmanUpgrades.HealthBonusBought, healthCost, healthCostText, healthButtonGameObject);
        }

        public void BuyMovementSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(movementSpeedCost);
            if (bought)
            {
                swordsmanUpgrades.MovementSpeedBonusBought = true;
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyAttackSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(attackSpeedCost);
            if (bought)
            {
                swordsmanUpgrades.AttackSpeedBonusBought = true;
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyAttackDamageBonus()
        {
            bool bought = UpgradesManager.Buy(attackDamageCost);
            if (bought)
            {
                swordsmanUpgrades.AttackDamageBonusBought = true;
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyHealthBonus()
        {
            bool bought = UpgradesManager.Buy(healthCost);
            if (bought)
            {
                swordsmanUpgrades.HealthBonusBought = true;
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }
    }
}

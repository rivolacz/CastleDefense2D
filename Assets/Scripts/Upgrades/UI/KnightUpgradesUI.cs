using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Project.Upgrades.UI
{
    public class KnightUpgradesUI : UpgradesUI
    {
        KnightUpgrades KnightUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.KnightUpgrades;
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

        [SerializeField] private GameObject unitUpgradesLockGameObject;

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
            float currentMoney = UpgradesManager.Upgrades.Money;
            if (KnightUpgrades == null) return;
            if (KnightUpgrades.KnightResearched)
            {
                unitUpgradesLockGameObject.SetActive(false);
            }
            else
            {
                unitUpgradesLockGameObject.SetActive(true);
            }
            UpdateMoneyText();
            CheckForUpgrade(currentMoney, KnightUpgrades.AttackDamageBonusBought, attackDamageCost, attackDamageCostText, attackDamageButtonGameObject);
            CheckForUpgrade(currentMoney, KnightUpgrades.MovementSpeedBonusBought, movementSpeedCost, movementSpeedCostText, movementSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, KnightUpgrades.AttackSpeedBonusBought, attackSpeedCost, attackSpeedCostText, attackSpeedButtonGameObject);
        }

        public void BuyMovementSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(movementSpeedCost);
            if (bought)
            {
                KnightUpgrades.MovementSpeedBonusBought = true;
            }
            UpgradesManager.SaveUpgrades();
            CheckBoughtUpgrades();
        }

        public void BuyAttackSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(attackSpeedCost);
            if (bought)
            {
                KnightUpgrades.AttackSpeedBonusBought = true;
            }
            UpgradesManager.SaveUpgrades();
            CheckBoughtUpgrades();
        }

        public void BuyAttackDamageBonus()
        {
            bool bought = UpgradesManager.Buy(attackDamageCost);
            if (bought)
            {
                KnightUpgrades.AttackDamageBonusBought = true;
            }
            UpgradesManager.SaveUpgrades();
            CheckBoughtUpgrades();
        }
    }
}

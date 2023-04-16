using System;
using TMPro;
using UnityEngine;

namespace Project.Upgrades.UI
{
    public class CashBonusAbilityUpgradesUI : UpgradesUI
    {
        CashBonusAbilityUpgrades CashBonusAbilityUpgrades
        {
            get{
                return UpgradesManager.Upgrades.CashBonusAbilityUpgrades;
            }
        }

        [SerializeField]
        private TMP_Text cashBonusCostText;
        [SerializeField]
        private GameObject cashBonusButtonGameObject;
        private float cashBonusCost = 100;

        [SerializeField]
        private TMP_Text cashBonus2CostText;
        [SerializeField]
        private GameObject cashBonus2ButtonGameObject;
        private float cashBonus2Cost = 100;

        [SerializeField]
        private TMP_Text effectLengthCostText;
        [SerializeField]
        private GameObject effectLengthButtonGameObject;
        private float effectLengthCost = 100;

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
            if (CashBonusAbilityUpgrades == null) return;
            UpdateMoneyText();

            CheckForUpgrade(currentMoney, CashBonusAbilityUpgrades.CashBonusBought, cashBonusCost, cashBonusCostText, cashBonusButtonGameObject);
            CheckForUpgrade(currentMoney, CashBonusAbilityUpgrades.CashBonus2Bought, cashBonus2Cost, cashBonus2CostText, cashBonus2ButtonGameObject);
            CheckForUpgrade(currentMoney, CashBonusAbilityUpgrades.EffectLengthBonusBought, effectLengthCost, effectLengthCostText, effectLengthButtonGameObject);
        }

        public void BuyCashBonus()
        {
            bool bought = UpgradesManager.Buy(cashBonusCost);
            if (bought)
            {
                CashBonusAbilityUpgrades.CashBonusBought = true;
                UpgradesManager.SendDataToAnalytics("CashBonus-cashBonus");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyCashBonus2()
        {
            bool bought = UpgradesManager.Buy(cashBonus2Cost);
            if (bought)
            {
                CashBonusAbilityUpgrades.CashBonus2Bought = true;
                UpgradesManager.SendDataToAnalytics("CashBonus-cashBonus2");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyEffectLengthBonus()
        {
            bool bought = UpgradesManager.Buy(effectLengthCost);
            if (bought)
            {
                CashBonusAbilityUpgrades.EffectLengthBonusBought = true;
                UpgradesManager.SendDataToAnalytics("CashBonus-effectLength");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }
    }
}

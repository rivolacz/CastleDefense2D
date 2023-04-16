using Project.Localization;
using Project.Upgrades.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Upgrades
{
    public class CastleUpgradesUI : UpgradesUI
    {
        CastleUpgrades CastleUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.CastleUpgrades;
            }
        }

        [SerializeField]
        private TMP_Text bonusHealthCostText;
        [SerializeField]
        private GameObject bonusHealthButtonGameObject;
        private float bonusHealthCost = 100;
        [SerializeField]
        private TMP_Text shootingArrowsCostText;
        [SerializeField]
        private GameObject shootingArrowsButtonGameObject;
        private float shootingArrowsCost = 100;
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
            if(CastleUpgrades == null)return;
            UpdateMoneyText();

            CheckForUpgrade(currentMoney, CastleUpgrades.BonusHealthBought, bonusHealthCost, bonusHealthCostText, bonusHealthButtonGameObject);
            CheckForUpgrade(currentMoney, CastleUpgrades.CastleCanShootArrowsBought, shootingArrowsCost, shootingArrowsCostText, shootingArrowsButtonGameObject);
        }

        public void BuyBonusHealth()
        {
            bool bought = UpgradesManager.Buy(bonusHealthCost);
            if (bought)
            {
                CastleUpgrades.BonusHealthBought = true;
                UpgradesManager.SendDataToAnalytics("Castle-health");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyShootingArrows()
        {
            bool bought = UpgradesManager.Buy(shootingArrowsCost);
            if (bought)
            {
                CastleUpgrades.CastleCanShootArrows = true;
                UpgradesManager.SendDataToAnalytics("Castle-shootingArrows");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }
    }
}

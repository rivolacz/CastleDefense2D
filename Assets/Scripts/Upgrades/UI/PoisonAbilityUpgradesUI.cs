using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Project.Upgrades.UI
{
    public class PoisonAbilityUpgradesUI : UpgradesUI
    {
        PoisonAbilityUpgrades PoisonAbilityUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.PoisonAbilityUpgrades;
            }
        }

        [SerializeField]
        private TMP_Text damagePerSecondCostText;
        [SerializeField]
        private GameObject damagePerSecondButtonGameObject;
        private float damagePerSecondCost = 100;

        [SerializeField]
        private TMP_Text effectDurationCostText;
        [SerializeField]
        private GameObject effectDurationButtonGameObject;
        private float effectDurationCost = 100;

        [SerializeField]
        private TMP_Text effectRangeCostText;
        [SerializeField]
        private GameObject effectRangeButtonGameObject;
        private float effectRangeCost = 100;

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
            if (PoisonAbilityUpgrades == null) return;
            UpdateMoneyText();

            CheckForUpgrade(currentMoney, PoisonAbilityUpgrades.DamagePerSecondBonusBought, damagePerSecondCost, damagePerSecondCostText, damagePerSecondButtonGameObject);
            CheckForUpgrade(currentMoney, PoisonAbilityUpgrades.EffectDurationBonusBought, effectDurationCost, effectDurationCostText, effectDurationButtonGameObject);
            CheckForUpgrade(currentMoney, PoisonAbilityUpgrades.EffectRangeBonusBought, effectRangeCost, effectRangeCostText, effectRangeButtonGameObject);
        }

        public void BuyDamagePerSecond()
        {
            bool bought = UpgradesManager.Buy(damagePerSecondCost);
            if (bought)
            {
                PoisonAbilityUpgrades.DamagePerSecondBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Poison-dps");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyEffectDuration()
        {
            bool bought = UpgradesManager.Buy(effectDurationCost);
            if (bought)
            {
                PoisonAbilityUpgrades.EffectDurationBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Pikeman-effectDuration");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyEffectRange()
        {
            bool bought = UpgradesManager.Buy(effectRangeCost);
            if (bought)
            {
                PoisonAbilityUpgrades.EffectRangeBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Pikeman-effectRange");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }
    }
}

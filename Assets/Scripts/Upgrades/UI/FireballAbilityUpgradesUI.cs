using TMPro;
using UnityEngine;

namespace Project.Upgrades.UI
{
    public class FireballAbilityUpgradesUI : UpgradesUI
    {
        FireballAbilityUpgrades FireballAbilityUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.FireballAbilityUpgrades;
            }
        }

        [SerializeField] private TMP_Text damageBonusCostText;
        [SerializeField] private GameObject damageBonusButtonGameObject;
        private float damageBonusCost = 100f;

        [SerializeField] private TMP_Text manaUsageReductionCostText;
        [SerializeField] private GameObject manaUsageReductionButtonGameObject;
        private float manaUsageReductionCost = 100f;

        [SerializeField] private GameObject instantlyKillAllUnitsButtonGameObject;
        [SerializeField] private TMP_Text instantlyKillAllUnitsCostText;
        private float instantlyKillAllUnitsCost = 1000f;

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
            if (FireballAbilityUpgrades == null) return;
            UpdateMoneyText();

            CheckForUpgrade(currentMoney, FireballAbilityUpgrades.DamageBonusBought, damageBonusCost, damageBonusCostText, damageBonusButtonGameObject);
            CheckForUpgrade(currentMoney, FireballAbilityUpgrades.ManaUsageReductionBought, manaUsageReductionCost, manaUsageReductionCostText, manaUsageReductionButtonGameObject);
            CheckForUpgrade(currentMoney, FireballAbilityUpgrades.InstantlyKillAllUnitsBought, instantlyKillAllUnitsCost, instantlyKillAllUnitsCostText, instantlyKillAllUnitsButtonGameObject);
        }

        public void BuyDamageBonus()
        {
            bool bought = UpgradesManager.Buy(damageBonusCost);
            if (bought)
            {
                FireballAbilityUpgrades.DamageBonusBought = true;
            }
            UpgradesManager.SaveUpgrades();
            CheckBoughtUpgrades();
        }

        public void BuyManaUsageReduction()
        {
            bool bought = UpgradesManager.Buy(manaUsageReductionCost);
            if (bought)
            {
                UpgradesManager.SendDataToAnalytics("Fireball-manaReduction");
                FireballAbilityUpgrades.ManaUsageReductionBought = true;
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyInstantlyKillAllUnits()
        {
            bool bought = UpgradesManager.Buy(instantlyKillAllUnitsCost);
            if (bought)
            {
                UpgradesManager.SendDataToAnalytics("Fireball-killInstantly");
                FireballAbilityUpgrades.InstantlyKillAllUnitsBought = true;
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }
    }
}

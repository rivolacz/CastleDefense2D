using Project.Upgrades;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Upgrades.UI
{
    public class BuilderUpgradesUI : UpgradesUI
    {
        BuilderUpgrades BuilderUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.BuilderUpgrades;
            }
        }

        [SerializeField]
        private TMP_Text movementSpeedCostText;
        [SerializeField]
        private GameObject movementSpeedButtonGameObject;
        private float movementSpeedCost = 100;

        [SerializeField]
        private TMP_Text buildingSpeedCostText;
        [SerializeField]
        private GameObject buildingSpeedButtonGameObject;
        private float buildingSpeedCost = 100;

        [SerializeField]
        private TMP_Text instantBuildingsCostText;
        [SerializeField]
        private GameObject instantBuildingsButtonGameObject;
        private float instantBuildingsCost = 1000;

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
            if (BuilderUpgrades == null) return;
            UpdateMoneyText();
            CheckForUpgrade(currentMoney, BuilderUpgrades.MovementSpeedBonusBought, movementSpeedCost, movementSpeedCostText, movementSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, BuilderUpgrades.BuildingSpeedMultiplierBought, buildingSpeedCost, buildingSpeedCostText, buildingSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, BuilderUpgrades.InstantlyBuildBuildingsBought, instantBuildingsCost, instantBuildingsCostText, instantBuildingsButtonGameObject);
        }

        public void BuyMovementSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(movementSpeedCost);
            if (bought)
            {
                UpgradesManager.Upgrades.BuilderUpgrades.MovementSpeedBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Builder-movementSpeed");

                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyBuildingSpeedMultiplier()
        {
            bool bought = UpgradesManager.Buy(buildingSpeedCost);
            if (bought)
            {
                UpgradesManager.Upgrades.BuilderUpgrades.BuildingSpeedMultiplierBought = true;
                UpgradesManager.SendDataToAnalytics("Builder-buildingSpeed");

                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyInstantlyBuildBuildings()
        {
            bool bought = UpgradesManager.Buy(instantBuildingsCost);
            if (bought)
            {
                UpgradesManager.Upgrades.BuilderUpgrades.InstantlyBuildBuildingsBought = true;
                UpgradesManager.SendDataToAnalytics("Builder-instantlyBuild");

                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }
    }
}

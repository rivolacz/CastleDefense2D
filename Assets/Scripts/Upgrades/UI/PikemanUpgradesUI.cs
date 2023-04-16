using TMPro;
using UnityEngine;

namespace Project.Upgrades.UI
{
    public class PikemanUpgradesUI : UpgradesUI
    {
        PikemanUpgrades PikemanUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.PikemanUpgrades;
            }
        }

        [SerializeField]
        private TMP_Text attackSpeedCostText;
        [SerializeField]
        private GameObject attackSpeedButtonGameObject;
        private float attackSpeedCost = 100;
        [SerializeField]
        private TMP_Text movementSpeedCostText;
        [SerializeField]
        private GameObject movementSpeedButtonGameObject;
        private float movementSpeedCost = 100;

        [SerializeField]
        private TMP_Text attackDamageCostText;
        [SerializeField]
        private GameObject attackDamageButtonGameObject;
        private float attackDamageCost = 100;
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
            if (PikemanUpgrades == null) return;
            UpdateMoneyText();
            CheckForUpgrade(currentMoney, PikemanUpgrades.AttackDamageBonusBought, attackDamageCost, attackDamageCostText, attackDamageButtonGameObject);
            CheckForUpgrade(currentMoney, PikemanUpgrades.MovementSpeedBonusBought, movementSpeedCost, movementSpeedCostText, movementSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, PikemanUpgrades.AttackSpeedBonusBought, attackSpeedCost, attackSpeedCostText, attackSpeedButtonGameObject);
        }

        public void BuyAttackDamageBonus()
        {
            bool bought = UpgradesManager.Buy(attackDamageCost);
            if (bought)
            {
                PikemanUpgrades.AttackDamageBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Pikeman-attackDamage");
                UpgradesManager.SaveUpgrades();
                CheckBoughtUpgrades();
            }
        }

        public void BuyMovementSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(movementSpeedCost);
            if (bought)
            {
                PikemanUpgrades.MovementSpeedBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Pikeman-movementSpeed");
                UpgradesManager.SaveUpgrades();
                CheckBoughtUpgrades();
            }
        }

        public void BuyAttackSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(attackSpeedCost);
            if (bought)
            {
                PikemanUpgrades.AttackSpeedBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Pikeman-attackSpeed");
                UpgradesManager.SaveUpgrades();
                CheckBoughtUpgrades();
            }
        }
    }
}

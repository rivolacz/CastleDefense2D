using Project.Upgrades;
using System;
using System.Collections;
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

        [SerializeField] private TMP_Text researchCostText;
        [SerializeField] private GameObject researchButtonGameObject;
        private float researchCost = 100;
        [SerializeField] private ProgressBar researchBar;
        [SerializeField] private GameObject unitUpgradesLockGameObject;

        private TimeSpan researchTime = new TimeSpan(0, 5, 0);
        private DateTime startResearchTime;
        private bool isResearching = false;

        private void OnEnable()
        {
            CheckBoughtUpgrades();
            if (!swordsmanUpgrades.Researched)
            {
                if (swordsmanUpgrades.ResearchStartTime != DateTime.MinValue)
                {
                    startResearchTime = swordsmanUpgrades.ResearchStartTime;
                    isResearching = true;
                    researchButtonGameObject.SetActive(false);
                    unitUpgradesLockGameObject.SetActive(true);
                    StartCoroutine(UpdateTimer());

                }
                else
                {
                    researchCostText.text = researchCost.ToString();
                    unitUpgradesLockGameObject.SetActive(true);
                }
            }
        }

        private void CheckBoughtUpgrades()
        {
            float currentMoney = UpgradesManager.Upgrades.Coins;

            CheckForUpgrade(currentMoney, swordsmanUpgrades.MovementSpeedBonusBought, movementSpeedCost, movementSpeedCostText, movementSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, swordsmanUpgrades.AttackSpeedBonusBought, attackSpeedCost, attackSpeedCostText, attackSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, swordsmanUpgrades.AttackDamageBonusBought, attackDamageCost, attackDamageCostText, attackDamageButtonGameObject);
            CheckForUpgrade(currentMoney, swordsmanUpgrades.HealthBonusBought, healthCost, healthCostText, healthButtonGameObject);
            CheckForUpgrade(currentMoney, swordsmanUpgrades.Researched, researchCost, researchCostText, researchButtonGameObject);

        }

        public void BuyMovementSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(movementSpeedCost);
            if (bought)
            {
                swordsmanUpgrades.MovementSpeedBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Swordsman-movementSpeed");
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
                UpgradesManager.SendDataToAnalytics("Swordsman-attackSpeed");
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
                UpgradesManager.SendDataToAnalytics("Swordsman-attackDamage");
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
                UpgradesManager.SendDataToAnalytics("Swordsman-health");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void StartResearch()
        {
            bool bought = UpgradesManager.Buy(researchCost);
            if (bought)
            {
                swordsmanUpgrades.ResearchStartTime = DateTime.Now;
                startResearchTime = DateTime.Now;
                UpgradesManager.SaveUpgrades();
                isResearching = true;
                UpgradesManager.SendDataToAnalytics("Swordsman-research");
                researchButtonGameObject.SetActive(false);
                StartCoroutine(UpdateTimer());
            }
        }

        public IEnumerator UpdateTimer()
        {
            Debug.Log("Timer");
            researchBar.gameObject.SetActive(true);
            while (isResearching)
            {
                double timePassed = (DateTime.Now - startResearchTime).TotalSeconds;
                double remainingTime = researchTime.TotalSeconds - timePassed;
                Debug.Log(remainingTime);
                if (remainingTime <= 0)
                {
                    isResearching = false;
                    swordsmanUpgrades.Researched = true;
                    unitUpgradesLockGameObject.SetActive(false);
                }
                else
                {
                    float percentage = (float)(timePassed / researchTime.TotalSeconds);
                    researchBar.FillProgressBar(percentage);
                }
                yield return new WaitForSeconds(1);
            }
        }
    }
}

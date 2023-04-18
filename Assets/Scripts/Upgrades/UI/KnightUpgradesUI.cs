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

        [SerializeField] private TMP_Text researchCostText;
        [SerializeField] private GameObject researchButtonGameObject;
        private float researchCost = 250;
        [SerializeField] private ProgressBar researchBar;
        [SerializeField] private GameObject unitUpgradesLockGameObject;

        private TimeSpan researchTime = new TimeSpan(0, 5,0);
        private DateTime startResearchTime;
        private bool isResearching = false;
        private void OnEnable()
        {
            CheckBoughtUpgrades();
            if (!KnightUpgrades.KnightResearched)
            {
                if (KnightUpgrades.KnightResearchStartTime != DateTime.MinValue)
                {
                    startResearchTime = KnightUpgrades.KnightResearchStartTime;
                    isResearching = true;
                    researchButtonGameObject.SetActive(false);
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
            if (KnightUpgrades == null) return;
            UpdateMoneyText();
            CheckForUpgrade(currentMoney, KnightUpgrades.AttackDamageBonusBought, attackDamageCost, attackDamageCostText, attackDamageButtonGameObject);
            CheckForUpgrade(currentMoney, KnightUpgrades.MovementSpeedBonusBought, movementSpeedCost, movementSpeedCostText, movementSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, KnightUpgrades.AttackSpeedBonusBought, attackSpeedCost, attackSpeedCostText, attackSpeedButtonGameObject);
            CheckForUpgrade(currentMoney, KnightUpgrades.KnightResearched, researchCost, researchCostText, researchButtonGameObject);
        }

        public void BuyMovementSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(movementSpeedCost);
            if (bought)
            {
                KnightUpgrades.MovementSpeedBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Knight-movementSpeed");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyAttackSpeedBonus()
        {
            bool bought = UpgradesManager.Buy(attackSpeedCost);
            if (bought)
            {
                KnightUpgrades.AttackSpeedBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Knight-attackSpeed");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void BuyAttackDamageBonus()
        {
            bool bought = UpgradesManager.Buy(attackDamageCost);
            if (bought)
            {
                KnightUpgrades.AttackDamageBonusBought = true;
                UpgradesManager.SendDataToAnalytics("Knight-damage");
                UpgradesManager.SaveUpgrades();
            }
            CheckBoughtUpgrades();
        }

        public void StartResearch()
        {
            bool bought = UpgradesManager.Buy(researchCost);
            if (bought)
            {
                KnightUpgrades.KnightResearchStartTime = DateTime.Now;
                startResearchTime = DateTime.Now;
                UpgradesManager.SaveUpgrades();
                isResearching = true;
                UpgradesManager.SendDataToAnalytics("Knight-researched");
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
                if (remainingTime <= 0)
                {
                    isResearching = false;
                    KnightUpgrades.KnightResearched = true;
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

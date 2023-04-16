using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project
{
    public class MoneyText : MonoBehaviour
    {
        [SerializeField]
        TMP_Text moneyText;

        private void OnEnable()
        {
            UpdateMoneyText();
        }

        public void UpdateMoneyText(float money)
        {
            if (moneyText != null)
            {
                moneyText.text = money.ToString();
            }
        }
        public void UpdateMoneyText()
        {
            if (moneyText != null)
            {
                moneyText.text = UpgradesManager.Upgrades.Coins.ToString();
            }
        }
    }
}

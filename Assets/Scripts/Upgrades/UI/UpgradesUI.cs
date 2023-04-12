using Project.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Upgrades.UI
{
    public abstract class UpgradesUI : MonoBehaviour
    {
        public void CheckForUpgrade(float playerMoney, bool upgradeBought, float upgradeCost, TMP_Text costText, GameObject button)
        {
            if (upgradeBought)
            {
                if (costText != null)
                {
                    costText.text = string.Empty;
                }
                if (button != null)
                {
                    button.SetActive(false);
                }
            }
            else
            {
                if (costText != null)
                {
                    costText.text = upgradeCost.ToString();
                }
                if (button == null) {
                    return;
                }
                if(playerMoney >= upgradeCost)
                {
                    button.GetComponent<Image>().color = Color.white;
                    button.GetComponent<Button>().interactable = true;
                }
                else
                {
                    button.GetComponent<Image>().color = Color.red;
                    button.GetComponent<Button>().interactable = false;
                }
            }
        }

        public void UpdateMoneyText()
        {
            GetComponentInParent<MoneyText>().UpdateMoneyText();
        }
    }
}

using Project.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Abilities
{
    [System.Serializable]
    public class CashBonusAbility : Ability
    {
        [SerializeField]
        private TMP_Text cashBonusText;
        [SerializeField]
        private ProgressBar cashBonusProgress;
        [SerializeField]
        private Image cashBonusImage;
        private float castCashBonus = 2;
        private float cashEffectBaseLength = 10;
        private float cashEffectLength = 10;
        private float ManaCost = 125;
        private bool canCast = true;
        private CashBonusAbilityUpgrades CashBonusAbilityUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.CashBonusAbilityUpgrades;
            }
        }

        public bool CanCast()
        {
            return canCast;
        }

        public override void Cast()
        {
            Debug.Log("CASTING");
            cashEffectLength = GetEffectLength();
            castCashBonus = CalculateBonus();
            GameData.CashMultiplierFromAbility = castCashBonus;
            SetUIActiveState(true);
            cashBonusText.text = $"{castCashBonus.ToString("0.0")}x";
            canCast = false;
        }

        private float CalculateBonus()
        {
            castCashBonus = 2;
            if (CashBonusAbilityUpgrades.CashBonusBought)
            {
                castCashBonus += CashBonusAbilityUpgrades.CashBonus;
            }
            if (CashBonusAbilityUpgrades.CashBonus2Bought)
            {
                castCashBonus += CashBonusAbilityUpgrades.CashBonus2;
            }
            return castCashBonus;
        }

        public override float GetManaCost()
        {
            return ManaCost;
        }

        public override void SetTarget(Vector2 target)
        {
        }

        public float GetEffectLength()
        {
            float bonusFromUpgrades = 0;
            if (CashBonusAbilityUpgrades.EffectLengthBonusBought)
            {
                bonusFromUpgrades = CashBonusAbilityUpgrades.EffectLengthBonus;
            }
            return cashEffectBaseLength + bonusFromUpgrades;
        }

        public void UpdateEffectProgress(float time)
        {
            float percentage = time / cashEffectLength;
            float inverted = Math.Abs(percentage - 1);
            cashBonusProgress.FillProgressBar(inverted);
        }

        public void CancelEffect()
        {
            GameData.CashMultiplierFromAbility = 1;
            SetUIActiveState(false);
            canCast = true;
        }

        private void SetUIActiveState(bool active)
        {
            cashBonusProgress.enabled = active;
            cashBonusProgress.gameObject.SetActive(active);
            cashBonusText.enabled = active;
            cashBonusImage.enabled = active;
        }
    }
}

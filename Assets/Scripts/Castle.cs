using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(ArcherTurret))]
    public class Castle : MonoBehaviour, IDamageable
    {
        private CastleUpgrades CastleUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.CastleUpgrades;
            }
        }

        [SerializeField]
        private ProgressBar healthBar;
        [SerializeField]
        private TMP_Text healthText;
        [SerializeField]
        private LerpTextValue coinGainText;
        [SerializeField]
        private TMP_Text daysPassedText;
        [SerializeField]
        private Canvas endGameCanvas;
        private float maxHealth = 1000;
        private float currentHealth = 1000;
        private float baseCoinReward = 15;

        private void Awake()
        {
            if (CastleUpgrades.BonusHealthBought)
            {
                maxHealth += CastleUpgrades.BonusHealth;
            }
            if (CastleUpgrades.CastleCanShootArrows)
            {
                GetComponent<ArcherTurret>().enabled = true;
            }
            currentHealth = maxHealth;
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
            float healthPercentage = currentHealth / maxHealth;
            healthBar.FillProgressBar(healthPercentage);
            healthBar.gameObject.SetActive(true);
            healthText.text = $"{currentHealth:0}/{maxHealth:0}";
            if (healthPercentage <= 0.2f)
            {
                healthBar.ChangeColor(Color.red);
            }
            else
            {
                healthBar.ChangeColor(Color.Lerp(Color.red, Color.green, healthPercentage));
            }
            if (currentHealth <= 0)
            {
                endGameCanvas.enabled = true;
                float coinBonusFromWaves = Enumerable.Range(1, GameData.CurrentWave).Sum() / 2;
                float coinBonus = baseCoinReward + coinBonusFromWaves;
                coinGainText.SetTargetMoney(coinBonus);
                daysPassedText.text = GameData.CurrentWave.ToString();
                Time.timeScale = 0;
                UpgradesManager.Upgrades.Coins += coinBonus;
                UpgradesManager.Upgrades.Retries += 1;
                UpgradesManager.SaveUpgrades();
                Destroy(gameObject);
            }
        }
    }
}

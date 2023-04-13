using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        public Canvas castleCanvas;
        [SerializeField]
        private ProgressBar healthBar;
        [SerializeField]
        private TMP_Text healthText;
        private float maxHealth = 1000;
        private float currentHealth = 1000;

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
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
            Debug.Log("Castle took hit");
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
                Destroy(gameObject);
            }
        }
    }
}

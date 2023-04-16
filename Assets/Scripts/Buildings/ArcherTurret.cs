using Project.Units;
using Project.Upgrades;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project
{
    public class ArcherTurret : MonoBehaviour , IDamageable
    {
        ArcherTurretUpgrades ArcherTurretUpgrades;
        public float Range;
        [SerializeField]
        private float AttackRate;
        [SerializeField]
        private float damage;
        [SerializeField]
        private GameObject ArrowPrefab;
        [SerializeField]
        private LayerMask enemyLayerMask;
        [SerializeField]
        private ProgressBar healthBar;

        private float AttackSpeedReduction = 0;
        private float AttackRangeBonus = 0;
        private float HealthBonus = 0;
        private float MaxHealth = 250;
        private float CurrentHealth = 250;
        public void Attack(Transform target)
        {
            GameObject arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
   
            arrow.GetComponent<Projectile>().SetTargetAndDamage(target, damage);
        }

        private void OnEnable()
        {
            ArcherTurretUpgrades = UpgradesManager.Upgrades.ArcherTurretUpgrades;
            GetUpgradeValues();
            StartCoroutine(AttackCoroutine());
        }

        private void GetUpgradeValues()
        {
            if (ArcherTurretUpgrades.AttackSpeedBonusBought)
            {
                AttackSpeedReduction = ArcherTurretUpgrades.AttackSpeedBonus;
            }
            if (ArcherTurretUpgrades.HealthBonusBought)
            {
                HealthBonus = ArcherTurretUpgrades.HealthBonus;
            }
            if (ArcherTurretUpgrades.AttackRangeBonusBought)
            {
                AttackRangeBonus = ArcherTurretUpgrades.AttackRangeBonus;
            }
            MaxHealth = MaxHealth + HealthBonus;
            CurrentHealth = MaxHealth;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private IEnumerator AttackCoroutine()
        {
            var enemies = Physics2D.OverlapCircleAll(transform.position, Range + AttackRangeBonus, enemyLayerMask);
            if (enemies.Length > 0)
            {
                Transform target = enemies.OrderBy(collider => Vector3.Distance(collider.transform.position, transform.position)).First().transform;
                Attack(target);
            }
            yield return new WaitForSeconds(AttackRate - AttackSpeedReduction);
            StartCoroutine(AttackCoroutine()); 
        }

        public void Damage(float damage)
        {
            CurrentHealth -= damage;
            if (healthBar != null)
            {
                healthBar.gameObject.SetActive(true);
                healthBar.FillProgressBar(CurrentHealth / MaxHealth);
            }
            if (CurrentHealth < 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}

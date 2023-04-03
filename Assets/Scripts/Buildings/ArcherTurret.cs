using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
    public class ArcherTurret : MonoBehaviour
    {
        [SerializeField]
        private float AttackRate;
        [SerializeField]
        private float Damage;
        [SerializeField]
        private GameObject ArrowPrefab;
        [SerializeField]
        private LayerMask enemyLayerMask;
        [SerializeField]
        private float Range;

        public void Attack(Transform target)
        {
            GameObject arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Projectile>().SetTargetAndDamage(target, Damage);
        }

        private void OnEnable()
        {
            StartCoroutine(AttackCoroutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private IEnumerator AttackCoroutine()
        {
            var enemies = Physics2D.OverlapCircleAll(transform.position, Range, enemyLayerMask);
            if (enemies.Length > 0)
            {
                Transform target = enemies.First().transform;
                Attack(target);
            }
            yield return new WaitForSeconds(AttackRate);
            StartCoroutine(AttackCoroutine()); 
        }
    }
}

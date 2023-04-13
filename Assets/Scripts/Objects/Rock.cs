using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Rock : MonoBehaviour
    {
        private float speed = 15f;
        private float damage;
        private Transform target;

        private void Start()
        {
            if (target == null) return;
            Vector3 direction = (target.position - transform.position).normalized;
        }

        private void Update()
        {
            if (target == null) return;
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        public void SetTargetAndDamage(Transform newTarget, float damage)
        {
            target = newTarget;
            this.damage = damage;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger " + collision.name);
            if (collision == null) return;
            var damageble = collision.GetComponent<IDamageable>();
            if (damageble == null)
            {
                Debug.Log("Damagable is null" + collision.GetType());
                return;
            }
            damageble.Damage(damage);
            Debug.Log(collision.transform.name, collision.gameObject);
            Destroy(gameObject);
        }
    }
}

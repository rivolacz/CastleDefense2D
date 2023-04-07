using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 10f;
        public float damage;
        public Transform target;

        private void Start()
        {
            if (target == null) return;
            Vector3 direction = (target.position - transform.position).normalized;
            SetRotationFromDirection(direction);
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
            if (collision == null) return;
            var damageble = collision.GetComponent<IDamageable>();
            if (damageble == null)
            {
                Debug.Log("Damagable is null" + collision.GetType());
                return;
            }
            damageble.Damage(damage);
            Debug.Log(collision.transform.name, collision.gameObject);
            transform.SetParent(collision.transform);
            transform.localPosition = Vector3.zero;
            target = null;
            GetComponent<Collider2D>().enabled = false;
        }

        public void SetRotationFromDirection(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, angle);
        }
    }
}

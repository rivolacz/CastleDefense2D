using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class PoisonArea : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask;
        private Vector2 target;
        private float range;
        private float damagePerSecond;


        private void Update()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(target, range, layerMask);
            foreach (Collider2D collider in colliders)
            {
                if (!collider.TryGetComponent<IDamageable>(out var damageable)) return;
                damageable.Damage(damagePerSecond * Time.deltaTime);
            }
        }

        public void SetData(Vector2 target, float range, float damagePerSecond, float effectDuration)
        {
            this.target = target;
            this.range = range;
            this.damagePerSecond = damagePerSecond;
            transform.position = target;
            transform.localScale = new Vector3(range, range, 1);
            Destroy(gameObject, effectDuration);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class AttackUnit : Units.Unit
    {
        public virtual void Attack()
        {
            var allColliders = new List<Collider2D>();
            weaponCollider.Overlap(contactFilter2D, allColliders);
            foreach (Collider2D collider in allColliders)
            {
                IDamageable damagable = collider.GetComponent<IDamageable>();
                damagable?.Damage(unitStats.AttackDamage);
            }
        }
    }
}

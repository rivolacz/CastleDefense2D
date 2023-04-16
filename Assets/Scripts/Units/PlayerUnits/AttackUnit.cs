using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class AttackUnit : Units.Unit
    {
        [SerializeField]
        protected ContactFilter2D contactFilter2D;
        [SerializeField]
        protected BoxCollider2D weaponCollider;
        protected float damageBonus = 0;
        public virtual void Attack()
        {
            Debug.Log("Attacking");
            var allColliders = new List<Collider2D>();
            weaponCollider.Overlap(contactFilter2D, allColliders);
            foreach (Collider2D collider in allColliders)
            {
                Debug.Log("Hitting");
                IDamageable damagable = collider.GetComponent<IDamageable>();
                damagable?.Damage(unitStats.AttackDamage + damageBonus);
            }
        }

        public void SetAttackSpeedBonus(float bonus)
        {
            float speed = 1 + bonus;
            StateMachine.unitAnimatorValuesSetter.SetAnimatorSpeed(speed);
        }
    }
}

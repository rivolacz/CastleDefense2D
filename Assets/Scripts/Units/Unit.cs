using Project.StateMachines;
using Project.StateMachines.States;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Globalization;

namespace Project.Units
{
    public class Unit : MonoBehaviour, IDamageable
    {
        public UnitStats unitStats;
        [SerializeField]
        protected ContactFilter2D contactFilter2D;
        [SerializeField]
        protected BoxCollider2D weaponCollider;


        public float currentHealth = 100;
        protected Vector2Int positionOnGrid = Vector2Int.zero;
        public StateMachine StateMachine;

        private PlacementAroundTarget placementAroundTarget = null;
        private Transform placementPosition = null;

        private void Awake()
        {
            StateMachine = GetComponent<StateMachine>();
        }

        public void ChangeState(BaseState newState)
        {
            StateMachine.ChangeState(newState);
        }

        public virtual void Attack()
        {
            var allColliders = new List<Collider2D>();
            weaponCollider.Overlap(contactFilter2D, allColliders);
            foreach(Collider2D collider in allColliders)
            {
                Debug.Log($"{collider.gameObject.name} is in {collider.gameObject.layer}");
                IDamageable damagable =  collider.GetComponent<IDamageable>();
                damagable?.Damage(unitStats.AttackDamage);
            }
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"{transform.name} took {damage} damage and has now {currentHealth}HP");
            if (currentHealth < 0) {
                Die();
            }
        }

        public void AssignPlacementToTarget(PlacementAroundTarget placementAroundTarget, Transform position)
        {
            this.placementAroundTarget = placementAroundTarget;
            placementPosition = position;
            Debug.Log($"Assigned position from {transform.position} to target" + position.position);
            Castle castle = FindAnyObjectByType<Castle>();
            StateMachine.ChangeState(new MoveState(placementPosition.position, StateMachine, castle.transform));
        }

        private void Die()
        {
            if (placementAroundTarget != null) {
                placementAroundTarget.RemoveFromTaken(placementPosition);
                placementAroundTarget = null;
                placementPosition = null;
            }
            Destroy(gameObject);          
        }
    }
}

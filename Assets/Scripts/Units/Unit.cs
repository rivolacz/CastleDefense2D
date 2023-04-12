using Project.StateMachines;
using Project.StateMachines.States;
using UnityEngine;

namespace Project.Units
{
    public class Unit : MonoBehaviour, IDamageable
    {
        public UnitStats unitStats;
        public float currentHealth = 100;
        protected Vector2Int positionOnGrid = Vector2Int.zero;
        public StateMachine StateMachine;

        [SerializeField]
        private ProgressBar healthBar;

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

        public virtual void Damage(float damage)
        {
            currentHealth -= damage;
            if (healthBar != null)
            {
                healthBar.gameObject.SetActive(true);
                healthBar.FillProgressBar(currentHealth / unitStats.MaxHealth);
            }
            Debug.Log($"{transform.name} took {damage} damage and has now {currentHealth}HP");
            if (currentHealth < 0) {
                Die();
            }
        }

        public void AssignPlacementToTarget(PlacementAroundTarget placementAroundTarget, Transform position)
        {
            this.placementAroundTarget = placementAroundTarget;
            placementPosition = position;
            //Debug.Log($"Assigned position from {transform.position} to target" + position.position);
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

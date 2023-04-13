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
        protected ProgressBar healthBar;


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
            if (currentHealth < 0) {
                Die();
            }
        }

        

        public virtual void Die()
        {
            Destroy(gameObject);          
        }
    }
}

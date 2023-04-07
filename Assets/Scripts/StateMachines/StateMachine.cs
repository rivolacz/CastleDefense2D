using Project.StateMachines.States;
using Project.Units;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.StateMachines
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]

    public class StateMachine : MonoBehaviour
    {
        public UnitAnimatorValuesSetter unitAnimatorValuesSetter;
        public UnitStats unitStats;

        public BaseState CurrentState;
        protected Animator animator;
        protected UnitMovement unitMovement;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            unitMovement = GetComponent<UnitMovement>();
            unitAnimatorValuesSetter = new UnitAnimatorValuesSetter(animator);
            CurrentState = new IdleState(this);
        }

        public void FixedUpdate()
        {
            if(CurrentState == null)
            {
                Debug.LogWarning("CURRENT STATE IS NULL");
                return;
            }
            CurrentState.StateUpdate();  
        }

        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            Debug.Log("Changed state to" + CurrentState.GetType());

            CurrentState.Enter();
        }

        public void MoveUnit(Vector2 moveDirection, bool updateLookDirection = true)
        {
            unitMovement.Move(moveDirection.normalized, updateLookDirection);
            if (unitAnimatorValuesSetter != null)
            {
                unitAnimatorValuesSetter?.SetMovementValues(moveDirection);
            }
        }

        public void Look(Vector2 lookDirection)
        {
            unitMovement.Look(lookDirection.normalized);
        }

        public void CanAttack()
        {
            unitAnimatorValuesSetter.AttackChangeBool(true);
        }

        public void CantAttack()
        {
            unitAnimatorValuesSetter.AttackChangeBool(false);
        }

        public void AttackTarget(Transform attackTarget)
        {
            CurrentState = new AttackState(attackTarget, this);
        }

        public void RefreshPath()
        {
            if (CurrentState is not IMovable movable) return;
            movable.RefreshPath();
        }
    }
}

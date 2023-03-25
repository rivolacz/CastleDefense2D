using Project.Units;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Project.StateMachines
{
    public class AttackState : BaseState
    {
        private Transform attackTarget;
        private UnitStats unitStats;

        public AttackState(Transform target, StateMachine stateMachine) : base(stateMachine)
        {
            attackTarget = target;
            this.stateMachine = stateMachine;
            unitStats = stateMachine?.unitStats;
        }

        public override void StateUpdate() 
        {
            if(attackTarget == null) { 
                stateMachine.ChangeState(new IdleState(stateMachine));
                return;
            }
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            Vector2 directionToTarget = GetDirectionToTarget();
            if (directionToTarget.magnitude < unitStats.AttackRange * 2)
            {
                UnitIsCloseToTarget();
                return;
            }
            else
            {
                stateMachine.MoveUnit(directionToTarget);
            }
        }

        private void UnitIsCloseToTarget()
        {
            if (stateMachine == null) return;
            Vector2 directionToCollider = GetDirectionToCollider();
            if (directionToCollider.magnitude < unitStats.AttackRange)
            {
                stateMachine.unitAnimatorValuesSetter.SetAttackTrigger();
                if (directionToCollider.magnitude < unitStats.AttackRange / 2)
                {
                    stateMachine.MoveUnit(Vector2.zero);
                    stateMachine.Look(directionToCollider);
                    return;
                }
            }
            stateMachine.MoveUnit(directionToCollider, true);

        }

        private Vector3 GetClosestPointOnCollider()
        {
            Collider2D collider = attackTarget.GetComponent<Collider2D>();
            Vector2 colliderClosestPoint= collider.ClosestPoint(stateMachine.transform.position);
            Vector3 closestPointOnCollider = collider.bounds.ClosestPoint(stateMachine.transform.position);
            Debug.DrawLine(stateMachine.transform.position, closestPointOnCollider, UnityEngine.Color.green,0.1f);
            return closestPointOnCollider;
        }

        private Vector2 GetDirectionToTarget()
        {
            Vector2 direction =  attackTarget.position - stateMachine.transform.position;
            return direction;
        }

        private Vector2 GetDirectionToCollider()
        {
            Vector3 closestPointOnCollider = GetClosestPointOnCollider();
            Vector2 direction = closestPointOnCollider - stateMachine.transform.position;
            return direction;
        }

    }
}

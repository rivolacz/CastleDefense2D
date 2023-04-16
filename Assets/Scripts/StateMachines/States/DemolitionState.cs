using Project.StateMachines;
using Project.Units;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Project.StateMachines.States
{
    public class DemolitionState : BaseState
    {
        private UnitStats unitStats;
        private Transform castleTransform;
        private LayerMask layerMask;
        private Transform unitTransform;
        private List<Vector3> path;
        private int currentPathIndex = 0;
        private Vector3 movePartTarget;

        public DemolitionState(List<Vector3> path, LayerMask layerMask, StateMachine stateMachine) : base(stateMachine)
        {
            unitStats = stateMachine.unitStats;
            this.layerMask = layerMask;
            castleTransform = GameData.CastleTransform;
            unitTransform = stateMachine.transform;
            this.path = path;
        }

        public override void Enter()
        {
            movePartTarget = path[currentPathIndex];
        }

        public override void StateUpdate()
        {
            if (CanShootAtCastle())
            {
                StartShooting(castleTransform);
                return;
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(unitTransform.position, unitStats.AttackRange, layerMask);
            if(colliders.Length > 0)
            {
                Transform target = colliders.OrderBy(collider => Vector3.Distance(collider.transform.position, unitTransform.position)).First().transform;
                StartShooting(target);
            }
            else
            {
                Move();
            }
        }

        private bool CanShootAtCastle()
        {
            if (castleTransform == null) return false;
            float distance = Vector3.Distance(unitTransform.position, castleTransform.position);
            return distance < unitStats.AttackRange;
        }

        private void StartShooting(Transform target)
        {
            stateMachine.SetTarget(target);
            stateMachine.unitAnimatorValuesSetter.SetAttackTrigger();
            Vector2 direction = target.position - unitTransform.position;
            SetRotationFromDirection(direction);
        }

        private void SetRotationFromDirection(Vector2 direction)
        {
            stateMachine.Rotate(direction);
        }

        private void Move()
        {
            Vector2 directionToTarget = GetDirectionToTarget();
            SetRotationFromDirection(directionToTarget);
            if (directionToTarget.magnitude < 1f)
            {
                if (IsAtTarget())
                {           
                    if (currentPathIndex + 1 >= path.Count) return;
                    currentPathIndex++;
                    movePartTarget = path[currentPathIndex];
                }
            }
            stateMachine.MoveUnit(directionToTarget.normalized);
        }

        private bool IsAtTarget()
        {
            return (movePartTarget - stateMachine.transform.position).magnitude <1f;
        }

        private Vector2 GetDirectionToTarget()
        {
            Vector2 direction = movePartTarget - stateMachine.transform.position;
            return direction;
        }
    }
}

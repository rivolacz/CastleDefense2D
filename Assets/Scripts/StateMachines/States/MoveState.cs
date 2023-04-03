using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

namespace Project.StateMachines.States
{
    public class MoveState : BaseState
    {
        private List<Vector3> wholePath = new List<Vector3>();
        public Vector3 TargetPosition { get; set; }

        private Vector3 movePartTarget;
        private List<Node> pathNodes = new();
        private int currentPathIndex = 0;
        private int currentNodeIndex = 0;
        private Transform unitTransform;
        private Transform attackTarget;
        private BaseState stateAfterMovingToTarget;

        public MoveState(List<Vector3> pathPositions, StateMachine stateMachine) : base(stateMachine)
        {
            wholePath = pathPositions;
            TargetPosition = wholePath.First();
            unitTransform = stateMachine.transform;
        }

        public MoveState(List<Vector3> pathPositions, StateMachine stateMachine, Transform attackTarget) : base(stateMachine)
        {
            wholePath = pathPositions;
            TargetPosition = wholePath.First();
            unitTransform = stateMachine.transform;
            this.attackTarget = attackTarget;
        }

        public MoveState(List<Vector3> pathPositions, StateMachine stateMachine, BaseState stateAfterMovingToTarget) : base(stateMachine)
        {
            wholePath = pathPositions;
            TargetPosition = wholePath.First();
            unitTransform = stateMachine.transform;
            this.stateAfterMovingToTarget = stateAfterMovingToTarget;
        }

        public MoveState(Vector3 pathPosition, StateMachine stateMachine, BaseState stateAfterMovingToTarget) : base(stateMachine)
        {
            movePartTarget = pathPosition;
            wholePath.Add(pathPosition);
            TargetPosition = movePartTarget;
            unitTransform = stateMachine.transform;
            this.stateAfterMovingToTarget = stateAfterMovingToTarget;
        }

        public MoveState(Vector3 movePosition, StateMachine stateMachine, Transform attackTarget) : base(stateMachine)
        {
            wholePath.Clear();
            wholePath.Add(movePosition);
            TargetPosition = movePosition;
            unitTransform = stateMachine.transform;
            this.attackTarget = attackTarget;
        }

        public MoveState(Vector3 movePosition, StateMachine stateMachine) : base(stateMachine)
        {
            wholePath.Clear();
            wholePath.Add(movePosition);
            TargetPosition = movePosition;
            unitTransform = stateMachine.transform;
        }

        public override void Enter()
        {
            if(Vector3.Distance(TargetPosition, unitTransform.position) < 3)
            {
                movePartTarget = TargetPosition;
                return;
            }
            pathNodes = PathFinding.FindPath(unitTransform.position, TargetPosition);
            if (pathNodes == null || pathNodes.Count < 1)
            {
                movePartTarget = TargetPosition;
            }
            else
            {
                movePartTarget = pathNodes[currentNodeIndex].position;
            }
        }

        public override void StateUpdate()
        {
            Vector2 directionToTarget = GetDirectionToTarget();
            if (directionToTarget.magnitude < .5f)
            {
                if (IsAtEndOfPath())
                {
                    if(stateAfterMovingToTarget != null)
                    {
                        Debug.Log(stateAfterMovingToTarget.GetType());
                        stateMachine.ChangeState(stateAfterMovingToTarget);
                    }
                    if (attackTarget != null)
                    {
                        Transform attackTransform = attackTarget.transform;
                        stateMachine.ChangeState(new AttackState(attackTransform, stateMachine));
                    }
                    else
                    {
                        stateMachine.ChangeState(new IdleState(stateMachine));
                    }
                }
                if (IsAtTarget())
                {
                    ChangePathSegment();
                }
                else
                {
                    IncreaseNodeIndex();
                }
            }
            else
            {
                stateMachine.MoveUnit(directionToTarget.normalized);
            }
        }

        private Vector2 GetDirectionToTarget()
        {
            Vector2 direction = movePartTarget - stateMachine.transform.position;
            return direction;
        }

        private bool IsAtTarget()
        {
            if (pathNodes == null) return true;
            return currentNodeIndex + 1 == pathNodes?.Count;
        }

        private bool IsAtEndOfPath()
        {
            Vector3 endPosition = wholePath.Last();
            Vector3 currentPosition = stateMachine.transform.position;
            float distance = (endPosition - currentPosition).magnitude;
            return distance < 3f;
        }

        private void IncreaseNodeIndex()
        {
            currentNodeIndex++;
            if (currentNodeIndex < pathNodes?.Count)
            {
                movePartTarget = pathNodes[currentNodeIndex].position;
            }
        }

        private void ChangePathSegment()
        {
            currentPathIndex++;
            if (currentPathIndex >= wholePath.Count) return;
            TargetPosition = wholePath[currentPathIndex];
            if (currentPathIndex + 1 < wholePath.Count) {
                pathNodes = PathFinding.FindPath(unitTransform.position, TargetPosition);
            }
            else
            {
                movePartTarget = TargetPosition;
                pathNodes.Clear();
            }
            currentNodeIndex = 0;
            if (pathNodes == null || pathNodes.Count == 0) return;
            var node = pathNodes[currentNodeIndex];
            movePartTarget = (Vector3)(node?.position);
        }
    }
}

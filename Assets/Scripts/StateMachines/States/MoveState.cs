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
        private Transform castle;

        public MoveState(List<Vector3> pathPositions, StateMachine stateMachine) : base(stateMachine)
        {
            wholePath = pathPositions;
            TargetPosition = wholePath.First();
            unitTransform = stateMachine.transform;
        }

        public MoveState(List<Vector3> pathPositions, StateMachine stateMachine, Transform castle) : base(stateMachine)
        {
            wholePath = pathPositions;
            TargetPosition = wholePath.First();
            unitTransform = stateMachine.transform;
            this.castle = castle;
        }

        public MoveState(Vector3 movePosition, StateMachine stateMachine, Transform castle) : base(stateMachine)
        {
            wholePath.Clear();
            wholePath.Add(movePosition);
            TargetPosition = wholePath.First();
            unitTransform = stateMachine.transform;
            this.castle = castle;
        }

        public MoveState(Vector3 movePosition, StateMachine stateMachine) : base(stateMachine)
        {
            wholePath.Clear();
            wholePath.Add(movePosition);
            Debug.Log(movePosition.ToString());
            TargetPosition = movePosition;
            unitTransform = stateMachine.transform;
        }

        public override void Enter()
        {
            pathNodes = PathFinding.FindPath(unitTransform.position, TargetPosition);
            Debug.Log("Enter move state" + pathNodes?.Count);
            if (pathNodes == null || pathNodes.Count < 1)
            {
                movePartTarget = TargetPosition;
            }
            else
            {
                movePartTarget = pathNodes[currentNodeIndex].position;
                Debug.Log(movePartTarget+"-----"+pathNodes.Count);
            }
        }

        public override void StateUpdate()
        {
            Vector2 directionToTarget = GetDirectionToTarget();
            if (directionToTarget.magnitude < .5f)
            {
                if (IsAtEndOfPath())
                {
                    if (castle != null)
                    {
                        Transform castleTransform = castle.transform;
                        stateMachine.ChangeState(new AttackState(castleTransform, stateMachine));
                    }
                    else
                    {
                        stateMachine.ChangeState(new IdleState(stateMachine));
                    }
                }
                if (IsAtTarget())
                {
                    Debug.Log("is at target");
                    ChangePathSegment();
                }
                else
                {
                    Debug.Log("Increase node index");

                    IncreaseNodeIndex();
                }
            }
            else
            {
                Debug.Log(directionToTarget.normalized);
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

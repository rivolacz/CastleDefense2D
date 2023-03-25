using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.StateMachines
{
    public class PatrolState : BaseState
    {
        private Vector2 newPosition;
        public PatrolState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            newPosition = GetPatrolingPoint();
        }

        public override void StateUpdate()
        {
            Vector2 currentPosition = stateMachine.transform.position;
            if((currentPosition - newPosition).sqrMagnitude < 0.5f)
            {
                stateMachine.ChangeState(new IdleState(stateMachine));
                return;
            }
            Vector2 directionToPoint = newPosition - currentPosition;
            stateMachine.MoveUnit(directionToPoint);
        }

        private Vector2 GetPatrolingPoint()
        {
            Vector2 patrolingPoint = new Vector2(stateMachine.transform.position.x + Random.Range(-50,50),stateMachine.transform.position.y + Random.Range(-50,50));
            return patrolingPoint;
        }
    }
}

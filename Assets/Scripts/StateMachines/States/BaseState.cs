using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.StateMachines
{
    public abstract class BaseState
    {
        protected StateMachine stateMachine;

        public BaseState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void StateUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}

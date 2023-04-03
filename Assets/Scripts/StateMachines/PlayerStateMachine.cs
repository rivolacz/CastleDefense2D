using Project.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.StateMachines.States;

namespace Project.StateMachines
{
    public class PlayerStateMachine : StateMachine
    {
        public BaseState AttackState;
        public BaseState IdleState;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            unitMovement = GetComponent<UnitMovement>();
            IdleState = new IdleState(this);
            CurrentState = IdleState;
            unitAnimatorValuesSetter = new UnitAnimatorValuesSetter(animator);
        }
    }
}

using Project.StateMachines;
using Project.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.StateMachines
{
    
    public class EnemyStateMachine : StateMachine
    {
        public BaseState AttackState;
        public BaseState IdleState;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            unitMovement = GetComponent<UnitMovement>();
            IdleState = new IdleState(this);
            CurrentState = new IdleState(this);
            unitAnimatorValuesSetter = new UnitAnimatorValuesSetter(animator);
            unitAnimatorValuesSetter.canAttack = true;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
    public class UnitAnimatorValuesSetter
    {
        public Animator animator;
        private readonly int movementX;
        private readonly int movementY;
        private readonly int attackTrigger;
        public bool canAttack = true;

        public UnitAnimatorValuesSetter() { }

        public UnitAnimatorValuesSetter(Animator animator)
        {
            this.animator = animator;
            movementX = Animator.StringToHash("MovementX");
            movementY = Animator.StringToHash("MovementY");
            attackTrigger = Animator.StringToHash("Attack");
        }

        public void SetMovementValues(Vector2 movementDirection)
        {

            animator.SetFloat(movementX, movementDirection.x);
            animator.SetFloat(movementY, movementDirection.y);
        }

        public void SetAttackTrigger()
        {
            if (canAttack)
            {
                animator.SetTrigger(attackTrigger);
            }
        }

        public void AttackChangeBool(bool attack)
        {
            canAttack = attack;
        }
    }
}

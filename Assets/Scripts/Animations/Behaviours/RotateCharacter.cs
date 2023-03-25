using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Animations.Behaviours
{
    public class RotateCharacter : StateMachineBehaviour
    {
        private readonly int movementXHash = Animator.StringToHash("MovementX");
        private readonly Quaternion lookingLeftQuaternion = new Quaternion(0, 0, 0, 0);
        private readonly Quaternion lookingRightQuaternion = new Quaternion(0, 180, 0, 0);

        override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float movementX = animator.GetFloat(movementXHash);
            if(movementX > 0)
            {
                animator.rootRotation = lookingRightQuaternion;
            }
            else if (movementX < 0)
            {
                animator.rootRotation = lookingLeftQuaternion;
            }
        }
    }
}

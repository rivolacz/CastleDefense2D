using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Project.Units
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField]
        private UnitStats unitStats;
        [SerializeField]
        private Transform transformToRotate;
        private new Rigidbody2D rigidbody;
        private readonly Quaternion lookingLeftQuaternion = new Quaternion(0, 0, 0, 0);
        private readonly Quaternion lookingRightQuaternion = new Quaternion(0, 180, 0, 0);
        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 movementDirection, bool updateLookDirection)
        {
            Vector2 currentPosition = transform.position;
            Vector2 destination = currentPosition + movementDirection * Time.fixedDeltaTime * unitStats.BaseMovementSpeed;
            rigidbody.MovePosition(destination);
            if (updateLookDirection)
            {
                Look(movementDirection);
            }
        }

        public void Look(Vector2 lookDirection)
        {
            if (lookDirection.x > 0)
            {
                transformToRotate.rotation = lookingRightQuaternion;
            }
            else if (lookDirection.x < 0)
            {
                transformToRotate.rotation = lookingLeftQuaternion;
            }
        }
    }
}

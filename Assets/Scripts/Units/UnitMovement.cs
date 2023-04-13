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
        [SerializeField]
        private bool rotateExactlyToLookDirection = false;
        private new Rigidbody2D rigidbody;
        private readonly Quaternion lookingLeftQuaternion = new Quaternion(0, 0, 0, 0);
        private readonly Quaternion lookingRightQuaternion = new Quaternion(0, 180, 0, 0);
        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 movementDirection, bool updateLookDirection, float movementSpeedBonus)
        {
            Vector2 currentPosition = transform.position;
            if (unitStats.BaseMovementSpeed + movementSpeedBonus < 0) return;
            Vector2 destination = currentPosition + movementDirection * Time.fixedDeltaTime * (unitStats.BaseMovementSpeed + movementSpeedBonus);
            rigidbody.MovePosition(destination);
            if (updateLookDirection)
            {
                Look(movementDirection);
            }
        }

        public void Look(Vector2 lookDirection)
        {
            if (rotateExactlyToLookDirection)
            {
                RotateExactlyToLookDirection(lookDirection);
                return;
            }
            if (lookDirection.x > 0)
            {
                transformToRotate.rotation = lookingRightQuaternion;
            }
            else if (lookDirection.x < 0)
            {
                transformToRotate.rotation = lookingLeftQuaternion;
            }
        }

        public void RotateExactlyToLookDirection(Vector2 lookDirection)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, angle);
            transformToRotate.rotation = rotation;
        }
    }
}

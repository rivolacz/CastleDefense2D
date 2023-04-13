using Project.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Project.Objects
{
    public class TimeWarp : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask;
        private Vector2 target;
        private float range;
        private float effectDuration;
        private float slowDownMovement;
        private float slowDownAttackSpeed;

        public void SetData(Vector2 target, float range, float slowDownMovement, float slowDownAttackSpeed, float effectDuration)
        {
            this.target = target;
            this.range = range;
            this.slowDownMovement = slowDownMovement;
            this.slowDownAttackSpeed = slowDownAttackSpeed;
            this.effectDuration = effectDuration;
            transform.position = target;
            transform.localScale = Vector2.one*range;
            Invoke(nameof(DisableEffect), effectDuration);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;
            Debug.Log(collision.name);
            if(collision.TryGetComponent<StateMachine>(out var stateMachine))
            {
                stateMachine.SlowDownUnit(slowDownMovement);
                stateMachine.unitAnimatorValuesSetter.UnitStartedToBeSlowedDown(slowDownAttackSpeed);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == null) return;
            Debug.Log(collision.name);
            CancelEffect(collision);
        }

        private void CancelEffect(Collider2D collider)
        {
            if (collider == null) return;
            if (collider.TryGetComponent<StateMachine>(out var stateMachine))
            {
                stateMachine.CancelSlow();
                stateMachine.unitAnimatorValuesSetter.UnitStoppedBeingSlowedDown();
            }
        }


        public void DisableEffect()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(target, range, layerMask);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.name);

                CancelEffect(collider);
            }
            Destroy(gameObject);
        }
    }
}

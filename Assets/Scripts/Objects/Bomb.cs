using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Animator))]
    public class Bomb : MonoBehaviour
    {
        private Animator animator;
        private float damage = 500;
        const string explodeTrigger = "Explode";

        Collider2D objectToDestroy = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            objectToDestroy = collision;
            GetComponent<Animator>().SetTrigger(explodeTrigger);
        }

        public void Explosion()
        {
            if (objectToDestroy == null) return;
            IDamageable damageable = objectToDestroy.gameObject.GetComponent<IDamageable>();
            damageable.Damage(damage);
            Destroy(gameObject);
        }

        public void RotateRandomly()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
        }
    }
}

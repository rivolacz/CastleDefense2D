using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Meteorite : MonoBehaviour
    {
        [SerializeField]
        private CinemachineImpulseSource impulseSource;
        [SerializeField]
        private LayerMask layerMask;
        float speed = 30;
        [SerializeField]
        private Sprite impactSprite;

        private SpriteRenderer spriteRenderer;
        private Vector2 target;
        private float range;
        private float damage;
        private float fadeAwayTime = 10;
        private bool impacted = false;
        private float force = 2500;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (target == null ||impacted) return;
            transform.Translate(Vector3.Normalize((Vector3)target - transform.position) * speed* Time.deltaTime);
            float distance = Vector3.Distance(transform.position, target);
            if(distance < .3f)
            {
                Impact();
            }
        }

        private void Impact()
        {
            spriteRenderer.sprite = impactSprite;
            DealDamage();
            StartCoroutine(StartFadingAway());
            impacted = true;
            transform.position = target;
            transform.localScale = Vector3.one * 3;
            impulseSource.GenerateImpulse();
        }

        public void SetData(Vector2 position,float range, float damage)
        {
            this.range = range;
            target = position;
            this.damage = damage;
            transform.position = position + new Vector2(10, 40);
        }

        private void DealDamage()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(target, range, layerMask);
            foreach (Collider2D collider in colliders)
            {
                if (!collider.TryGetComponent<IDamageable>(out var damageable)) continue;
                Debug.Log(collider.name);
                Vector2 direction = collider.transform.position - transform.position;
                float distance = direction.magnitude;
                damageable.Damage(damage);
                if (!collider.TryGetComponent<Rigidbody2D>(out var rb)) continue;
                Vector2 forceVector = direction.normalized * force * (1 - distance / range);
                Debug.Log(forceVector);
                rb.AddForce(forceVector, ForceMode2D.Force);
            }
        }


        private IEnumerator StartFadingAway()
        {
            float time = fadeAwayTime;
            while(time > 0)
            {
                yield return null;
                time -= Time.deltaTime;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, time / fadeAwayTime);
            }
            Destroy(gameObject);
        }
    }
}

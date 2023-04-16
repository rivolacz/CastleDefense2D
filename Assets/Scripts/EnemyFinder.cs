using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Project
{
    public static class EnemyFinder
    {
        private const float pointRange = 4f;
        public static Transform GetEnemyTransform(LayerMask enemyLayerMask, Vector2 position, float range)
        {
            Collider2D[] colliders = GetEnemiesInRange(enemyLayerMask,position, range);
            Collider2D collider = GetClosestCollider(colliders, position);
            if (collider == null) return null;
            return collider.transform;
        }

        public static Transform GetEnemyTransform(LayerMask enemyLayerMask, Vector2 position)
        {
            Collider2D[] colliders = GetEnemiesInRange(enemyLayerMask, position, pointRange);
            Collider2D collider = GetClosestCollider(colliders, position);
            if (collider == null) return null;
            return collider.transform;
        }

        private static Collider2D[] GetEnemiesInRange(LayerMask enemyLayerMask, Vector2 position, float range)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range, enemyLayerMask);
            return colliders;
        }

        private static Collider2D GetClosestCollider(Collider2D[] colliders, Vector2 position)
        {
            float closestDistance = float.MaxValue;
            Collider2D closestCollider = null;
            foreach(Collider2D collider in colliders)
            {
                float distance = ((Vector2)collider.transform.position - position).magnitude;
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }
            return closestCollider;
        }
    }
}

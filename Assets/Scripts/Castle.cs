using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Castle : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private Canvas buyUnitCanvas;
        [SerializeField]
        private CanvasManager canvasManager;
        private float health = 999999999999;

        public void Damage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("Clicked");
            canvasManager.ReenableCanvas(buyUnitCanvas);
        }

    }
}

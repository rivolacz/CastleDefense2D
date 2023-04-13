using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image fillBarImage;

        public void FillProgressBar(float percentage)
        {
            if (fillBarImage != null)
            {
                fillBarImage.fillAmount = percentage;
            }
        }

        public void ChangeColor(Color color)
        {
            fillBarImage.color = color;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(TMP_Text))]
    public class LerpTextValue : MonoBehaviour
    {
        private float lerpDuration = 1f; // The duration of the lerping effect
        private TMP_Text moneyText;
        private float currentMoney;
        private float targetMoney;

        void Start()
        {
            moneyText = GetComponent<TMP_Text>();
            currentMoney = 0f;
            targetMoney = 0f;
            UpdateMoneyText();
        }

        public void SetTargetMoney(float target)
        {
            targetMoney = target;
            StartCoroutine(LerpMoneyValue());
        }

        IEnumerator LerpMoneyValue()
        {
            float timeElapsed = 0f;

            while (timeElapsed < lerpDuration)
            {
                currentMoney = Mathf.Lerp(currentMoney, targetMoney, timeElapsed / lerpDuration);
                UpdateMoneyText();
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            currentMoney = targetMoney;
            UpdateMoneyText();
        }

        void UpdateMoneyText()
        {
            moneyText.text = "+" + currentMoney.ToString("0");
        }
    }
}

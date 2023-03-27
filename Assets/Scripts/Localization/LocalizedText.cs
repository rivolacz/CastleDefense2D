using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Project.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField]
        private string textKey = "FillMeUp";

        TextMeshProUGUI text;

        private void Awake()
        {
            WordsDictionary.OnChangedLanguage += SetText;
            if (text == null)
            {
                text = GetComponent<TextMeshProUGUI>();
            }          
        }

        private void OnDestroy()
        {
            WordsDictionary.OnChangedLanguage -= SetText;
        }

        public void SetText()
        {
            string localizedValue = WordsDictionary.GetLocalizedText(textKey);
            if (text == null) return;
            text.text = localizedValue;
            var font = WordsDictionary.GetCurrentFont();
            text.font = font;
        }
    }
}

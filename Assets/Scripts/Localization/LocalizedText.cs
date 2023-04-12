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
        private bool localize = true;

        private void Awake()
        {
            textKey = textKey.Trim();
            WordsDictionary.OnChangedLanguage += SetText;        
        }

        private void OnEnable()
        {
            SetText();
        }

        private void OnDestroy()
        {
            WordsDictionary.OnChangedLanguage -= SetText;
        }

        public void SetText()
        {
            if (!localize) return;
            string localizedValue = WordsDictionary.GetLocalizedText(textKey);
            if (text == null)
            {
                text = GetComponent<TextMeshProUGUI>();
            }
            if (string.IsNullOrEmpty(localizedValue)) return;
            text.text = localizedValue;
            var font = WordsDictionary.GetCurrentFont();
            if (font != null)
            {
                text.font = font;
            }
        }

        public void ChangeKey(string key)
        {
            textKey = key;
            SetText();
        }

        public void ShouldLocalize(bool localize)
        {
            this.localize = localize;
        }
    }
}

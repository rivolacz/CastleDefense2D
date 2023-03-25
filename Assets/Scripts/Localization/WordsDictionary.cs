using Project.Assets.Scripts.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Project.Localization
{
    public class WordsDictionary : MonoBehaviour
    {
        public Language defaultLanguage;
        public static Language DefaultLanguage;
        private static Dictionary<Language, Dictionary<string, string>> languagesData;
        private static Language currentLanguage;
        private static Dictionary<string, string> currentLanguageData;

        public delegate void ChangedLanguage();
        public static event ChangedLanguage OnChangedLanguage;

        private void Awake()
        {
            DefaultLanguage = defaultLanguage;
        }

        public static void SetNewLanguage(Language language)
        {
            currentLanguage = language;
            if (languagesData.ContainsKey(language))
            {
                currentLanguageData = languagesData[language];
            }
            else
            {
                var languageData = LanguageLoader.GetLanguageData(language.CSVFile);
                languagesData.Add(language, languageData);
                currentLanguageData = languageData;
            }
            OnChangedLanguage?.Invoke();
        }

        public static string GetLocalizedText(string key)
        {
            if (currentLanguageData == null) {
                SetNewLanguage(DefaultLanguage);
            }
            bool success = currentLanguageData.TryGetValue(key, out var text);
            if(!success) {
                Debug.LogError($"Missing localized data for language: {currentLanguage.Name} and key: {key}");
            }
            return text;
        }
    }
}

using Project.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace Project.Localization
{
    public static class WordsDictionary
    {
        private static Dictionary<Language, Dictionary<string, string>> languagesData = new Dictionary<Language, Dictionary<string, string>>();
        private static Language currentLanguage;
        private static Dictionary<string, string> currentLanguageData;

        public delegate void ChangedLanguage();
        public static event ChangedLanguage OnChangedLanguage;

        public static void SetNewLanguage(Language language)
        {
            if (language == null) return;
            currentLanguage = language;
            if (languagesData.ContainsKey(language))
            {
                currentLanguageData = languagesData[language];
            }
            else
            {
                var languageData = LanguageLoader.GetLanguageData(language.CSVFile);
                if(languageData == null)
                {
                    Debug.LogError($"{language.Name} doesnt contain CSV file");
                }
                languagesData.Add(language, languageData);
                currentLanguageData = languageData;
            }
            OnChangedLanguage?.Invoke();
        }

        public static string GetLocalizedText(string key)
        {
            if (currentLanguageData == null) 
            {
                return string.Empty;
            }
            bool success = currentLanguageData.TryGetValue(key, out var text);
            if(!success) {
                Debug.LogError($"Missing localized data for language: {currentLanguage.Name} and key: {key}");
                return string.Empty;
            }
            return text;
        }

        public static TMP_FontAsset GetCurrentFont()
        {
            return currentLanguage?.Font;
        }
    }
}

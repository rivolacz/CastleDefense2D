using Project.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class EditorCode : MonoBehaviour
    {
        public TextAsset defaultLanguage;

        public void Awake()
        {
            if (Application.isEditor)
            {
                Destroy(gameObject);
            }
            Language language = new Language();
            language.CSVFile = defaultLanguage;
            WordsDictionary.SetNewLanguage(language);
        }
    }
}

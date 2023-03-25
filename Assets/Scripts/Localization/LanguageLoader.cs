using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Assets.Scripts.Localization
{
    public static class LanguageLoader
    {
        public static Dictionary<string, string> GetLanguageData(TextAsset languageFile)
        {
            Dictionary<string, string> languageData = new Dictionary<string, string>();
            List<string> dataLines = languageFile.text.Split('\n').ToList();
            dataLines.RemoveAt(0);
            foreach (string line in dataLines)
            {
                var data = line.Split(',');
                string key = data[0];
                string value = data[1];
                if (!string.IsNullOrEmpty(key))
                {
                    languageData.Add(key, value);
                }
            }
            return languageData;
        }
    }
}

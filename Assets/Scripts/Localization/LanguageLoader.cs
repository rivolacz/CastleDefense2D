using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Localization
{
    public static class LanguageLoader
    {
        public static Dictionary<string, string> GetLanguageData(TextAsset languageFile)
        {
            if (languageFile == null) return null;
            Dictionary<string, string> languageData = new Dictionary<string, string>();
            List<string> dataLines = languageFile.text.Split('\n').ToList();
            dataLines.RemoveAt(0);
            foreach (string line in dataLines)
            {
                var data = line.Split(",\"");
                if (data.Length < 2) {
                    data = line.Split(",");
                    if (data.Length < 2) continue;
                }
                string key = data[0];
                string value = MergeLines(data);
                if (!string.IsNullOrEmpty(key) && key != " ")
                {
                    try
                    {
                        if (value.Length > 3 && value[value.Length-2] == '"')
                        {
                            value = value.Remove(value.Length-2,2);
                        }
                        languageData.Add(key, value);
                    }catch(Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
            return languageData;
        }

        public static string MergeLines(string[] lines)
        {
            string line = "";
            for (int i = 1; i < lines.Length; i++)
            {
                line += lines[i];
            }
            return line;
        }
    }
}

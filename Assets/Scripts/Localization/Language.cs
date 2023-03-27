using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Project.Localization
{
    [Serializable]
    public class Language
    {
        public string Name;
        public Sprite Flag;
        public TextAsset CSVFile;
        public TMP_FontAsset Font;
    }
}

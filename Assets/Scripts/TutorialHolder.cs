using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

namespace Project
{
    public class TutorialHolder : MonoBehaviour
    {
        private const string firstTimeKey = "PlayerPlayingFirstTime";
        [SerializeField]
        private GameObject tutorialCanvas;

        private void Awake()
        {
            bool showTutorial = !PlayerPrefs.HasKey(firstTimeKey);
            if (showTutorial)
            {
                tutorialCanvas.SetActive(true);
                PlayerPrefs.SetInt(firstTimeKey, Convert.ToInt32(1));
            }
        }
    }
}

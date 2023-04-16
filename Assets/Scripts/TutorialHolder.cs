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
        private Canvas tutorialCanvas;

        private void Awake()
        {
            bool askForTutorial = !PlayerPrefs.HasKey(firstTimeKey);
        }

        
        public void CancelTutorial()
        {
            PlayerPrefs.SetInt(firstTimeKey, Convert.ToInt32(1));
            SendDataToAnalytics();
        }

        public void SendDataToAnalytics()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
            { "Time", 0 },
            };
            AnalyticsService.Instance.CustomData("TutorialFinished", parameters);
        }
    }
}

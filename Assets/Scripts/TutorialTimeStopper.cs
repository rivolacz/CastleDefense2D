using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

namespace Project
{
    public class TutorialTimeStopper : MonoBehaviour
    {
        private float startReading;
        private void OnEnable()
        {
            startReading = Time.time;
        }

        public void SendDataToAnalytics()
        {
            float readingTime = Time.time - startReading;
            Debug.Log(readingTime);
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
            { "Time", readingTime },
            };
            Debug.Log("Time finished" + readingTime);
            AnalyticsService.Instance.CustomData("TutorialFinished", parameters);
        }
    }
}

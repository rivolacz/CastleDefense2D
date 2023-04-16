using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public class GameOver : MonoBehaviour
    {
        public void LoadPlayerMenu()
        {
            Debug.Log("Loading");
            SceneManager.LoadScene(1);
        }

        public void SendWinDataToAnalytics()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
            { "Retry", UpgradesManager.Upgrades.Retries },
            };
            AnalyticsService.Instance.CustomData("TutorialFinished", parameters);
        }

        public void SendLostDataToAnalytics()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
            { "Retry", UpgradesManager.Upgrades.Retries },
            { "Wave",GameData.CurrentWave }
            };
            AnalyticsService.Instance.CustomData("TutorialFinished", parameters);
        }
    }
}

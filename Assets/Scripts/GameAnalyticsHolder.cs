using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

namespace Project
{
    public class GameAnalyticsHolder : MonoBehaviour
    {
        // Start is called before the first frame update
        async void Start()
        {
            try
            {
                Debug.Log("init");
                await UnityServices.InitializeAsync();
                List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
                foreach (string consentIdentifier in consentIdentifiers)
                {
                    if (consentIdentifier == "pipl")
                    {
                        AnalyticsService.Instance.ProvideOptInConsent(consentIdentifier, false);
                    }
                }
            }
            catch (ConsentCheckException e)
            {
                // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
            }
        }
    }
}

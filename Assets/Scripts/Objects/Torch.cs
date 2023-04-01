using Project.Waves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private void OnEnable()
    {
        WaveManager.OnDayStarted += DayStarted;
        WaveManager.OnNightStarted += NightStarted;
    }

    private void OnDisable()
    {
        WaveManager.OnDayStarted -= DayStarted;
        WaveManager.OnNightStarted -= NightStarted;
    }

    public void DayStarted()
    {

    }

    public void NightStarted()
    {

    }
}

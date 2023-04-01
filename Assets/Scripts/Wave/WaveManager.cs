using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Project.Waves.WaveManager;

namespace Project.Waves
{
    public class WaveManager : MonoBehaviour
    {
        public delegate void DayStarted();
        public static event DayStarted OnDayStarted;

        public delegate void NightStarted();
        public static event NightStarted OnNightStarted;

        public List<SpawnPointManager> SpawnPointManagers;
        public List<WaveTimes> WaveTimes;
        private WaveTimes currentWaveTime;    
        private int waveIndex = 1;


        private void Start()
        {
            currentWaveTime = WaveTimes.First();
            StartCoroutine(nameof(NextWave));
        }

        private void StartWave()
        {
            foreach (var spawnPoint in SpawnPointManagers) {
                spawnPoint.SpawnWave(waveIndex);
            }
            waveIndex++;
        }

        public IEnumerable NextWave()
        {
            StartWave();
            OnDayStarted?.Invoke();
            yield return currentWaveTime.WaveTimeLength;
            OnNightStarted?.Invoke();
            yield return currentWaveTime.NightTime;
            StartCoroutine(nameof(NextWave));
        }
    }
}

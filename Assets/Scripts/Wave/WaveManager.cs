using FunkyCode;
using Project.Localization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
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
        public List<WavePathHolder> WavePathHolders;

        [SerializeField]
        private LightCycle lightCycle;
        [SerializeField]
        private TMP_Text dayCounter;
        [SerializeField]
        private TMP_Text bigDayCounter;
        [SerializeField]
        private Animator bigDayCounterAnimator;
        [SerializeField]
        private Canvas winGameCanvas;
        private WaveTimes currentWaveTime;
        private int waveNumber = 1;
        private int waveTimesIndex = 1;
        private int wavePathHoldersIndex = 0;

        private void Start()
        {
            var font = WordsDictionary.GetCurrentFont();
            if(font != null)
            {
                bigDayCounter.font = font;
            }
            currentWaveTime = WaveTimes.First();
            StartCoroutine(nameof(NextWave));
        }

        private void StartWave()
        {
            if(waveNumber == 51)
            {
                GameWined();
            }
            dayCounter.text = waveNumber.ToString();
            bigDayCounter.text = $"{WordsDictionary.GetLocalizedText("day").Trim()} {waveNumber}";
            bigDayCounterAnimator.SetTrigger("ShowDay");
            CheckWaveUpdates();
            foreach (var spawnPoint in SpawnPointManagers) {
                spawnPoint.SpawnWave(waveNumber);
            }
            waveNumber++;
            GameData.CurrentWave = waveNumber;
        }

        private void CheckWaveUpdates()
        {
            CheckForWaveTimes();
            CheckForNewPath();
        }

        private void CheckForWaveTimes()
        {
            if (waveTimesIndex + 1> WaveTimes.Count) return;
            var waveTime = WaveTimes[waveTimesIndex];
            if(waveTime.Wave >= waveNumber)
            {
                waveTimesIndex++;
                currentWaveTime = waveTime;
            }
        }

        private void CheckForNewPath()
        {
            if (wavePathHoldersIndex + 1 > WavePathHolders.Count) return;
            var pathHolder = WavePathHolders[wavePathHoldersIndex];
            Debug.Log(pathHolder.Wave + "   " + waveNumber);
            if (pathHolder.Wave <= waveNumber)
            {
                wavePathHoldersIndex++;
                TilemapRenderer renderer = pathHolder.PathToUnlock.GetComponent<TilemapRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = true;
                }
            }
        }

        private void GameWined()
        {
            winGameCanvas.enabled = true;
            Time.timeScale = 0;
            UpgradesManager.Upgrades.Coins += 1000;
            UpgradesManager.Upgrades.Retries += 1;
            UpgradesManager.SaveUpgrades();
        }

        public IEnumerator NextWave()
        {
            StartWave();
            OnDayStarted?.Invoke();
            float time = 0;
            float waitTime = 0.2f;
            while (time < currentWaveTime.WaveTimeLength)
            {
                yield return new WaitForSeconds(waitTime);
                float portionOfDay = time / currentWaveTime.WaveTimeLength;
                lightCycle.SetTime(portionOfDay);
                time+= waitTime;
            }
            OnNightStarted?.Invoke();
            lightCycle.SetTime(0);
            yield return new WaitForSeconds(currentWaveTime.NightTime);
            StartCoroutine(nameof(NextWave));
        }
    }
}

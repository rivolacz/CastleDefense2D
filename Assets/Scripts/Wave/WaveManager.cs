using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Waves
{
    public class WaveManager : MonoBehaviour
    {
        public List<SpawnPointManager> SpawnPointManagers;

        private int waveIndex = 1;

        private void Start()
        {
            StartWave();
        }

        private void StartWave()
        {
            foreach (var spawnPoint in SpawnPointManagers) {
                spawnPoint.SpawnWave(waveIndex);
            }
            waveIndex++;
        }
    }
}

using Project.Waves;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class SpawnPointManager : MonoBehaviour
    {
        public List<Wave> Waves;
        public List<Transform> UnitPath;

        private IEnumerator coroutine;
        private Transform unitSpawnPosition;
        private void Awake()
        {
            unitSpawnPosition = UnitPath[0];
        }

        public void SpawnWave(int waveNumber)
        {
            var wavesInThisRound = Waves.FindAll(wave => wave.WaveNumber == waveNumber);
            if (wavesInThisRound.Count == 0) return;
            foreach (var wave in wavesInThisRound) {
                StartSpawningEnemies(wave);
            }
        }

        private void StartSpawningEnemies(Wave currentWave)
        {
            foreach (var unit in currentWave.unitsInWave)
            {
                Debug.Log(unit.Count); ;
                StartCoroutine(nameof(SpawnUnitsCoroutine), unit);
            }
        }

        public IEnumerator SpawnUnitsCoroutine(SpawningUnits units)
        {
            Debug.Log("Spawning units");
            for (int i = 0; i < units.Count; i++)
            {
                GameObject unit = Instantiate(units.UnitPrefab, unitSpawnPosition.position, Quaternion.identity);
                EnemyUnit enemy = unit.GetComponent<EnemyUnit>();
                enemy.SetPath(UnitPath);
                yield return new WaitForSeconds(units.TimeBetweenUnitSpawns);
            }
        }
    }
}

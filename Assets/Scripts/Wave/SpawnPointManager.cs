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
        public void SpawnWave(int waveNumber)
        {
            var wavesInThisRound = Waves.FindAll(wave => wave.WaveNumber == waveNumber);
            if (wavesInThisRound == null) return;
            foreach (var wave in wavesInThisRound) {
                StartSpawningEnemies(wave);
            }
        }

        private void StartSpawningEnemies(Wave currentWave)
        {
            foreach (var unit in currentWave.unitsInWave)
            {
                StartCoroutine(nameof(SpawnUnitsCoroutine), unit);
            }
        }

        IEnumerator SpawnUnitsCoroutine(UnitsOnSpawnpoint units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                GameObject unit = Instantiate(units.UnitPrefab, transform);
                EnemyUnit enemy = unit.GetComponent<EnemyUnit>();
                enemy.SetPath(UnitPath);
                yield return new WaitForSeconds(units.TimeBetweenUnitSpawns);
            }
        }
    }
}

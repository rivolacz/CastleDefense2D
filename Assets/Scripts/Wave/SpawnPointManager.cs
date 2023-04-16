using Project.Waves;
using Project.StateMachines.States;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project
{
    public class SpawnPointManager : MonoBehaviour
    {
        public List<Wave> Waves;
        public List<Transform> UnitPath;
        public Transform castleTransform;

        [SerializeField]
        private LayerMask playerBuildingLayerMask;
        private IEnumerator coroutine;
        private Transform unitSpawnPosition;
        private void Awake()
        {
            unitSpawnPosition = UnitPath[0];
            if(castleTransform == null)
            {
                castleTransform = UnitPath.Last();
            }
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
                if (enemy == null) {
                    continue;
                }
                if (enemy is EnemyAirBaloon)
                {
                    yield return new WaitForSeconds(units.TimeBetweenUnitSpawns);
                }
                else if (enemy is EnemyCatapult)
                {
                    var catapult = enemy as EnemyCatapult;
                    List<Vector3> positions = UnitPath.Select(t => t.position).ToList();
                    DemolitionState demolitionState = new DemolitionState(positions, playerBuildingLayerMask, catapult.StateMachine);
                    Debug.Log("Catapult setting to demoliton");
                    catapult.ChangeState(demolitionState);
                    yield return new WaitForSeconds(units.TimeBetweenUnitSpawns);
                }
                else
                {
                    enemy.SetPath(UnitPath, castleTransform);
                    yield return new WaitForSeconds(units.TimeBetweenUnitSpawns);
                }
            }
        }
    }
}

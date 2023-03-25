using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Waves
{
    [CreateAssetMenu(fileName = "New wave", menuName = "ScriptableObjects/Wave")]
    public class Wave : ScriptableObject
    {
        public int WaveNumber;
        public List<UnitsOnSpawnpoint> unitsInWave;
        public float TimeBetweenWaves = 5f;
    }
}

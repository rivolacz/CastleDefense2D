using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Waves
{
    [System.Serializable]
    public class Wave
    {
        public int WaveNumber;
        public List<SpawningUnits> unitsInWave;
    }
}

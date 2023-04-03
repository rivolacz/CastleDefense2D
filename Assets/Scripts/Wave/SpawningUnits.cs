using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "New units info", menuName = "ScriptableObjects/Spawning units info")]
    public class SpawningUnits : ScriptableObject
    {
        public int Count;
        public float TimeBetweenUnitSpawns = 0.2f;
        public GameObject UnitPrefab;
    }
}

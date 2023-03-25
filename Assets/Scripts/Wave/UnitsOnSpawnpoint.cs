using System.Collections;
using System;
using UnityEngine;
using Project.Units;

namespace Project
{
    [Serializable]
    public class UnitsOnSpawnpoint
    {
        public int Count;
        public float TimeBetweenUnitSpawns = 0.2f;
        public GameObject UnitPrefab;
    }
}

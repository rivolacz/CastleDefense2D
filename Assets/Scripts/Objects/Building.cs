using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "New units info", menuName = "ScriptableObjects/Spawning units info")]
    public class Building : ScriptableObject
    {
        public Sprite BuildingIcon;
        public GameObject BuildingPrefab;
        public float BuildingCost = 1563;
    }
}

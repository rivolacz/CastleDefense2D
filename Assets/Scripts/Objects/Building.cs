using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "New building", menuName = "ScriptableObjects/Building")]
    public class Building : ScriptableObject
    {
        public Sprite BuildingIcon;
        public GameObject BuildingPrefab;
        public float BuildingCost;
    }
}

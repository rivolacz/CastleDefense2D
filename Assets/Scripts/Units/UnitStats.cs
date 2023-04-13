using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Units
{
    [CreateAssetMenu(fileName ="New unit", menuName ="ScriptableObjects/Unit")]
    public class UnitStats : ScriptableObject
    {
        public string Name;
        public float BaseMovementSpeed;
        public float MaxHealth;
        public float AttackDamage;
        public float AttackRange;
        public int BuyCost;
        public float RewardForKilling;
    }
}

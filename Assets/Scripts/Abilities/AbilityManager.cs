using System;
using UnityEngine;

namespace Project.Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField]
        private float MaxMana = 300;
        private float CurrentMana = 0;
        private float ManaPerSecond = 5;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Abilities
{
    public class FireballAbility : Ability
    {
        [SerializeField]
        private GameObject FireballPrefab;

        public override void Cast()
        {
            UnityEngine.Object.Instantiate(FireballPrefab);
        }
    }
}

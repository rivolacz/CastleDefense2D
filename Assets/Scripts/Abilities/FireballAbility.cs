using Project.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Abilities
{
    [System.Serializable]
    public class FireballAbility : Ability
    {
        public float Range = 3;
        private float fireballDamage = 70;
        [SerializeField]
        float ManaCost = 100;
        [SerializeField]
        private GameObject FireballPrefab;

        private FireballAbilityUpgrades FireballAbilityUpgrades
        {
            get
            {
                return UpgradesManager.Upgrades.FireballAbilityUpgrades;
            }
        }

        private Vector2 target;

        public override void Cast()
        {
            GameObject fireball = UnityEngine.Object.Instantiate(FireballPrefab);
            Meteorite meteorite = fireball.GetComponent<Meteorite>();
            meteorite.SetData(target, Range, fireballDamage);
        }

        public override float GetManaCost()
        {
            float manaCostReduction = 0;
            if (FireballAbilityUpgrades.ManaUsageReductionBought)
            {
                manaCostReduction = FireballAbilityUpgrades.ManaUsageReduction;
            }
            return ManaCost - manaCostReduction;
        }


        public override void SetTarget(Vector2 target)
        {
            this.target = target;
        }
    }
}

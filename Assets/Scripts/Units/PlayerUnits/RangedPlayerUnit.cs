using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Units
{
    public class RangedPlayerUnit : PlayerUnit
    {
        public override void Attack()
        {
            Shoot();
        }

        private void Shoot()
        {
            Debug.Log("Shoot");
        }
    }
}

using Project.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Units
{
    [RequireComponent(typeof(PlayerStateMachine))]
    public class PlayerUnit : AttackUnit, ISelectable
    {
        [SerializeField]
        private SpriteRenderer selectionObject;

        public void Select()
        {
            if (selectionObject != null)
            {
                selectionObject.enabled = true;
            }
        }

        public void Deselect()
        {
            if (selectionObject != null)
            {
                selectionObject.enabled = false;
            }
        }

        public override void Die()
        {
            FindAnyObjectByType<UnitSelection>().DeselectUnit(this);
            base.Die();
        }
    }
}

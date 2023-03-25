using Project.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Units
{
    [RequireComponent(typeof(PlayerStateMachine))]
    public class PlayerUnit : Unit, ISelectable
    {
        [SerializeField]
        private SpriteRenderer selectionObject;

        public void Select()
        {
            selectionObject.enabled = true;
        }

        public void Deselect()
        {
            selectionObject.enabled = false;
        }
    }
}

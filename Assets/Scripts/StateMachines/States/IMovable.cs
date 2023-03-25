using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.StateMachines.States
{
    public interface IMovable
    {
        public Vector3 TargetPosition { get; set; }

        public void RefreshPath();
    }
}

using Project.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class BuildingState : BaseState
    {
        private Building Building { get; set; }
        private Vector3 BuildingPosition { get; set; }
        private StateMachine StateMachine { get; set; }

        public BuildingState(Building building, Vector3 position, StateMachine stateMachine) : base(stateMachine)
        {
            Building = building;
            BuildingPosition = position;
            StateMachine = stateMachine;
        }

        public override void Enter()
        {
            Debug.Log("Building a building with cost of " + Building.BuildingCost);
        }
    }
}

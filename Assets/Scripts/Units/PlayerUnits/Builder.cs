using Project.StateMachines.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Builder : Units.PlayerUnit
    {
        private void Start()
        {
            
            BuildBuilding(new Building(), new Vector3(10,10,0));
        }

        public void BuildBuilding(Building building, Vector3 position)
        {
            StateMachine.ChangeState(new MoveState(position,StateMachine, new BuildingState(building, position, StateMachine)));
        }
    }
}

using Project.StateMachines;
using Project.StateMachines.States;
using Project.Units;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(EnemyStateMachine))]
    [RequireComponent(typeof(UnitMovement))]
    public class EnemyUnit : Unit
    {
        [SerializeField]
        private LayerMask enemyLayerMask;

        //private EnemyStateMachine stateMachine;

        private List<Transform> unitPath;
        private Transform targetToMove;
        private Vector3 targetPosition;
        private UnitMovement unitMovement;

        private void Awake()
        {
            unitMovement = GetComponent<UnitMovement>();
            StateMachine = GetComponent<EnemyStateMachine>();
        }

        private void Update()
        {
            if (targetToMove == null) return;
            //Move();
        }

        public void SetPath(List<Transform> path)
        {
            List<Vector3> pathPositions = new List<Vector3>();
            foreach(var node in path)
            {
                pathPositions.Add(node.position);
            }
            Castle castle = FindFirstObjectByType<Castle>();
            Transform castleTransform = castle.transform;
            StateMachine.ChangeState(new MoveState(pathPositions, StateMachine, castleTransform));
        }
    }
}

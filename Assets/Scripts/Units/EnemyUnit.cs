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
    public class EnemyUnit : AttackUnit
    {
        [SerializeField]
        private LayerMask enemyLayerMask;

        //private EnemyStateMachine stateMachine;

        private List<Transform> unitPath;
        private Transform targetToMove;
        private Vector3 targetPosition;
        private UnitMovement unitMovement;
        private PlacementAroundTarget placementAroundTarget = null;
        private Transform placementPosition = null;
        private void Awake()
        {
            unitMovement = GetComponent<UnitMovement>();
            StateMachine = GetComponent<EnemyStateMachine>();
        }

        private void Update()
        {
            if (targetToMove == null) return;
        }

        public void SetPath(List<Transform> path)
        {
            List<Vector3> pathPositions = new List<Vector3>();
            foreach(var node in path)
            {
                pathPositions.Add(node.position);
            }
            pathPositions.RemoveAt(0);
            Castle castle = path.Last().GetComponent<Castle>();
            Transform castleTransform = castle.transform;
            StateMachine.ChangeState(new MoveState(pathPositions, StateMachine, castleTransform));
        }

        public void SetStraightPath(Transform castle, float offsetY)
        {
            StateMachine.ChangeState(new AttackState(castle, offsetY, StateMachine));
        }

        public override void Damage(float damage)
        {
            currentHealth -= damage;
            if (healthBar != null)
            {
                healthBar.gameObject.SetActive(true);
                healthBar.FillProgressBar(currentHealth / unitStats.MaxHealth);
            }
            if (currentHealth < 0)
            {
                Die();
            }
        }

        public override void Die()
        {
            if (placementAroundTarget != null)
            {
                placementAroundTarget.RemoveFromTaken(placementPosition);
                placementAroundTarget = null;
                placementPosition = null;
            }
            GameData.GetCoins(unitStats.RewardForKilling * GameData.CashMultiplierFromAbility);
            Destroy(gameObject);
        }

        public void AssignPlacementToTarget(PlacementAroundTarget placementAroundTarget, Transform position)
        {
            this.placementAroundTarget = placementAroundTarget;
            placementPosition = position;
            //Debug.Log($"Assigned position from {transform.position} to target" + position.position);
            Castle castle = FindAnyObjectByType<Castle>();
            StateMachine.ChangeState(new MoveState(placementPosition.position, StateMachine, castle.transform));
        }
    }
}

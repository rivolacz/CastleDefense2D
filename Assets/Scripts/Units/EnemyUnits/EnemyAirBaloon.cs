using Project.StateMachines;
using Project.Units;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(EnemyStateMachine))]
    [RequireComponent(typeof(UnitMovement))]
    public class EnemyAirBaloon : EnemyUnit
    {
        [SerializeField]
        private GameObject BombPrefab;

        [SerializeField]
        private Transform SpawnPosition;

        private Transform castlePosition;
        private void Start()
        {
            castlePosition = FindFirstObjectByType<Castle>().transform;
            float offsetY = Random.Range(3f, 6f);
            StateMachine.ChangeState(new AttackState(castlePosition, offsetY, StateMachine));
        }

        public override void Attack()
        {
            Instantiate(BombPrefab, SpawnPosition.position, Quaternion.identity);
        }
    }
}

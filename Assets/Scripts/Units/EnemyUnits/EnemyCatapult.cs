using Project.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class EnemyCatapult : EnemyUnit
    {
        [SerializeField]
        private GameObject rockPrefab;
        [SerializeField]
        private Transform rockSpawnPosition;
        private Rock rock;
        private StateMachine stateMachine;

        public void InstantiateRock()
        {
            rock = Instantiate(rockPrefab, rockSpawnPosition.position, Quaternion.identity).GetComponent<Rock>();
            Debug.Log("Spawn rock");
        }

        public override void Attack()
        {
            Debug.Log("attack");
            stateMachine = GetComponent<StateMachine>();
            if (stateMachine == null) return;
            rock.SetTargetAndDamage(stateMachine.Target, unitStats.AttackDamage);
        }

    }
}

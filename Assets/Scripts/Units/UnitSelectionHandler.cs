using Project.StateMachines;
using Project.StateMachines.States;
using Project.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;


namespace Project
{
    [RequireComponent(typeof(UnitSelection))]
    public class UnitSelectionHandler : MonoBehaviour
    {
        [SerializeField]
        private LayerMask enemyLayerMask;
        private UnitSelection unitSelection;
        private PlayerInput input;

        private void Awake()
        {
            input = new PlayerInput();
            input.Player.Enable();
            unitSelection = GetComponent<UnitSelection>();
            EnhancedTouchSupport.Enable();
        }

        public void AttackButton()
        {
            unitSelection.CanDeselectAllUnits = false;
            input.Tap.Enable();
            input.Tap.TapPosition.started += _ => Attack();
        }

        public void MoveButton()
        {
            unitSelection.CanDeselectAllUnits = false;
            input.Tap.Enable();
            input.Tap.TapPosition.started += _ => Move();
        }

        public void Attack()
        {
            Vector2 attackPosition = input.Player.FirstTouchPosition.ReadValue<Vector2>();
            Transform attackTarget = EnemyFinder.GetEnemyTransform(enemyLayerMask, attackPosition);
            SetAttackStateToUnits(attackTarget);
            input.Tap.Disable();
            unitSelection.CanDeselectAllUnits = true;
            input.Tap.TapPosition.started -= _ => Attack();

        }

        public void Move()
        {
            Vector2 tapPosition = input.Tap.TapPosition.ReadValue<Vector2>();
            Vector2 movePosition = Camera.main.ScreenToWorldPoint(tapPosition);
            Debug.Log(movePosition);
            Ray ray = Camera.main.ScreenPointToRay(tapPosition);
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                movePosition = hit.point;
            }
            Debug.Log("Start moving to "+ movePosition);
            SetMoveStateToUnits(movePosition);
            input.Tap.Disable();
            input.Tap.TapPosition.started -= _ => Move();
            unitSelection.CanDeselectAllUnits = true;

        }

        private void SetAttackStateToUnits(Transform attackTarget)
        {
            List<ISelectable> selectables = unitSelection.GetSelectables();
            foreach(ISelectable selectable in selectables)
            {
                PlayerUnit unit = selectable as PlayerUnit;
                StateMachine stateMachine = unit.GetComponent<StateMachine>();
                stateMachine.AttackTarget(attackTarget);
            }
        }

        private void SetMoveStateToUnits(Vector2 movePosition)
        {
            List<ISelectable> selectables = unitSelection.GetSelectables();
            foreach (ISelectable selectable in selectables)
            {
                PlayerUnit unit = selectable as PlayerUnit;
                StateMachine stateMachine = unit.GetComponent<StateMachine>();
                stateMachine.ChangeState(new MoveState(movePosition,stateMachine));
            }
        }
    }
}

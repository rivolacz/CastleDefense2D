using Project.StateMachines;
using Project.StateMachines.States;
using Project.Units;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.Windows;


namespace Project
{
    [RequireComponent(typeof(UnitSelection))]
    public class UnitSelectionHandler : MonoBehaviour
    {
        [SerializeField]
        private LayerMask enemyLayerMask;
        [SerializeField]
        private LayerMask playerBuildingLayerMask;

        [SerializeField]
        private Button attackButton;
        [SerializeField]
        private Button moveButton;
        [SerializeField]
        private Button constructionButton;


        private UnitSelection unitSelection;
        private PlayerInput input;
        private Camera cam;
        private Vector2 worldTapPosition;
        private bool assigningAction = false;
        private void Awake()
        {
            input = new PlayerInput();
            input.Player.Enable();
            input.Tap.Enable();
            unitSelection = GetComponent<UnitSelection>();
            cam = Camera.main;
            EnhancedTouchSupport.Enable();
        }

        private void Update()
        {
            Vector2 tapPosition = input.Player.FirstTouchPosition.ReadValue<Vector2>();
            Vector2 newworldTapPosition = Camera.main.ScreenToWorldPoint(new Vector3(tapPosition.x, tapPosition.y, Camera.main.nearClipPlane));
            worldTapPosition = newworldTapPosition;
        }

        public void AttackButton()
        {
            ResetAllButtons();
            attackButton.transform.localScale = Vector3.one;
            unitSelection.CanDeselectAllUnits = false;
            input.Tap.Enable();
            input.Tap.TapPosition.started += _ => Attack();
        }

        public void MoveButton()
        {
            ResetAllButtons();
            moveButton.transform.localScale = Vector3.one;
            unitSelection.CanDeselectAllUnits = false;
            input.Tap.Enable();
            input.Tap.TapPosition.started += _ => Move();
        }


        public void BuildButton()
        {
            ResetAllButtons();
            constructionButton.transform.localScale = Vector3.one;
            unitSelection.CanDeselectAllUnits = false;
            input.Tap.Enable();
            input.Tap.TapPosition.started += _ => Build();
        }

        public void Attack()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Debug.Log("ATTACK");
            var attackPosition = WorldPosition();
            Transform attackTarget = EnemyFinder.GetEnemyTransform(enemyLayerMask, attackPosition);
            Debug.Log("Attack position" + attackPosition);
            if (attackTarget == null) {
                Debug.Log("No target found");
                return;
            }
            SetAttackStateToUnits(attackTarget);

        }

        public void Move()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Debug.Log("MOVE");
            Vector2 movePosition = WorldPosition();
            SetMoveStateToUnits(movePosition);
        }

        public void Build()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Vector2 buildPosition = WorldPosition();
            var colliders = Physics2D.OverlapCircleAll(buildPosition,2, playerBuildingLayerMask);
            Debug.Log(buildPosition + " has " + colliders.Length);
            ConstructionSite constructionSite = null;
            foreach (var collider in colliders)
            {
                Debug.Log(collider.name,collider.gameObject);
                constructionSite = collider.GetComponentInParent<ConstructionSite>();
                if (constructionSite != null)
                {
                    break;
                }
            }
            if (constructionSite == null) return;
            Debug.Log("Found construction site");
            SetBuildStateToUnits(constructionSite, buildPosition);
        }

        private void SetAttackStateToUnits(Transform attackTarget)
        {
            List<ISelectable> selectables = unitSelection.GetSelectables();
            foreach(ISelectable selectable in selectables)
            {
                PlayerUnit unit = selectable as PlayerUnit;
                if(unit == null || unit is Builder)  continue;
                StateMachine stateMachine = unit.GetComponent<StateMachine>();
                AttackState attackState = new AttackState(attackTarget, stateMachine);
                MoveState moveState = new MoveState(attackTarget.position, stateMachine, 0, attackState);
                stateMachine.ChangeState(moveState);
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

        private void SetBuildStateToUnits(ConstructionSite building, Vector2 position)
        {
            List<ISelectable> selectables = unitSelection.GetSelectables();
            foreach (ISelectable selectable in selectables)
            {
                Builder unit = selectable as Builder;
                Debug.Log(selectable.GetType());
                if (unit == null || unit is not Builder) continue;
                unit.BuildBuilding(building, position);
            }
        }

        private Vector3 WorldPosition()
        {
            Vector2 tapPosition = input.Player.FirstTouchPosition.ReadValue<Vector2>();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(tapPosition.x, tapPosition.y, Camera.main.nearClipPlane));
            return worldPosition;
        }

        private void ResetAllButtons()
        {
            attackButton.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            moveButton.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            constructionButton.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            input.Tap.TapPosition.started -= _ => Move();
            input.Tap.TapPosition.started -= _ => Attack();
            input.Tap.TapPosition.started -= _ => Build();

        }
    }
}

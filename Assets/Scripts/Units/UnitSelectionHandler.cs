using Project.StateMachines;
using Project.StateMachines.States;
using Project.Units;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
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
            if (unitSelection.GetSelectables().Count == 0)
            {
                assigningAction = false;
                return;
            }
            if (assigningAction) return;
            assigningAction = true;
            Debug.Log("ATTACK BUTTON");
            unitSelection.CanDeselectAllUnits = false;
            input.Tap.Enable();
            input.Tap.TapPosition.started += _ => Attack();
        }

        public void MoveButton()
        {
            if (unitSelection.GetSelectables().Count == 0)
            {
                assigningAction = false;
                return;
            }
            if (assigningAction) return;
            assigningAction = true;
            Debug.Log("MOVE BUTTON");
            unitSelection.CanDeselectAllUnits = false;
            input.Tap.Enable();
            input.Tap.TapPosition.started += _ => Move();
        }


        public void BuildButton()
        {
            if (unitSelection.GetSelectables().Count == 0) {
                assigningAction = false;
                return;
            }
            assigningAction = true;
            Debug.Log("BUILD BUTTON");
            unitSelection.CanDeselectAllUnits = false;
            input.Tap.Enable();
            input.Tap.TapPosition.started += _ => Build();
        }

        public void Attack()
        {
            Debug.Log("ATTACK");
            Vector2 tapPosition = input.Player.FirstTouchPosition.ReadValue<Vector2>();
            var attackPosition = TapPositionToWorldPosition(tapPosition,enemyLayerMask);
            Transform attackTarget = EnemyFinder.GetEnemyTransform(enemyLayerMask, attackPosition);
            SetAttackStateToUnits(attackTarget);
            unitSelection.CanDeselectAllUnits = true;
            assigningAction = false;
            input.Tap.TapPosition.started -= _ => Attack();
        }

        public void Move()
        {
            Debug.Log("MOVE");
            Vector2 movePosition = WorldPosition();
            SetMoveStateToUnits(movePosition);
            assigningAction = false;
            input.Tap.TapPosition.started -= _ => Move();
            unitSelection.CanDeselectAllUnits = true;
        }

        public void Build()
        {
            Vector2 buildPosition = WorldPosition();
            Debug.Log("BUILD" + buildPosition);
            var colliders = Physics2D.OverlapCircleAll(buildPosition,5, playerBuildingLayerMask);
            if(colliders.Length == 0)
            {
                Debug.Log("DIDNT COLLIDE");
            }
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
            assigningAction = false;
            unitSelection.CanDeselectAllUnits = true;
            input.Tap.TapPosition.started -= _ => Build();
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

        private void SetBuildStateToUnits(ConstructionSite building, Vector2 position)
        {
            List<ISelectable> selectables = unitSelection.GetSelectables();
            foreach (ISelectable selectable in selectables)
            {
                Builder unit = selectable as Builder;
                Debug.Log(selectable.GetType());
                if (unit == null) continue;
                unit.BuildBuilding(building, position);
            }
        }

        private Vector3 TapPositionToWorldPosition(Vector2 tapPosition, LayerMask layerMask)
        {
            Vector3 tapPositionWithZCoordinate = new Vector3(tapPosition.x, tapPosition.y, cam.transform.position.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(tapPosition.x, tapPosition.y, Camera.main.nearClipPlane));
            Vector2 buildPosition = Camera.main.ScreenToWorldPoint(tapPositionWithZCoordinate);
            Ray ray = Camera.main.ScreenPointToRay(tapPosition);
            if (Physics.Raycast(ray, out RaycastHit hit,1000, layerMask))
            {
                var pos =  hit.point;
                Debug.Log("Ray:" + pos + " --------"+ buildPosition);
                return pos;
            }
            Debug.Log($"Didnt hit {buildPosition} --- {worldPos}");
            return buildPosition;
        }

        private Vector3 WorldPosition()
        {
            Vector2 tapPosition = input.Player.FirstTouchPosition.ReadValue<Vector2>();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(tapPosition.x, tapPosition.y, Camera.main.nearClipPlane));
            return worldPosition;
        }
    }
}

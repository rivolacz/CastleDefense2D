using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Windows;
using System.Reflection;
using UnityEngine.InputSystem;

namespace Project
{
    public class UnitSelection : MonoBehaviour
    {
        [HideInInspector]
        public bool CanDeselectAllUnits { get; set; } = true;

        [SerializeField]
        private Canvas selectedActionCanvas;
        [SerializeField]
        private RectTransform selectionArea;
        [SerializeField]
        private LayerMask selectableLayerMask;
        private PlayerInput input;
        private Vector2 startUIPosition;
        private Vector3 startWorldPosition;
        private bool selecting = false;
        private List<ISelectable> selectables = new List<ISelectable>();

        private void Awake()
        {
            selectionArea.gameObject.SetActive(false);
            input = new PlayerInput();
            input.Enable();
            input.UnitSelection.Tap.performed += _ => Tap();
            input.UnitSelection.HoldFinger.performed += ctx => StartHolding(ctx);
            input.UnitSelection.HoldFinger.canceled += ctx => SelectUnits(ctx);
            input.UnitSelection.DoubleTap.performed += _ => DeselectAllUnits();
        }

        private void Update()
        {
            if (selecting)
            {
                Vector2 value = input.UnitSelection.FirstTouchInformation.ReadValue<Vector2>();
                ChangeSelectionBoxSize(value);
            }
        }


        public List<ISelectable> GetSelectables()
        {
            return selectables;
        }

        public void StartHolding(InputAction.CallbackContext context)
        {
            if (Settings.CameraMoving) {
                return;
            }
            selecting = true;
            Vector2 value = input.UnitSelection.FirstTouchInformation.ReadValue<Vector2>();
            selectionArea.gameObject.SetActive(true);
            startUIPosition = value;
        }

        public void Tap()
        {
            ClickOnScreen();                       
        }

        private void SelectUnits(InputAction.CallbackContext context)
        {
            if (!selecting) return;
            selectionArea.gameObject.SetActive(false);
            Vector2 touchPosition = input.UnitSelection.FirstTouchInformation.ReadValue<Vector2>();
            Vector3 endPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            startWorldPosition = Camera.main.ScreenToWorldPoint(startUIPosition);
            Collider2D[] colliders = Physics2D.OverlapAreaAll(startWorldPosition, endPosition, selectableLayerMask);
            Debug.Log(colliders.Length);
            foreach(Collider2D collider in colliders)
            {
                Debug.Log("SELECTING WITH BOX");
                ISelectable selectable = collider.GetComponent<ISelectable>();
                if (selectable == null) continue;
                if(selectables.Contains(selectable)) continue;
                SelectUnit(selectable);
            }
            selecting = false;
        }

        private void SelectUnit(ISelectable unit)
        {
            unit.Select();
            selectables.Add(unit);
            Debug.Log("Adding " + selectables.GetHashCode());
            if (!selectedActionCanvas.enabled)
            {
                selectedActionCanvas.enabled = true;
            }
        }

        private void DeselectUnit(ISelectable unit)
        {
            unit.Deselect();
            Debug.Log("Before removing from list has " + selectables.Count);
            selectables.Remove(unit);
            Debug.Log("Removing " + selectables.GetHashCode());

            Debug.Log("After removing from list has " + selectables.Count);

            if (selectables.Count == 0)
            {
                selectedActionCanvas.enabled = false;
            }
        }

        private void ChangeSelectionBoxSize(Vector2 toPosition)
        {
            Vector2 offset = toPosition - startUIPosition;
            selectionArea.sizeDelta = new Vector2(Mathf.Abs(offset.x),Mathf.Abs(offset.y));
            selectionArea.anchoredPosition = startUIPosition + offset / 2;
        }

        private void ClickOnScreen()
        {
            Vector2 touchPosition = input.Player.FirstTouchPosition.ReadValue<Vector2>();
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touchPosition);
            Collider2D[] colliders = Physics2D.OverlapPointAll(worldPoint, selectableLayerMask);
            foreach(Collider2D collider in colliders)
            {
                ISelectable selectable = collider.GetComponent<ISelectable>();
                if (selectable == null) continue;
                Debug.Log(selectable.ToString());
                if (selectables.Contains(selectable))
                {
                    DeselectUnit(selectable);
                }
                else
                {
                    SelectUnit(selectable);
                }
            }
        }

        private void DeselectAllUnits()
        {
            if (!CanDeselectAllUnits || selecting) return;
            selectables.ForEach(selectable => selectable.Deselect());
            selectables.Clear();
            selectedActionCanvas.enabled = false;
        }
    }
}

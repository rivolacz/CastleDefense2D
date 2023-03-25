using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Project {
    public class CameraZoom : MonoBehaviour
    {
        private const float CameraZoomThreshold = .9f;
        private float zoomingSpeed = 0.1f;
        private PlayerInput input;
        private Camera cam;
        private Vector2 lastFirstTouchPosition;
        private Vector2 lastSecondTouchPosition;
        private Vector2 firstTouchPosition;
        private Vector2 secondTouchPosition;
        private float lastDistance = 0;

        void Awake()
        {
            input = new PlayerInput();
            cam = Camera.main;
        }

        void OnEnable()
        {
            input.Enable();
        }

        private void Update()
        {
            firstTouchPosition = input.Player.FirstTouchPosition.ReadValue<Vector2>();
            secondTouchPosition = input.Player.SecondTouchPosition.ReadValue<Vector2>();
            if (firstTouchPosition != lastFirstTouchPosition && secondTouchPosition != lastSecondTouchPosition)
            {
                TouchDetected();
                lastFirstTouchPosition = firstTouchPosition;
                lastSecondTouchPosition = secondTouchPosition;
            }
            else
            {
                lastDistance = 0;
            }          
        }

        void OnDisable()
        {
            input.Disable();
        }

        private void TouchDetected()
        {
            Vector2 firstTouchDelta = input.Player.FirstTouchDelta.ReadValue<Vector2>();
            Vector2 secondTouchDelta = input.Player.SecondTouchDelta.ReadValue<Vector2>();
            if (Vector2.Dot(firstTouchDelta, secondTouchDelta) < CameraZoomThreshold)
            {
                Zooming();
            }
        }

        private void Zooming()
        {
            float newDistance = (firstTouchPosition - secondTouchPosition).magnitude;
            float distanceDifference = lastDistance - newDistance;
            float zoomingOffset = zoomingSpeed * distanceDifference * Time.deltaTime;
            zoomingOffset = (float)Math.Clamp(zoomingOffset, -1, 1);
            Zoom(zoomingOffset);
            lastDistance = newDistance;
        }

        private void Zoom(float offset)
        {
            float newSize = Math.Clamp(cam.orthographicSize + offset, 5f, 35);
            cam.orthographicSize = newSize;
        }
    }
}

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public CinemachineVirtualCamera cameraTransform;
    [SerializeField]
    private CinemachineConfiner2D confiner;
    private const float ZoomingSpeed = 10f;
    private const float CameraMovementSpeed = 10f;
    private bool doubleTouchActivated = false;
    PlayerInput input;
    Vector2 firstTouchDelta;
    Vector2 secondTouchDelta;
    Vector2 firstTouchPosition;
    Vector2 secondTouchPosition;
    private Camera cam;
    private float lastMovementTime = 0;
    private void Awake()
    {
        input = new PlayerInput();
        input.Camera.Enable();
        if(cameraTransform == null)
        {
            Debug.LogError("Assigne virtual camera to camera movement");
        }
        cam = Camera.main;
    }

    private void Update()
    {
        if (doubleTouchActivated)
        {
            Settings.CameraMoving = true;
            lastMovementTime = Time.time;
            float angle = Vector2.Angle(secondTouchDelta, firstTouchDelta);
            ProcessAngle(angle);
        }

        if(Time.time - lastMovementTime > 2f)
        {
            Settings.CameraMoving = false;
        }
    }

    

    private void OnEnable()
    {
        input.Camera.Enable();
        input.Camera.SecondTouchDelta.canceled += SetCameraPositionToBounds;
        input.Camera.SecondTouchDelta.performed += SecondTouchStarted;
        input.Camera.SecondTouchDelta.canceled += ResetValues;
        input.Camera.FirstTouchDelta.performed += FirstTouchStarted;
        input.Camera.FirstTouchPosition.performed += FirstTouchPosition;
        input.Camera.SecondTouchPosition.performed += SecondTouchPosition;

    }

    private void SecondTouchPosition(InputAction.CallbackContext context)
    {
        secondTouchPosition = context.ReadValue<Vector2>();

    }

    private void FirstTouchPosition(InputAction.CallbackContext context)
    {
        firstTouchPosition = context.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        input.Camera.SecondTouchDelta.canceled -= SetCameraPositionToBounds;
        input.Camera.SecondTouchDelta.performed -= SecondTouchStarted;
        input.Camera.SecondTouchDelta.canceled -= ResetValues;
        input.Camera.FirstTouchDelta.performed -= FirstTouchStarted;
        input.Camera.FirstTouchPosition.performed -= FirstTouchPosition;
        input.Camera.SecondTouchPosition.performed -= SecondTouchPosition;
        input.Camera.Disable();
    }

    private void SecondTouchStarted(InputAction.CallbackContext context)
    {
        doubleTouchActivated = true;
        secondTouchDelta = context.ReadValue<Vector2>();

    }
    private void FirstTouchStarted(InputAction.CallbackContext context)
    {
        firstTouchDelta = context.ReadValue<Vector2>();
    }

    private void ResetValues(InputAction.CallbackContext context)
    {
        doubleTouchActivated = false;
    }

    private void ProcessAngle(float angle)
    {
        if(angle < 30)
        {
            var average = GetDeltasAverage(firstTouchDelta, secondTouchDelta);
            average *= CameraMovementSpeed * Time.deltaTime;
            Vector3 currentPosition = cameraTransform.transform.position;
            Vector3 newPosition = currentPosition + new Vector3(average.x, average.y, 0f);
            MoveCamera(newPosition);       
        }
        else if(angle > 150)
        {
            Zooming();
        }
    }

    private void SetCameraPositionToBounds(InputAction.CallbackContext context)
    {
        cameraTransform.transform.position = Camera.main.transform.position;
    }

    private void MoveCamera(Vector3 newPosition)
    {
        cameraTransform.transform.position = newPosition;
    }

    private Vector2 GetDeltasAverage(Vector2 firstTouch, Vector2 secondTouch)
    {
        float x = (firstTouch.x + secondTouch.x)/2;
        float y = (firstTouch.y + secondTouch.y) / 2; 
        return new Vector2(x, y);
    }

    private void Zooming()
    {
        float distanceWithDelta = Vector2.Distance(firstTouchPosition + firstTouchDelta, secondTouchPosition + secondTouchDelta);
        float distance = Vector2.Distance(firstTouchPosition, secondTouchPosition);
        int modifier = 1;
        if(distanceWithDelta > distance)
        {
            modifier = -1;
        }
        var averageDelta = GetDeltasAverage(firstTouchDelta, secondTouchDelta) * ZoomingSpeed * Time.deltaTime ;
        float zoomingOffset = averageDelta.magnitude * modifier;
        Zoom(zoomingOffset);
    }

    private void Zoom(float offset)
    {
        float newSize = Math.Clamp(cam.orthographicSize + offset, 5f, 35);
        if(newSize != cameraTransform.m_Lens.OrthographicSize)
        {
            confiner.InvalidateCache();
        }
        cameraTransform.m_Lens.OrthographicSize = newSize;
    }
}

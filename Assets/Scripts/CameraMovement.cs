using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public CinemachineVirtualCamera cameraTransform;
    private const float Speed = 1f;
    PlayerInput input;
    private Vector2 moveValue;

    private void Awake()
    {
        input = new PlayerInput();
        input.Camera.Enable();
        if(cameraTransform == null)
        {
            Debug.LogError("Assigne virtual camera to camera movement");
        }
    }

    private void OnEnable()
    {
        input.Camera.Enable();
        input.Camera.Move.performed += OnMovePerformed;
        input.Camera.Move.canceled += SetCameraPositionToBounds;
    }

    private void OnDisable()
    {
        input.Camera.Move.performed -= OnMovePerformed;
        input.Camera.Move.canceled -= SetCameraPositionToBounds;
        input.Camera.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Settings.CameraMoving = true;
        moveValue = context.ReadValue<Vector2>();
        moveValue = moveValue.normalized * Speed;
        Vector3 currentPosition = cameraTransform.transform.position;

        // Calculate the new position based on moveValue
        Vector3 newPosition = currentPosition + new Vector3(moveValue.x, moveValue.y, 0f);
        cameraTransform.transform.position = newPosition;
    }

    private void SetCameraPositionToBounds(InputAction.CallbackContext context)
    {
        Settings.CameraMoving = false;
        cameraTransform.transform.position = Camera.main.transform.position;
    }
}

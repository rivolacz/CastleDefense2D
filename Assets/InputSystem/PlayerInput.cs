//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.0
//     from Assets/InputSystem/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c191193d-746c-4bbc-97cc-29ab7f9d22cc"",
            ""actions"": [
                {
                    ""name"": ""FirstTouchPosition"",
                    ""type"": ""Value"",
                    ""id"": ""9533cd12-82a6-41cf-8caa-93751a2414b4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondTouchPosition"",
                    ""type"": ""Value"",
                    ""id"": ""d84d97ae-d1e9-424d-ba9d-dc0ea44cf535"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FirstTouchDelta"",
                    ""type"": ""Value"",
                    ""id"": ""8dfc037a-b38a-4470-881d-d505407b98e6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondTouchDelta"",
                    ""type"": ""Value"",
                    ""id"": ""b2ef82e9-158f-4b9d-9cb5-548b0c750b3c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""2b88a57d-3168-449c-8838-c549388526dd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DoubleTapReleased"",
                    ""type"": ""Value"",
                    ""id"": ""d00b3dbc-7729-48e4-b7a5-e8ad4919dd9e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a75f8f45-b612-47ad-ba7b-95551487ff19"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstTouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42164c30-70b0-4f1c-8ac4-3231f5afaba5"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstTouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""332b8ec0-5427-4ba6-b678-46c0b2028b8f"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondTouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a16d3c76-0cc1-404d-a6a9-0cb4b8bb29d8"",
                    ""path"": ""<Touchscreen>/touch0/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstTouchDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d69b5cc3-8be0-4d6f-8028-b5dd5ca27dcc"",
                    ""path"": ""<Touchscreen>/touch1/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondTouchDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7a70f7b-5709-480d-8507-65917e36c326"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3afd5057-b4f8-49e4-9c46-517335beb48d"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DoubleTapReleased"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UnitSelection"",
            ""id"": ""7d218776-9eb0-4a40-971b-73432cdaf32f"",
            ""actions"": [
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""3e01906a-947c-47c7-b4c1-7167d8d7d485"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FirstTouchInformation"",
                    ""type"": ""Value"",
                    ""id"": ""5812a79a-46a6-481c-a393-aab38029d3d7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""HoldFinger"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9e7f2361-35dd-4cb3-8ec4-ccf4b0c71915"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DoubleTap"",
                    ""type"": ""Button"",
                    ""id"": ""b10f0233-fb56-427e-9479-adb78675b258"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""06209d5c-dcd0-4ce6-a8f2-8e3cbc2d5eb6"",
                    ""path"": ""<Touchscreen>/touch*/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2825c1c7-927b-41a1-921d-5cbed685e295"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e5a2667-5c19-40f5-a601-4548ef7e3e9f"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstTouchInformation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7217caca-862f-4a16-99de-15c37eaf87ee"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HoldFinger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f3c9f80-9c27-4e3f-8136-700726e51720"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DoubleTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Tap"",
            ""id"": ""c3c9095a-2f22-450a-8803-64ca4405e871"",
            ""actions"": [
                {
                    ""name"": ""TapPosition"",
                    ""type"": ""Button"",
                    ""id"": ""cda53d0f-cf92-43f6-a0c5-393aefb1bf24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6077fb0f-aeb0-4ab8-9518-b8e7e6c7a072"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TapPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""114ecd79-0b3e-4c67-9c1c-a1b765f58d0c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TapPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""458c7323-5c73-41d7-b9aa-61b9b8b86d96"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TapPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""e982cca6-3eba-43c5-b257-f50d98e75cfa"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6637bad3-b682-49b6-9bcf-975c30162e92"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7abe295f-c608-46ba-9a49-57e88917a8bb"",
                    ""path"": ""<Touchscreen>/touch1/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32b5df4f-faba-4633-9346-d4919c838dad"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_FirstTouchPosition = m_Player.FindAction("FirstTouchPosition", throwIfNotFound: true);
        m_Player_SecondTouchPosition = m_Player.FindAction("SecondTouchPosition", throwIfNotFound: true);
        m_Player_FirstTouchDelta = m_Player.FindAction("FirstTouchDelta", throwIfNotFound: true);
        m_Player_SecondTouchDelta = m_Player.FindAction("SecondTouchDelta", throwIfNotFound: true);
        m_Player_MouseDelta = m_Player.FindAction("MouseDelta", throwIfNotFound: true);
        m_Player_DoubleTapReleased = m_Player.FindAction("DoubleTapReleased", throwIfNotFound: true);
        // UnitSelection
        m_UnitSelection = asset.FindActionMap("UnitSelection", throwIfNotFound: true);
        m_UnitSelection_Tap = m_UnitSelection.FindAction("Tap", throwIfNotFound: true);
        m_UnitSelection_FirstTouchInformation = m_UnitSelection.FindAction("FirstTouchInformation", throwIfNotFound: true);
        m_UnitSelection_HoldFinger = m_UnitSelection.FindAction("HoldFinger", throwIfNotFound: true);
        m_UnitSelection_DoubleTap = m_UnitSelection.FindAction("DoubleTap", throwIfNotFound: true);
        // Tap
        m_Tap = asset.FindActionMap("Tap", throwIfNotFound: true);
        m_Tap_TapPosition = m_Tap.FindAction("TapPosition", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Move = m_Camera.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_FirstTouchPosition;
    private readonly InputAction m_Player_SecondTouchPosition;
    private readonly InputAction m_Player_FirstTouchDelta;
    private readonly InputAction m_Player_SecondTouchDelta;
    private readonly InputAction m_Player_MouseDelta;
    private readonly InputAction m_Player_DoubleTapReleased;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @FirstTouchPosition => m_Wrapper.m_Player_FirstTouchPosition;
        public InputAction @SecondTouchPosition => m_Wrapper.m_Player_SecondTouchPosition;
        public InputAction @FirstTouchDelta => m_Wrapper.m_Player_FirstTouchDelta;
        public InputAction @SecondTouchDelta => m_Wrapper.m_Player_SecondTouchDelta;
        public InputAction @MouseDelta => m_Wrapper.m_Player_MouseDelta;
        public InputAction @DoubleTapReleased => m_Wrapper.m_Player_DoubleTapReleased;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @FirstTouchPosition.started += instance.OnFirstTouchPosition;
            @FirstTouchPosition.performed += instance.OnFirstTouchPosition;
            @FirstTouchPosition.canceled += instance.OnFirstTouchPosition;
            @SecondTouchPosition.started += instance.OnSecondTouchPosition;
            @SecondTouchPosition.performed += instance.OnSecondTouchPosition;
            @SecondTouchPosition.canceled += instance.OnSecondTouchPosition;
            @FirstTouchDelta.started += instance.OnFirstTouchDelta;
            @FirstTouchDelta.performed += instance.OnFirstTouchDelta;
            @FirstTouchDelta.canceled += instance.OnFirstTouchDelta;
            @SecondTouchDelta.started += instance.OnSecondTouchDelta;
            @SecondTouchDelta.performed += instance.OnSecondTouchDelta;
            @SecondTouchDelta.canceled += instance.OnSecondTouchDelta;
            @MouseDelta.started += instance.OnMouseDelta;
            @MouseDelta.performed += instance.OnMouseDelta;
            @MouseDelta.canceled += instance.OnMouseDelta;
            @DoubleTapReleased.started += instance.OnDoubleTapReleased;
            @DoubleTapReleased.performed += instance.OnDoubleTapReleased;
            @DoubleTapReleased.canceled += instance.OnDoubleTapReleased;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @FirstTouchPosition.started -= instance.OnFirstTouchPosition;
            @FirstTouchPosition.performed -= instance.OnFirstTouchPosition;
            @FirstTouchPosition.canceled -= instance.OnFirstTouchPosition;
            @SecondTouchPosition.started -= instance.OnSecondTouchPosition;
            @SecondTouchPosition.performed -= instance.OnSecondTouchPosition;
            @SecondTouchPosition.canceled -= instance.OnSecondTouchPosition;
            @FirstTouchDelta.started -= instance.OnFirstTouchDelta;
            @FirstTouchDelta.performed -= instance.OnFirstTouchDelta;
            @FirstTouchDelta.canceled -= instance.OnFirstTouchDelta;
            @SecondTouchDelta.started -= instance.OnSecondTouchDelta;
            @SecondTouchDelta.performed -= instance.OnSecondTouchDelta;
            @SecondTouchDelta.canceled -= instance.OnSecondTouchDelta;
            @MouseDelta.started -= instance.OnMouseDelta;
            @MouseDelta.performed -= instance.OnMouseDelta;
            @MouseDelta.canceled -= instance.OnMouseDelta;
            @DoubleTapReleased.started -= instance.OnDoubleTapReleased;
            @DoubleTapReleased.performed -= instance.OnDoubleTapReleased;
            @DoubleTapReleased.canceled -= instance.OnDoubleTapReleased;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UnitSelection
    private readonly InputActionMap m_UnitSelection;
    private List<IUnitSelectionActions> m_UnitSelectionActionsCallbackInterfaces = new List<IUnitSelectionActions>();
    private readonly InputAction m_UnitSelection_Tap;
    private readonly InputAction m_UnitSelection_FirstTouchInformation;
    private readonly InputAction m_UnitSelection_HoldFinger;
    private readonly InputAction m_UnitSelection_DoubleTap;
    public struct UnitSelectionActions
    {
        private @PlayerInput m_Wrapper;
        public UnitSelectionActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tap => m_Wrapper.m_UnitSelection_Tap;
        public InputAction @FirstTouchInformation => m_Wrapper.m_UnitSelection_FirstTouchInformation;
        public InputAction @HoldFinger => m_Wrapper.m_UnitSelection_HoldFinger;
        public InputAction @DoubleTap => m_Wrapper.m_UnitSelection_DoubleTap;
        public InputActionMap Get() { return m_Wrapper.m_UnitSelection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UnitSelectionActions set) { return set.Get(); }
        public void AddCallbacks(IUnitSelectionActions instance)
        {
            if (instance == null || m_Wrapper.m_UnitSelectionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UnitSelectionActionsCallbackInterfaces.Add(instance);
            @Tap.started += instance.OnTap;
            @Tap.performed += instance.OnTap;
            @Tap.canceled += instance.OnTap;
            @FirstTouchInformation.started += instance.OnFirstTouchInformation;
            @FirstTouchInformation.performed += instance.OnFirstTouchInformation;
            @FirstTouchInformation.canceled += instance.OnFirstTouchInformation;
            @HoldFinger.started += instance.OnHoldFinger;
            @HoldFinger.performed += instance.OnHoldFinger;
            @HoldFinger.canceled += instance.OnHoldFinger;
            @DoubleTap.started += instance.OnDoubleTap;
            @DoubleTap.performed += instance.OnDoubleTap;
            @DoubleTap.canceled += instance.OnDoubleTap;
        }

        private void UnregisterCallbacks(IUnitSelectionActions instance)
        {
            @Tap.started -= instance.OnTap;
            @Tap.performed -= instance.OnTap;
            @Tap.canceled -= instance.OnTap;
            @FirstTouchInformation.started -= instance.OnFirstTouchInformation;
            @FirstTouchInformation.performed -= instance.OnFirstTouchInformation;
            @FirstTouchInformation.canceled -= instance.OnFirstTouchInformation;
            @HoldFinger.started -= instance.OnHoldFinger;
            @HoldFinger.performed -= instance.OnHoldFinger;
            @HoldFinger.canceled -= instance.OnHoldFinger;
            @DoubleTap.started -= instance.OnDoubleTap;
            @DoubleTap.performed -= instance.OnDoubleTap;
            @DoubleTap.canceled -= instance.OnDoubleTap;
        }

        public void RemoveCallbacks(IUnitSelectionActions instance)
        {
            if (m_Wrapper.m_UnitSelectionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUnitSelectionActions instance)
        {
            foreach (var item in m_Wrapper.m_UnitSelectionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UnitSelectionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UnitSelectionActions @UnitSelection => new UnitSelectionActions(this);

    // Tap
    private readonly InputActionMap m_Tap;
    private List<ITapActions> m_TapActionsCallbackInterfaces = new List<ITapActions>();
    private readonly InputAction m_Tap_TapPosition;
    public struct TapActions
    {
        private @PlayerInput m_Wrapper;
        public TapActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @TapPosition => m_Wrapper.m_Tap_TapPosition;
        public InputActionMap Get() { return m_Wrapper.m_Tap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TapActions set) { return set.Get(); }
        public void AddCallbacks(ITapActions instance)
        {
            if (instance == null || m_Wrapper.m_TapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TapActionsCallbackInterfaces.Add(instance);
            @TapPosition.started += instance.OnTapPosition;
            @TapPosition.performed += instance.OnTapPosition;
            @TapPosition.canceled += instance.OnTapPosition;
        }

        private void UnregisterCallbacks(ITapActions instance)
        {
            @TapPosition.started -= instance.OnTapPosition;
            @TapPosition.performed -= instance.OnTapPosition;
            @TapPosition.canceled -= instance.OnTapPosition;
        }

        public void RemoveCallbacks(ITapActions instance)
        {
            if (m_Wrapper.m_TapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITapActions instance)
        {
            foreach (var item in m_Wrapper.m_TapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TapActions @Tap => new TapActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private List<ICameraActions> m_CameraActionsCallbackInterfaces = new List<ICameraActions>();
    private readonly InputAction m_Camera_Move;
    public struct CameraActions
    {
        private @PlayerInput m_Wrapper;
        public CameraActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Camera_Move;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void AddCallbacks(ICameraActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(ICameraActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface IPlayerActions
    {
        void OnFirstTouchPosition(InputAction.CallbackContext context);
        void OnSecondTouchPosition(InputAction.CallbackContext context);
        void OnFirstTouchDelta(InputAction.CallbackContext context);
        void OnSecondTouchDelta(InputAction.CallbackContext context);
        void OnMouseDelta(InputAction.CallbackContext context);
        void OnDoubleTapReleased(InputAction.CallbackContext context);
    }
    public interface IUnitSelectionActions
    {
        void OnTap(InputAction.CallbackContext context);
        void OnFirstTouchInformation(InputAction.CallbackContext context);
        void OnHoldFinger(InputAction.CallbackContext context);
        void OnDoubleTap(InputAction.CallbackContext context);
    }
    public interface ITapActions
    {
        void OnTapPosition(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/Input/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerOverworld"",
            ""id"": ""ee0b343c-ac49-41a5-b3a0-377a6da09d17"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""ef3e40b5-3510-4fbb-a0b8-ce769345024f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""49db07ca-c992-4b5a-bb3d-1c01ad60456b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sneak"",
                    ""type"": ""Value"",
                    ""id"": ""9455f502-0243-456d-aa05-7b647b5c65c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""8fbaca3f-23fd-4b88-97cb-640ac6f4f7bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Hit"",
                    ""type"": ""Button"",
                    ""id"": ""f29d4908-292d-4545-82b6-cf8e4b4759f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fa3d8bf4-2214-40dd-a613-f61b50939c37"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9376400b-cd9e-40d8-9a91-eb78ab0eab00"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""72f7a0d6-8daa-495b-aa78-52c93759641e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c5933bf6-7bc2-428c-ae2d-b741eb8007af"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fc1bb73c-7230-4c11-9352-6afe2a476ba5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6eb5e7c2-d867-4af5-b548-ddb0d6355f2c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""69b59ff5-2b92-4caf-bb3e-354e8546871d"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1ea9a23-06a1-4b75-9133-7dd338ff7f3c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c025b5d5-dda6-4d9e-a18a-4add2da1f621"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerOverworld
        m_PlayerOverworld = asset.FindActionMap("PlayerOverworld", throwIfNotFound: true);
        m_PlayerOverworld_Interact = m_PlayerOverworld.FindAction("Interact", throwIfNotFound: true);
        m_PlayerOverworld_Move = m_PlayerOverworld.FindAction("Move", throwIfNotFound: true);
        m_PlayerOverworld_Sneak = m_PlayerOverworld.FindAction("Sneak", throwIfNotFound: true);
        m_PlayerOverworld_Run = m_PlayerOverworld.FindAction("Run", throwIfNotFound: true);
        m_PlayerOverworld_Hit = m_PlayerOverworld.FindAction("Hit", throwIfNotFound: true);
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

    // PlayerOverworld
    private readonly InputActionMap m_PlayerOverworld;
    private IPlayerOverworldActions m_PlayerOverworldActionsCallbackInterface;
    private readonly InputAction m_PlayerOverworld_Interact;
    private readonly InputAction m_PlayerOverworld_Move;
    private readonly InputAction m_PlayerOverworld_Sneak;
    private readonly InputAction m_PlayerOverworld_Run;
    private readonly InputAction m_PlayerOverworld_Hit;
    public struct PlayerOverworldActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerOverworldActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_PlayerOverworld_Interact;
        public InputAction @Move => m_Wrapper.m_PlayerOverworld_Move;
        public InputAction @Sneak => m_Wrapper.m_PlayerOverworld_Sneak;
        public InputAction @Run => m_Wrapper.m_PlayerOverworld_Run;
        public InputAction @Hit => m_Wrapper.m_PlayerOverworld_Hit;
        public InputActionMap Get() { return m_Wrapper.m_PlayerOverworld; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerOverworldActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerOverworldActions instance)
        {
            if (m_Wrapper.m_PlayerOverworldActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnInteract;
                @Move.started -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnMove;
                @Sneak.started -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnSneak;
                @Sneak.performed -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnSneak;
                @Sneak.canceled -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnSneak;
                @Run.started -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnRun;
                @Hit.started -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnHit;
                @Hit.performed -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnHit;
                @Hit.canceled -= m_Wrapper.m_PlayerOverworldActionsCallbackInterface.OnHit;
            }
            m_Wrapper.m_PlayerOverworldActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sneak.started += instance.OnSneak;
                @Sneak.performed += instance.OnSneak;
                @Sneak.canceled += instance.OnSneak;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Hit.started += instance.OnHit;
                @Hit.performed += instance.OnHit;
                @Hit.canceled += instance.OnHit;
            }
        }
    }
    public PlayerOverworldActions @PlayerOverworld => new PlayerOverworldActions(this);
    public interface IPlayerOverworldActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSneak(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnHit(InputAction.CallbackContext context);
    }
}

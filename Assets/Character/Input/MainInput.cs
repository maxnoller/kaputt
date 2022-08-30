//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Character/Input/MainInput.inputactions
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

namespace kaputt.Character.Input
{
    public partial class @MainInput : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MainInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInput"",
    ""maps"": [
        {
            ""name"": ""Foot"",
            ""id"": ""0542d35b-62fc-4cd4-91b9-91366fcc8679"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0f3dea66-3cab-463f-9a13-8260e72e0b8d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""e0b5c130-0c0d-4269-92da-24af679697c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""c81c7de4-b01e-4d49-99bd-1df2e53a183d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a1310f53-fbf8-4c99-9f93-1ced624f4296"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""ca764eef-cde5-490c-a1b5-68877a2302e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryMouse"",
                    ""type"": ""Button"",
                    ""id"": ""6dd9ee4d-4c08-48c4-a27b-aecb9cdf4c4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryMouse"",
                    ""type"": ""Button"",
                    ""id"": ""9e83825e-a5c6-47af-96ce-4c0245e25b22"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""43f665d7-bd4b-4294-b719-7d6065228fe9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5b03a763-d6bd-4fbc-9ae9-f06d54cd57c8"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7259efe3-aaea-497e-95de-ff1cee294594"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7987a42a-dedb-4ba6-abf9-afb94f49e0fc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5801e077-f292-4d3a-9e85-f98ad714ceb1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1ece152b-ad07-4dca-ac35-45fbed515d5c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""50b30c1a-8182-41a0-9548-822faf91bfbd"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44c512c1-2820-439c-b4fa-4ebc2ffcfaae"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20b4ad48-22d6-470b-8fa5-bc2cddf4b233"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8e4e8d4-4fb2-41da-b8e5-c2c58b685c3e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a40c376-7cab-4db8-bb94-db91cafbbbf3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bde8e4fb-0322-4f7c-8846-2248922e976b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b55c3b25-78cd-4eac-a9f1-a5666cba1d3b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Foot
            m_Foot = asset.FindActionMap("Foot", throwIfNotFound: true);
            m_Foot_Movement = m_Foot.FindAction("Movement", throwIfNotFound: true);
            m_Foot_Run = m_Foot.FindAction("Run", throwIfNotFound: true);
            m_Foot_Crouch = m_Foot.FindAction("Crouch", throwIfNotFound: true);
            m_Foot_Look = m_Foot.FindAction("Look", throwIfNotFound: true);
            m_Foot_Jump = m_Foot.FindAction("Jump", throwIfNotFound: true);
            m_Foot_PrimaryMouse = m_Foot.FindAction("PrimaryMouse", throwIfNotFound: true);
            m_Foot_SecondaryMouse = m_Foot.FindAction("SecondaryMouse", throwIfNotFound: true);
            m_Foot_Escape = m_Foot.FindAction("Escape", throwIfNotFound: true);
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

        // Foot
        private readonly InputActionMap m_Foot;
        private IFootActions m_FootActionsCallbackInterface;
        private readonly InputAction m_Foot_Movement;
        private readonly InputAction m_Foot_Run;
        private readonly InputAction m_Foot_Crouch;
        private readonly InputAction m_Foot_Look;
        private readonly InputAction m_Foot_Jump;
        private readonly InputAction m_Foot_PrimaryMouse;
        private readonly InputAction m_Foot_SecondaryMouse;
        private readonly InputAction m_Foot_Escape;
        public struct FootActions
        {
            private @MainInput m_Wrapper;
            public FootActions(@MainInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Foot_Movement;
            public InputAction @Run => m_Wrapper.m_Foot_Run;
            public InputAction @Crouch => m_Wrapper.m_Foot_Crouch;
            public InputAction @Look => m_Wrapper.m_Foot_Look;
            public InputAction @Jump => m_Wrapper.m_Foot_Jump;
            public InputAction @PrimaryMouse => m_Wrapper.m_Foot_PrimaryMouse;
            public InputAction @SecondaryMouse => m_Wrapper.m_Foot_SecondaryMouse;
            public InputAction @Escape => m_Wrapper.m_Foot_Escape;
            public InputActionMap Get() { return m_Wrapper.m_Foot; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(FootActions set) { return set.Get(); }
            public void SetCallbacks(IFootActions instance)
            {
                if (m_Wrapper.m_FootActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_FootActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_FootActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_FootActionsCallbackInterface.OnMovement;
                    @Run.started -= m_Wrapper.m_FootActionsCallbackInterface.OnRun;
                    @Run.performed -= m_Wrapper.m_FootActionsCallbackInterface.OnRun;
                    @Run.canceled -= m_Wrapper.m_FootActionsCallbackInterface.OnRun;
                    @Crouch.started -= m_Wrapper.m_FootActionsCallbackInterface.OnCrouch;
                    @Crouch.performed -= m_Wrapper.m_FootActionsCallbackInterface.OnCrouch;
                    @Crouch.canceled -= m_Wrapper.m_FootActionsCallbackInterface.OnCrouch;
                    @Look.started -= m_Wrapper.m_FootActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_FootActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_FootActionsCallbackInterface.OnLook;
                    @Jump.started -= m_Wrapper.m_FootActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_FootActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_FootActionsCallbackInterface.OnJump;
                    @PrimaryMouse.started -= m_Wrapper.m_FootActionsCallbackInterface.OnPrimaryMouse;
                    @PrimaryMouse.performed -= m_Wrapper.m_FootActionsCallbackInterface.OnPrimaryMouse;
                    @PrimaryMouse.canceled -= m_Wrapper.m_FootActionsCallbackInterface.OnPrimaryMouse;
                    @SecondaryMouse.started -= m_Wrapper.m_FootActionsCallbackInterface.OnSecondaryMouse;
                    @SecondaryMouse.performed -= m_Wrapper.m_FootActionsCallbackInterface.OnSecondaryMouse;
                    @SecondaryMouse.canceled -= m_Wrapper.m_FootActionsCallbackInterface.OnSecondaryMouse;
                    @Escape.started -= m_Wrapper.m_FootActionsCallbackInterface.OnEscape;
                    @Escape.performed -= m_Wrapper.m_FootActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= m_Wrapper.m_FootActionsCallbackInterface.OnEscape;
                }
                m_Wrapper.m_FootActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Run.started += instance.OnRun;
                    @Run.performed += instance.OnRun;
                    @Run.canceled += instance.OnRun;
                    @Crouch.started += instance.OnCrouch;
                    @Crouch.performed += instance.OnCrouch;
                    @Crouch.canceled += instance.OnCrouch;
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @PrimaryMouse.started += instance.OnPrimaryMouse;
                    @PrimaryMouse.performed += instance.OnPrimaryMouse;
                    @PrimaryMouse.canceled += instance.OnPrimaryMouse;
                    @SecondaryMouse.started += instance.OnSecondaryMouse;
                    @SecondaryMouse.performed += instance.OnSecondaryMouse;
                    @SecondaryMouse.canceled += instance.OnSecondaryMouse;
                    @Escape.started += instance.OnEscape;
                    @Escape.performed += instance.OnEscape;
                    @Escape.canceled += instance.OnEscape;
                }
            }
        }
        public FootActions @Foot => new FootActions(this);
        public interface IFootActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnRun(InputAction.CallbackContext context);
            void OnCrouch(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnPrimaryMouse(InputAction.CallbackContext context);
            void OnSecondaryMouse(InputAction.CallbackContext context);
            void OnEscape(InputAction.CallbackContext context);
        }
    }
}
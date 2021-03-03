// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/PlayerInputMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputMap : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputMap"",
    ""maps"": [
        {
            ""name"": ""PlayerTesting"",
            ""id"": ""3f64dd18-adf2-4e8e-b818-b1285d19f2d0"",
            ""actions"": [
                {
                    ""name"": ""DamageNPC"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c5f01d83-cb6d-40df-bdf4-74a34d9a4008"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""RestartScene"",
                    ""type"": ""Button"",
                    ""id"": ""603b9eaf-5bb2-412b-8302-2d0ca6373332"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f9e2486b-2882-4835-8806-7d26ef975706"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControlScheme"",
                    ""action"": ""DamageNPC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e398b0ae-f86b-466d-aff2-8be3bd504c87"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControlScheme"",
                    ""action"": ""RestartScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MenuInteractions"",
            ""id"": ""ec18b7dc-868c-458f-b01f-610c0fb8090d"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlayerControlScheme"",
            ""bindingGroup"": ""PlayerControlScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerTesting
        m_PlayerTesting = asset.FindActionMap("PlayerTesting", throwIfNotFound: true);
        m_PlayerTesting_DamageNPC = m_PlayerTesting.FindAction("DamageNPC", throwIfNotFound: true);
        m_PlayerTesting_RestartScene = m_PlayerTesting.FindAction("RestartScene", throwIfNotFound: true);
        // MenuInteractions
        m_MenuInteractions = asset.FindActionMap("MenuInteractions", throwIfNotFound: true);
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

    // PlayerTesting
    private readonly InputActionMap m_PlayerTesting;
    private IPlayerTestingActions m_PlayerTestingActionsCallbackInterface;
    private readonly InputAction m_PlayerTesting_DamageNPC;
    private readonly InputAction m_PlayerTesting_RestartScene;
    public struct PlayerTestingActions
    {
        private @PlayerInputMap m_Wrapper;
        public PlayerTestingActions(@PlayerInputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @DamageNPC => m_Wrapper.m_PlayerTesting_DamageNPC;
        public InputAction @RestartScene => m_Wrapper.m_PlayerTesting_RestartScene;
        public InputActionMap Get() { return m_Wrapper.m_PlayerTesting; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerTestingActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerTestingActions instance)
        {
            if (m_Wrapper.m_PlayerTestingActionsCallbackInterface != null)
            {
                @DamageNPC.started -= m_Wrapper.m_PlayerTestingActionsCallbackInterface.OnDamageNPC;
                @DamageNPC.performed -= m_Wrapper.m_PlayerTestingActionsCallbackInterface.OnDamageNPC;
                @DamageNPC.canceled -= m_Wrapper.m_PlayerTestingActionsCallbackInterface.OnDamageNPC;
                @RestartScene.started -= m_Wrapper.m_PlayerTestingActionsCallbackInterface.OnRestartScene;
                @RestartScene.performed -= m_Wrapper.m_PlayerTestingActionsCallbackInterface.OnRestartScene;
                @RestartScene.canceled -= m_Wrapper.m_PlayerTestingActionsCallbackInterface.OnRestartScene;
            }
            m_Wrapper.m_PlayerTestingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DamageNPC.started += instance.OnDamageNPC;
                @DamageNPC.performed += instance.OnDamageNPC;
                @DamageNPC.canceled += instance.OnDamageNPC;
                @RestartScene.started += instance.OnRestartScene;
                @RestartScene.performed += instance.OnRestartScene;
                @RestartScene.canceled += instance.OnRestartScene;
            }
        }
    }
    public PlayerTestingActions @PlayerTesting => new PlayerTestingActions(this);

    // MenuInteractions
    private readonly InputActionMap m_MenuInteractions;
    private IMenuInteractionsActions m_MenuInteractionsActionsCallbackInterface;
    public struct MenuInteractionsActions
    {
        private @PlayerInputMap m_Wrapper;
        public MenuInteractionsActions(@PlayerInputMap wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_MenuInteractions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuInteractionsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuInteractionsActions instance)
        {
            if (m_Wrapper.m_MenuInteractionsActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_MenuInteractionsActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public MenuInteractionsActions @MenuInteractions => new MenuInteractionsActions(this);
    private int m_PlayerControlSchemeSchemeIndex = -1;
    public InputControlScheme PlayerControlSchemeScheme
    {
        get
        {
            if (m_PlayerControlSchemeSchemeIndex == -1) m_PlayerControlSchemeSchemeIndex = asset.FindControlSchemeIndex("PlayerControlScheme");
            return asset.controlSchemes[m_PlayerControlSchemeSchemeIndex];
        }
    }
    public interface IPlayerTestingActions
    {
        void OnDamageNPC(InputAction.CallbackContext context);
        void OnRestartScene(InputAction.CallbackContext context);
    }
    public interface IMenuInteractionsActions
    {
    }
}

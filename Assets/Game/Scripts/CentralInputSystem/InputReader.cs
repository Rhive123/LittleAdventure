using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance { get; private set; }
    
    private InputSystem_Actions _input;
    public InputSystem_Actions Input => _input;
    
    // Track current active map
    private InputActionMap _currentActionMap;
    public InputActionMap CurrentActionMap => _currentActionMap;
    
    // Events storage
    private Dictionary<InputAction, Action> _performedEvents = new();
    private Dictionary<InputAction, Action> _canceledEvents = new();
    
    // Continuous inputs
    public Vector2 MoveInput { get; private set; }
    public Vector2 NavigateInput { get; private set; }
    
    // Event when action map changes
    public event Action<InputActionMap> OnActionMapChanged;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        _input = new InputSystem_Actions();
    }
    
    void Start()
    {
        // Default to Player input
        SwitchActionMap(_input.Player);
    }
    
    void Update()
    {
        // Poll continuous inputs based on current map
        if (_currentActionMap == _input.Player.Get())
            MoveInput = _input.Player.Move.ReadValue<Vector2>();
        else if (_currentActionMap == _input.UI.Get())
            NavigateInput = _input.UI.Navigate.ReadValue<Vector2>();
    }
    
    void OnDestroy()
    {
        if (_currentActionMap != null)
            DisableActionMap(_currentActionMap);
    }
    
    // =====================
    // ACTION MAP SWITCHING
    // =====================
    
    public void SwitchActionMap(InputActionMap newMap)
    {
        if (_currentActionMap == newMap) return;
        
        if (_currentActionMap != null)
            DisableActionMap(_currentActionMap);
        
        EnableActionMap(newMap);
        _currentActionMap = newMap;
        
        OnActionMapChanged?.Invoke(newMap);
        Debug.Log($"Switched to action map: {newMap.name}");
    }
    
    // Convenience methods
    public void SwitchToPlayer() => SwitchActionMap(_input.Player); // Use .Get if does not work
    public void SwitchToUI() => SwitchActionMap(_input.UI);
    public void SwitchToCutscene() => SwitchActionMap(_input.Cutscene);
    public void SwitchToDialogue() => SwitchActionMap(_input.Dialogue);
    
    public void DisableAllInput()
    {
        if (_currentActionMap != null)
            DisableActionMap(_currentActionMap);
        _currentActionMap = null;
        Debug.Log("All input disabled");
    }
    
    // =====================
    // INTERNAL MAP HANDLING
    // =====================
    
    private void EnableActionMap(InputActionMap actionMap)
    {
        foreach (var action in actionMap.actions)
        {
            action.performed += OnActionPerformed;
            action.canceled += OnActionCanceled;
        }
        actionMap.Enable();
    }
    
    private void DisableActionMap(InputActionMap actionMap)
    {
        foreach (var action in actionMap.actions)
        {
            action.performed -= OnActionPerformed;
            action.canceled -= OnActionCanceled;
        }
        actionMap.Disable();
    }
    
    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        if (_performedEvents.TryGetValue(ctx.action, out var callback))
            callback?.Invoke();
    }
    
    private void OnActionCanceled(InputAction.CallbackContext ctx)
    {
        if (_canceledEvents.TryGetValue(ctx.action, out var callback))
            callback?.Invoke();
    }
    
    // =====================
    // SUBSCRIPTION METHODS
    // =====================
    
    public void SubscribePerformed(InputAction action, Action callback)
    {
        if (!_performedEvents.ContainsKey(action))
            _performedEvents[action] = null;
        _performedEvents[action] += callback;
    }
    
    public void UnsubscribePerformed(InputAction action, Action callback)
    {
        if (_performedEvents.ContainsKey(action))
            _performedEvents[action] -= callback;
    }
    
    public void SubscribeCanceled(InputAction action, Action callback)
    {
        if (!_canceledEvents.ContainsKey(action))
            _canceledEvents[action] = null;
        _canceledEvents[action] += callback;
    }
    
    public void UnsubscribeCanceled(InputAction action, Action callback)
    {
        if (_canceledEvents.ContainsKey(action))
            _canceledEvents[action] -= callback;
    }
    
    // =====================
    // HELPER METHODS
    // =====================
    
    public bool IsActionMapActive(InputActionMap map) => _currentActionMap == map;
    public bool IsPlayerMapActive() => _currentActionMap == _input.Player.Get();
    public bool IsUIMapActive() => _currentActionMap == _input.UI.Get();
    public bool IsCutsceneMapActive() => _currentActionMap == _input.Cutscene.Get();
    public bool IsDialogueMapActive() => _currentActionMap == _input.Dialogue.Get();
}
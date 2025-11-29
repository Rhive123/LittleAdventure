using UnityEngine;
using UnityEngine.InputSystem;

public class InputListenerPattern : MonoBehaviour
{
    void OnEnable()
    {
        InputReader.Instance.OnActionMapChanged += HandleActionMapChanged;
        
        SubscribeToCurrentMap();
    }
    
    void OnDisable()
    {
        InputReader.Instance.OnActionMapChanged -= HandleActionMapChanged;
        
        UnsubscribeAll();
    }
    
    void HandleActionMapChanged(InputActionMap newMap)
    {
        UnsubscribeAll();
        SubscribeToCurrentMap();
    }
    
    void SubscribeToCurrentMap()
    {
        // Subscribe based on current map
        if (InputReader.Instance.IsPlayerMapActive())
        {
            Debug.Log("Subscribe to Player: YourAction");
            InputReader.Instance.SubscribePerformed(
                InputReader.Instance.Input.Player.Attack,
                HandleYourAction
            );
        }
    }
    
    void UnsubscribeAll()
    {
        InputReader.Instance.UnsubscribePerformed(
            InputReader.Instance.Input.Player.Attack,
            HandleYourAction
        );
    }
    
    void HandleYourAction()
    {
        Debug.Log("YourAction triggered!");
    }
}
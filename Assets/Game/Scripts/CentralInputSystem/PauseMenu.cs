using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    
    private bool _isPaused;
    
    void OnEnable()
    {
        InputReader.Instance.OnActionMapChanged += HandleActionMapChanged;
        
        // Subscribe based on current map
        SubscribeToCurrentMap();
    }
    
    void OnDisable()
    {
        InputReader.Instance.OnActionMapChanged -= HandleActionMapChanged;
        
        // Unsubscribe from all
        UnsubscribeAll();
    }
    
    void HandleActionMapChanged(InputActionMap newMap)
    {
        UnsubscribeAll();
        SubscribeToCurrentMap();
    }
    
    void SubscribeToCurrentMap()
    {
        if (InputReader.Instance.IsPlayerMapActive())
        {
            Debug.Log("Subscribe to Player: Pause");
            InputReader.Instance.SubscribePerformed(
                InputReader.Instance.Input.Player.Pause,
                HandlePause
            );
        }
        else if (InputReader.Instance.IsUIMapActive())
        {
            Debug.Log("Subscribe to UI: Unpause");
            InputReader.Instance.SubscribePerformed(
                InputReader.Instance.Input.UI.Unpause,
                HandleResume
            );
        }
    }
    
    void UnsubscribeAll()
    {
        InputReader.Instance.UnsubscribePerformed(
            InputReader.Instance.Input.Player.Pause,
            HandlePause
        );
        
        InputReader.Instance.UnsubscribePerformed(
            InputReader.Instance.Input.UI.Cancel,
            HandleResume
        );
    }
    
    void HandlePause()
    {
        if (_isPaused) return;
        
        Debug.Log("Paused");
        _isPaused = true;
        //pausePanel.SetActive(true);
        Time.timeScale = 0f;
        InputReader.Instance.SwitchToUI();
    }
    
    void HandleResume()
    {
        if (!_isPaused) return;
        
        Debug.Log("Resumed");
        _isPaused = false;
        //pausePanel.SetActive(false);
        Time.timeScale = 1f;
        InputReader.Instance.SwitchToPlayer();
    }
}
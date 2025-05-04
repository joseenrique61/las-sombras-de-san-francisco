using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject MenuPanel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ToggleMenuPanel(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started) return;

        if(MenuPanel != null)
        {
            MenuPanel.SetActive(!MenuPanel.activeSelf);
        }
    }
}

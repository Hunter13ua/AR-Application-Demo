using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIStateManager : MonoBehaviour
{
    public enum UIState
    {
        InitialScreen,
        ARScreen
    }

    [SerializeField] private UIState currentState = UIState.InitialScreen;
    [SerializeField] private List<UIPanel> panels = new();

    [NonSerialized] public UnityEvent<UIState> OnStateChanged;

    public UIState CurrentState => currentState;

    private void Awake()
    {
        InitializePanels();
        UpdatePanelsForCurrentState();
    }

    private void InitializePanels()
    {
        foreach (var panel in panels)
        {
            panel?.Initialize();
        }
    }

    private void UpdatePanelsForCurrentState()
    {
        // Hide all panels first
        foreach (var panel in panels)
        {
            panel?.Hide();
        }

        // Show the panel for current state
        foreach (var panel in panels)
        {
            if (panel != null && panel.PanelState == currentState)
            {
                panel.Show();
                break;
            }
        }
    }

    public void SetState(UIState newState)
    {
        if (currentState == newState) return;

        UIState previousState = currentState;
        currentState = newState;

        UpdatePanelsForCurrentState();
        OnStateChanged?.Invoke(currentState);
    }
}

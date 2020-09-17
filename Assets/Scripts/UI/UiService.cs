using System;
using System.Collections;
using System.Collections.Generic;
using ProjectX;
using UnityEngine;

public class UiService : MonoBehaviour
{
    [SerializeField] private UiSources UiSources;

    private Camera camera;

    private BaseWindow        _currentWindow;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void OnEnable()
    {
        InputHandler.CancelButtonClickEvent += OnCancelClick;
    }

    private void OnDisable()
    {
        InputHandler.CancelButtonClickEvent -= OnCancelClick;
    }

    private void OnDestroy()
    {
        InputHandler.CancelButtonClickEvent -= OnCancelClick;
        if (_currentWindow != null)
        {
            _currentWindow.CancelEvent -= OnCancelClick;
        }
    }

    public void sssss()
    {
        NotificationManager.Instance.SetNotification("О бла работает!");
    }

    public void ShowInventoryWindow()
    {
        var inventoryWindow = InitWindow(UiSources.InventoryUi);
        if (inventoryWindow != null)
        {
            inventoryWindow.Show();
        }
    }

    private void OnTransitionRoute(string route)
    {
        var window = UiSources.MenuWindow;
        switch (route)
        {
            case "settings":
                window = UiSources.MenuWindow;
                break;
                ;
            default:
                window = UiSources.MenuWindow;
                break;
        }

        TransitionToWindow(InitWindow(window));
    }

    private void TransitionToWindow(BaseWindow nextWindow)
    {
        ChangeCurrentWindow(nextWindow);
        nextWindow.Show();
        ChangeUiMode(false, true, nextWindow.IsStoppedTime);
    }

    public void ChangeUiMode(bool gameplayEnabled = true, bool? cursorVisability = null, bool stopTime = true)
    {
        Time.timeScale = stopTime ? 0f : 1f;
    }

    private void ChangeCurrentWindow(BaseWindow window)
    {
        if (_currentWindow != null)
            _currentWindow.CancelEvent -= OnCancelClick;
        _currentWindow = window;
        if (window != null)
            window.CancelEvent += OnCancelClick;
    }
    
    private void OnCancelClick()
    {
        if (_currentWindow)
        {
            _currentWindow.Clear();
            ChangeUiMode(true, true, false);
        }
        else
        {
            var inventoryWindow = InitWindow(UiSources.MenuWindow);
        }
    }

    private T InitWindow<T>(T prefab) where T : BaseWindow
    {
        if (_currentWindow == prefab || (_currentWindow != null && _currentWindow.name.Contains("MainMenu")))
        {
            return null;
        }

        if (_currentWindow != null)
        {
            _currentWindow.CancelEvent -= OnCancelClick;
            _currentWindow.Clear();
        }
        
        var window = Instantiate(prefab, transform);
        _currentWindow = window;
        _currentWindow.CancelEvent += OnCancelClick;
        ChangeUiMode(false, true);
        return window;
    }
}

[Serializable]
public class UiSources
{
    public SImpleInventoryUI InventoryUi;
    public BaseWindow MenuWindow;
} 
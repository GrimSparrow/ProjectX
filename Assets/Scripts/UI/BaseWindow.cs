using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWindow : MonoBehaviour
{
    public static Action<BaseWindow> Transition;
    public static Action<string> TransitionRoute;
    public static Action<BaseWindow, object> TransitionA;

    public bool IsStoppedTime = true;

    public event Action CancelEvent;

    public virtual void Show()
    {
        gameObject.SetActive(true);
        UpdateInfo();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Clear()
    {
        Destroy(gameObject);
    }

    public virtual void UpdateInfo()
    {
    }

    public virtual void OnClick(string windowRoute)
    {
        TransitionRoute?.Invoke(windowRoute);
    }

    public virtual void OnClick(BaseWindow nextScreen)
    {
        Transition?.Invoke(nextScreen);
    }

    public virtual void CancelClick()
    {
        CancelEvent?.Invoke();
    }

    protected virtual void RemoveListeners()
    {
    }

    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }
}

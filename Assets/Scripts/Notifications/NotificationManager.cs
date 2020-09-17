using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private TextMeshProUGUI fadeText;
    private static  NotificationManager instance;
    public  static  NotificationManager Instance
    {
        get => instance;
        set => instance = value;
    }
    void Start()
    {
        if (Instance == null) 
        { 
            Instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator notificationCoroutine;

    public void SetNotification(string message)
    {
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }

        notificationCoroutine = FadeOutNotification(message);
        StartCoroutine(notificationCoroutine);
    }

    private IEnumerator FadeOutNotification(string message)
    {
        fadeText.SetText(message);
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            fadeText.color = new Color(
                fadeText.color.r,
                fadeText.color.g,
                fadeText.color.b,
                Mathf.Lerp(1f,0f,t/fadeTime));
            yield return null;
        }
    }
}

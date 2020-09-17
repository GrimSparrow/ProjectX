using System;
using System.Collections;
using UnityEngine;

public class LockButtonClicked : MonoBehaviour
{
    public static event Action<int> Pressed = delegate {  };
    
    [SerializeField] private int index;
    [SerializeField] private float destination;
    private bool isPressed = false;

    private void Start()
    {
        isPressed = false;
        LockControl.IncorrectCombination += OnInvorrectCombination;
    }

    private void OnDestroy()
    {
        LockControl.IncorrectCombination -= OnInvorrectCombination;
    }

    private void OnMouseDown()
    {
        if (!isPressed)
        {
            StartCoroutine("Press");
        }
    }

    private IEnumerator Press()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(0f, 0f,destination);
            yield return new WaitForSeconds(0.01f);
        }

        isPressed = true;
        Pressed(index);
    }

    public void OnInvorrectCombination()
    {
        StartCoroutine("Release");
    }
    
    private IEnumerator Release()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(0f, 0f,-destination);
            yield return new WaitForSeconds(0.01f);
        }

        isPressed = false;
    }
}

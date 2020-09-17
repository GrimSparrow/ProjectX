using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    public static event Action IncorrectCombination = delegate {};
    [SerializeField] private List<int> correctCombination;
    [SerializeField] private List<int> result;
    private void Start()
    {
        LockButtonClicked.Pressed += CheckResult;
    }

    private void OnDestroy()
    {
        LockButtonClicked.Pressed -= CheckResult;
    }

    private void CheckResult(int index)
    {
        result.Add(index);
        if (result.Count == correctCombination.Count)
        {
            var isCorrect = result.SequenceEqual(correctCombination);
            if (isCorrect)
            {
                Debug.Log("Opened");
            }
            else
            {
                IncorrectCombination.Invoke();
            }
        }
    }
}

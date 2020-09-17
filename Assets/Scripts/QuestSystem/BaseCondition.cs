using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Create Condition")]
public class BaseCondition : ScriptableObject
{
    public bool IsPass()
    {
        Debug.Log("Passed");
        return true;
    }
}

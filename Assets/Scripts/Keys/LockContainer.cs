using ProjectX;
using UnityEngine;

public abstract class BaseLockContainer : MonoBehaviour
{
    public InventoryController inventory;
    
    public void Start()
    {
        inventory = InventoryController.Instance;
    }

    public virtual bool CanOpen()
    {
        return true;
    }
}
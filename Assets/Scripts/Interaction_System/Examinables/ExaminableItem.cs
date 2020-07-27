using Lowscope.Saving.Components;
using UnityEngine;

public class ExaminableItem : ExaminableBase
{
    public int maxCount;
    public string description;
    public string itemName;
    public Sprite icon;
    public override void Use()
    {
        var container = GetComponent<Saveable>();
        if (container != null)
        {
            InventoryController.Instance.AddItem(container);
        }
        
    }

    public override ExaminableBase Prepare()
    {
        var collider = GetComponent<Collider>();
        var rigid = GetComponent<Rigidbody>();
        if (collider != null )
        {
            collider.enabled = false;
        }
        if (rigid != null )
        {
            Destroy(rigid);
        }

        return this;
    }
}

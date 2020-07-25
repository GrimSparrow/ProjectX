using UnityEngine;

public class PickExaminable : ExaminableBase
{
    public override void Use()
    {
        Debug.Log($"{ObjectName} is picked up");
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

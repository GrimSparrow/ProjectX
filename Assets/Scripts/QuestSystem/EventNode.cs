using UnityEngine.Events;
using XNode;

[NodeTint("#FFFFAA")]
public class EventNode : TaskBaseNode
{
    public UnityEvent[] triggers;

    public override void Trigger()
    {
        foreach (var trigger in triggers)
        {
            trigger.Invoke();
        }
        
        NodePort port = null;
        port = GetOutputPort("output");
        if (port == null) return;
        for (int i = 0; i < port.ConnectionCount; i++)
        {
            NodePort connection = port.GetConnection(i);
            (connection.node as TaskBaseNode)?.Trigger();
        }
    }
}

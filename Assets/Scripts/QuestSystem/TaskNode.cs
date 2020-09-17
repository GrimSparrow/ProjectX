using System;
using UnityEngine;
using XNode;

[Serializable]
[NodeTintAttribute("#CCFFCC")]
public class TaskNode : TaskBaseNode
{
    
    [TextArea] 
    [Header("Description")]
    public string Description;
    [TextArea] 
    [Header("HelpText")]
    public string HelpText;


    public void TryToComplete()
    {
        NodePort port = null;
        port = GetOutputPort("output");

        if (port == null) return;
        for (int i = 0; i < port.ConnectionCount; i++)
        {
            NodePort connection = port.GetConnection(i);
            (connection.node as TaskBaseNode)?.Trigger();
        }
    }
    
    public override void Trigger()
    {
        (graph as Quest).current = this;
    }

}

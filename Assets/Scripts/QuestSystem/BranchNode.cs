using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#CCCCFF")]
public class BranchNode : TaskBaseNode
{
    public BaseCondition[] conditions;
    
    [Output] public TaskBaseNode pass;
    [Output] public TaskBaseNode fail;
    
    private bool success;

    public override void Trigger() {
        // Perform condition
        bool success = true;
        for (int i = 0; i < conditions.Length; i++) {
            if (conditions[i].IsPass()) continue;
            success = false;
            break;
        }

        //Trigger next nodes
        NodePort port;
        if (success) port = GetOutputPort("pass");
        else port = GetOutputPort("fail");
        if (port == null) return;
        for (int i = 0; i < port.ConnectionCount; i++) {
            NodePort connection = port.GetConnection(i);
            (connection.node as TaskBaseNode).Trigger();
        }
    }
}

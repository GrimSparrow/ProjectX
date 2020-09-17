using System.Linq;
using UnityEngine;
using XNode;

[CreateAssetMenu(menuName = "Quest/Create Quest", order = 0)]
public class Quest : NodeGraph
{
    [HideInInspector] public TaskBaseNode current;
    public void Restart() {
        current = nodes.Find(x => x is TaskBaseNode && x.Inputs.All(y => !y.IsConnected)) as TaskBaseNode;
    }
}

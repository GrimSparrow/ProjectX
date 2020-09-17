using XNode;

public abstract class TaskBaseNode : Node
{
    [Input(backingValue = ShowBackingValue.Never)] public TaskBaseNode input;
    [Output(backingValue = ShowBackingValue.Never)] public TaskBaseNode output;

    public abstract void Trigger();
    public override object GetValue(NodePort port)
    {
        return null;
    }
}

using UnityEditor;
using XNodeEditor;

#if UNITY_EDITOR
[CustomNodeEditor(typeof(BranchNode))]
public class BranchNodeEditor : NodeEditor
{
    public override void OnBodyGUI() {
        serializedObject.Update();

        BranchNode node = target as BranchNode;
        NodeEditorGUILayout.PortField(target.GetInputPort("input"));
        EditorGUILayout.Space();
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("conditions"));
        NodeEditorGUILayout.PortField(target.GetOutputPort("pass"));
        NodeEditorGUILayout.PortField(target.GetOutputPort("fail"));

        serializedObject.ApplyModifiedProperties();
    }

    public override int GetWidth() {
        return 336;
    }
}
#endif
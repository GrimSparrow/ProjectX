using UnityEditor;
using UnityEngine;
using XNodeEditor;

#if UNITY_EDITOR
[CustomNodeEditor(typeof(EventNode))]
public class EventNodeEditor : NodeEditor
{
    public override void OnBodyGUI() {
        serializedObject.Update();

        EventNode node = target as EventNode;
        NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
        NodeEditorGUILayout.PortField(target.GetOutputPort("output"));
        EditorGUILayout.Space();
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("triggers"));

        serializedObject.ApplyModifiedProperties();
    }

    public override int GetWidth() {
        return 336;
    }
}
#endif
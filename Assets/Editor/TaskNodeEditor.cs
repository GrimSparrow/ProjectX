using UnityEngine;
using XNodeEditor;

#if UNITY_EDITOR
[CustomNodeEditor(typeof(TaskNode))]
public class TaskNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();
        TaskNode node = target as TaskNode;
        
            GUILayout.BeginHorizontal();
            
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
            GUILayout.EndHorizontal();
        
        GUILayout.Space(-30);
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Description"), GUIContent.none);
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("HelpText"), GUIContent.none);
        node.name = serializedObject.FindProperty("Description").stringValue;
        
        serializedObject.ApplyModifiedProperties();
    }

    public override int GetWidth()
    {
        return 300;
    }
}
#endif

using System;
using UnityEngine;
using XNodeEditor;

#if UNITY_EDITOR
[NodeEditor.CustomNodeEditorAttribute(typeof(Quest))]
public class QuestEditor : NodeGraphEditor
{
   public override string GetNodeMenuName(Type type)
   {
      return base.GetNodeMenuName(type).Replace("Quest/","");
   }
}
#endif
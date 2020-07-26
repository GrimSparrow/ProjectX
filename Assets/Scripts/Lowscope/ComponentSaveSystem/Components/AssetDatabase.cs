using Lowscope.Saving.Components;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CanEditMultipleObjects]
public class GuidGenerator : EditorWindow
{
    [MenuItem("Tools/GenerateGuids/Generate Guid for all PrefabSources in resources")]
    public static void GenerateGuidsForPrefabSource()
    {
        var sources = Resources.FindObjectsOfTypeAll<PrefabSourceContainer>();
        if (sources == null || sources.Length == 0)
        {
            Debug.Log("PrefabSourceContainer objects not found in resources");
            return;
        }

        foreach (var s in sources)
        {
            s.GenerateToolsProperties();
            //EditorUtility.SetDirty(s);
        }
    }
}
#endif
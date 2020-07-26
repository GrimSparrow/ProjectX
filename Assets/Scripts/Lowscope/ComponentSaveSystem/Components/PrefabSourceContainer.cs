using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewPrefabsSource", menuName = "Serialization/PrefabSource")]
public class PrefabSourceContainer : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private string _guid;

    [FormerlySerializedAs("_path")] [SerializeField]
    private string _containerPath;

    public GameObject Prefab => _prefab;
    public string PrefabGuid => _guid;
    public string PathSourceContainer => _containerPath;

#if UNITY_EDITOR
    public void GenerateToolsProperties()
    {
        string previewGuid = _guid;
        long local;
        if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(_prefab.GetInstanceID(), out _guid, out local))
        {
            Debug.LogError($"Exception on trying get guid of prefab {_prefab}");
        }

        _containerPath = AssetDatabase.GetAssetPath(GetInstanceID());
        if (!string.IsNullOrEmpty(_containerPath))
        {
            _containerPath = _containerPath.Substring(17);
            _containerPath = _containerPath.Remove(_containerPath.Length - 6);
        }

        if (!string.IsNullOrEmpty(previewGuid) && previewGuid != _guid)
        {
            Debug.LogWarning($"Guid of prefab was changed. Preview: {previewGuid} \n Path: {_containerPath} ");
            EditorUtility.SetDirty(this);
        }
    }
#endif
}
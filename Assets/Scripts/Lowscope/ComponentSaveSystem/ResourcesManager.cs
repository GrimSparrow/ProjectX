
using UnityEngine;

public static class ResourcesManager 
{
    public static PrefabSourceContainer LoadSourceContainerByPathFromResources(string path)
    {
        var result = Resources.Load<PrefabSourceContainer>(path);

        return result;
    }
}

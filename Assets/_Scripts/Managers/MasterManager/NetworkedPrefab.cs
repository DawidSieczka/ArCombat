using UnityEngine;

[System.Serializable]
public class NetworkedPrefab
{
    public GameObject Prefab;
    public string Path;

    private string _resourcesDirectory = "resources";

    public NetworkedPrefab(GameObject obj, string path)
    {
        Prefab = obj;
        Path = GetCorrectedPrefabPath(path);
    }

    public string GetCorrectedPrefabPath(string basePath)
    {
        int extensionLength = System.IO.Path.GetExtension(basePath).Length;
        int additionalDirectoryLength = _resourcesDirectory.Length + 1; // 1 == '/'
        int startIndex = basePath.ToLower().IndexOf(_resourcesDirectory);

        if (startIndex == -1)
            return string.Empty;
        else
            return basePath.Substring(startIndex + additionalDirectoryLength, basePath.Length - (additionalDirectoryLength + startIndex + extensionLength));
    }
}
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

#if UNITY_EDITOR

using UnityEditor;

#endif

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }

    [SerializeField]
    private GameSettings _gameSettings;

    [SerializeField]
    private List<NetworkedPrefab> _networkedPrefabs = new List<NetworkedPrefab>();

    public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        foreach (var networkedPrefab in Instance._networkedPrefabs)
        {
            if (networkedPrefab.Prefab == obj)
            {
                if (networkedPrefab.Path != string.Empty)
                {
                    return PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                }
                else
                {
                    Debug.LogError($"Paht is empty for gameobject name: {networkedPrefab.Prefab.name}");
                }
            }
        }

        return null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PopulateNetworkedPrefabs()
    {
#if UNITY_EDITOR

        Instance._networkedPrefabs.Clear();

        GameObject[] results = Resources.LoadAll<GameObject>("Init");
        foreach (var result in results)
        {
            if (result.GetComponent<PhotonView>() != null)
            {
                var path = AssetDatabase.GetAssetPath(result);
                var newPrefab = new NetworkedPrefab(result, path);
                Instance._networkedPrefabs.Add(newPrefab);
                Debug.Log($"Instantiated: {newPrefab.Prefab.name}, {newPrefab.Path}");
            }
        }
#endif
    }
}
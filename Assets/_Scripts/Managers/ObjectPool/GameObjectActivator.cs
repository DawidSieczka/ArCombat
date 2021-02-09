using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void InitIfNotExists()
    {
        if (_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
    }

    public void Disactivate()
    {
        InitIfNotExists();
        _meshRenderer.enabled = false;
    }

    public void Activate()
    {
        InitIfNotExists();
        _meshRenderer.enabled = true;
    }
}
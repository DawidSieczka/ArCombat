using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private LineRenderer _line;
    [SerializeField] private float _reflectDistance = 1f;
    public Camera camera;
    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.SetVertexCount(2);
    }

    private void Update()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        var screenCenter = new Vector3(x, y,transform.position.z);
        Ray ray = new Ray(camera.ScreenToWorldPoint(screenCenter), transform.position);
        var hit = Physics.RaycastAll(transform.position, ray.origin, 100);
        if (hit !=null)
        {
            _line.SetPositions(new[] { transform.position, ray.origin });
        }
    }
}

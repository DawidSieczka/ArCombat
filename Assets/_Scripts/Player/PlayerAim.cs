using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        int screenX = Screen.width / 2;
        int screenY = Screen.height / 2;
        var screenCenter = new Vector3(screenX, screenY,transform.position.z);
        Ray ray = new Ray(camera.ScreenToWorldPoint(screenCenter), transform.position);
        RaycastHit[] hit = Physics.RaycastAll(transform.position, ray.origin, 100);
        
        if (hit !=null)
        {
            Debug.DrawRay(screenCenter, transform.position, Color.red,10000);
            //_line.SetPositions(new[] { transform.position, ray.origin });
        }
    }
}

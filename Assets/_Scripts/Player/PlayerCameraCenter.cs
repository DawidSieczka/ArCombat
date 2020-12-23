using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraCenter : MonoBehaviour
{
    public void SetDepthToPlayersPosition(bool isZDepthAxis, float xDepth, float zDepth)
    {
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);

        if (isZDepthAxis)
            transform.position = new Vector3(transform.position.x, transform.position.y, zDepth);
        else
            transform.position = new Vector3(xDepth, transform.position.y, transform.position.z);
    }
}

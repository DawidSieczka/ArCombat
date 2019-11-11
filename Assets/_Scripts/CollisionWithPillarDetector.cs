using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithPillarDetector : MonoBehaviour
{
    private PillarManager _pillarManager;

    private static bool _readyToUse = true;

    private void Start()
    {
        _pillarManager = GameObject.FindGameObjectWithTag(Tag.GameManager.ToString())
            .GetComponent<PillarManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_pillarManager.WantRotate)
        {
            if (other.CompareTag(Tag.PillarTag.ToString()))
            {
                if (gameObject.CompareTag(Tag.RotateRight.ToString()))
                {
                    _readyToUse = false;
                    _pillarManager.AngleRotate = -90;
                }
                else if (gameObject.CompareTag(Tag.RotateLeft.ToString()))
                {
                    _readyToUse = false;
                    _pillarManager.AngleRotate = 90;
                }
                else
                {
                    _pillarManager.AngleRotate = 0;
                }
            }
        }
    }
}
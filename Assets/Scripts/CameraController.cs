using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target = null;

    private float maxZ = 0;

    private void LateUpdate()
    {
        var pos = target.position;
        pos.x = 0;
        pos.y = 0;

        if (pos.z > maxZ)
        {
            transform.position = pos;
            maxZ = pos.z;
        }

    }
}

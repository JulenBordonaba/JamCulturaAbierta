using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        Vector3 aux = target.position;
        aux.y = transform.position.y;
        transform.position = aux;
    }
}

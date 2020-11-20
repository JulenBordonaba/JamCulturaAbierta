using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MatrixBlender))]
public class PerspectiveSwitcher : MonoBehaviour
{
    private Matrix4x4 ortho,
                        perspective;
    public float fov = 60f,
                        near = .3f,
                        far = 1000f,
                        orthographicSize = 50f;
    private float aspect;
    private MatrixBlender blender;
    private bool orthoOn;

    void Start()
    {
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        blender = (MatrixBlender)GetComponent(typeof(MatrixBlender));
        blender.myCamera.projectionMatrix = perspective;
        orthoOn = false;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SwitchPerspective(1.5f,);
    //    }
    //}

    public void SwitchPerspective(float os, float t)
    {
        orthoOn = !orthoOn;
        ortho = Matrix4x4.Ortho(-os * aspect, os * aspect, -os, os, near, far);
        if (orthoOn)
        {
            blender.BlendToMatrix(ortho, t);
        }
        else
            blender.BlendToMatrix(perspective, t);
    }
}

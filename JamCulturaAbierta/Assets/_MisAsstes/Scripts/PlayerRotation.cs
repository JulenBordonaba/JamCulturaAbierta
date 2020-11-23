using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour, IPlayer
{
    public Transform xRotationTransform;
    public Transform yRotationTransform;

    [Range(0,-180)]
    public float minX = -50;
    [Range(0,180)]
    public float maxX = 70;

    public Vector2 sensitivity;

    private bool isPlayerPaused = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (IsPaused) return;
        HandleInput();
    }

    private void HandleInput()
    {
        float xRot = -Input.GetAxis("Mouse Y") * OptionsMenu.settings.sensitivity.y * 2;
        float yRot = Input.GetAxis("Mouse X") * OptionsMenu.settings.sensitivity.x * 2;

        ApplyRotation(xRot, yRot);

    }

    public void EnterPainting()
    {
        isPlayerPaused = true;
    }

    public void ExitPainting()
    {
        isPlayerPaused = false;
    }

    private void ApplyRotation(float x, float y)
    {
        float finalX = xRotationTransform.localEulerAngles.x + x;
        float xRot = x;
        float dif = 0;

        if(finalX>180)
        {
            finalX -= 360;
        }

        if(finalX< minX)
        {
            dif = finalX - minX;
            xRot = x - dif;
        }
        if (finalX > maxX)
        {
            dif = finalX - maxX;
            xRot = x - dif;
        }



        xRotationTransform.Rotate(new Vector3(xRot, 0, 0) * Time.deltaTime * Application.targetFrameRate);
        yRotationTransform.Rotate(new Vector3(0, y, 0) * Time.deltaTime * Application.targetFrameRate);
        

    }

    public bool IsPaused
    {
        get
        {
            return isPlayerPaused || PauseManager.Instance.onPause;
        }
    }
}

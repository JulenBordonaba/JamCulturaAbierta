using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadro : MonoBehaviour, IInteractable
{
    public static Cuadro current;
    public Transform cameraPivot;
    public Transform cameraTarget;
    public CinemachineVirtualCamera travellCamera;
    public float orthographicSize = 1.5f;

    public Transform spawnPosition;

    public Transform inPaintObjects;

    public Cuadro pair;

    public bool inMenu = false;

    private void Awake()
    {
        if(!inMenu)
        inPaintObjects.gameObject.SetActive(false);
    }

    //private void Update()
    //{
    //    if (current != this) return;
    //    if(Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Exit();
    //    }
    //}


    public void Travel()
    {
        CameraManager.Instance.EnterPainting();
    }

    public void Interact()
    {
        current = this;


        CameraManager.Instance.LookPainting();
    }

    public void Exit()
    {
        current.inPaintObjects.gameObject.SetActive(false);
        CameraManager.Instance.ReturnToPlayer();
    }
}

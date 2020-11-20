using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadro : MonoBehaviour, IInteractable
{
    public static Cuadro current;
    public Transform cameraPivot;
    public Transform cameraTarget;
    public float orthographicSize = 1.5f;

    public Transform inPaintObjects;

    public Cuadro pair;


    public Dictionary<int, Color> colors = new Dictionary<int, Color>()
    {
        {0,Color.blue },
        {1,Color.red },
        {2,Color.green },
        {3,Color.yellow },
    };

    private void Awake()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinimap : MonoBehaviour, IPlayer
{

    public static bool canUseMap = false;

    public GameObject mapObject;

    public AudioSource mapSound;

    private bool isPlayerPaused;

    [HideInInspector]
    public bool isMapOpen = false;
    

    private void Start()
    {
        canUseMap = false;
    }

    public void EnterPainting()
    {
        isPlayerPaused = true;
    }

    public void ExitPainting()
    {
        isPlayerPaused = false;
    }

    private void Update()
    {
        if (IsPaused) return;
        if (!canUseMap) return;
        
        if(Input.GetKeyDown(KeyCode.M))
        {
            SwitchMap();
        }

    }

    public void SwitchMap()
    {
        if(isMapOpen)
        {
            CloseMap();
        }
        else
        {
            OpenMap();
        }
        if(mapSound!=null)
        {
            mapSound.Play();
        }
        mapObject.SetActive(isMapOpen);
    }

    public void CloseMap()
    {
        isMapOpen = false;
    }

    public void OpenMap()
    {
        isMapOpen = true;
    }

    public bool IsPaused
    {
        get
        {
            return isPlayerPaused || PauseManager.Instance.onPause;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject mainPauseMenu;
    public GameObject[] pauseSubmenus;


    private void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }

    private void SwitchPause()
    {
        if(PauseManager.Instance.onPause)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        PauseManager.Instance.Pause();
        pauseMenu?.SetActive(true);
        mainPauseMenu?.SetActive(true);
        foreach (GameObject submenu in pauseSubmenus)
        {
            submenu.SetActive(false);
        }
    }

    public void Resume()
    {
        PauseManager.Instance.Resume();
        
        pauseMenu?.SetActive(false);
    }
}

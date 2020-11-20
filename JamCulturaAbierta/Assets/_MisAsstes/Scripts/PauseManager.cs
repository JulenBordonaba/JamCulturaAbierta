using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{


    public bool onPause = false;

    #region Before pause settings

    private PauseSettings pauseSettings;

    #endregion
    public void Pause()
    {
        if (pauseSettings == null)
        {
            pauseSettings = new PauseSettings();
        }
        SaveSettings(ref pauseSettings);

        SetPauseSettings();

        onPause = true;
    }

    public void Resume()
    {
        if (pauseSettings == null)
        {
            pauseSettings = new PauseSettings();
        }
        LoadSettings(pauseSettings);

        onPause = false;
    }

    private void SaveSettings(ref PauseSettings settings)
    {

        settings.cursorVisible = Cursor.visible;
        settings.cursorLockMode = Cursor.lockState;
        settings.timeScale = Time.timeScale;
    }

    private void SetPauseSettings()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    private void LoadSettings(PauseSettings settings)
    {
        Cursor.visible = settings.cursorVisible;
        Cursor.lockState = settings.cursorLockMode;
        Time.timeScale = settings.timeScale;
    }


    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1;
    }


}

public class PauseSettings
{
    public bool cursorVisible = true;
    public CursorLockMode cursorLockMode = CursorLockMode.None;
    public float timeScale = 1;

    public PauseSettings()
    {
        cursorVisible = true;
        cursorLockMode = CursorLockMode.None;
        timeScale = 1;
    }
}

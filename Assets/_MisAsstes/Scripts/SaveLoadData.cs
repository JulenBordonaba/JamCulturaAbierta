﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadData : Singleton<SaveLoadData>
{
    [HideInInspector]
    public bool dataLoaded = false;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        LoadData();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveData();
    }


    private void OnApplicationQuit()
    {
        SaveData();
    }


    public void SaveData()
    {
        if (!dataLoaded) return;
        print("Save");
        Global.SaveData<Settings>(OptionsMenu.settings, "Settings.json");
    }

    public void LoadData()
    {
        print("Load");
        Settings settings = Global.LoadData<Settings>("Settings.json");
        if (settings == null)
        {
            OptionsMenu.settings = new Settings();
        }
        else
        {
            OptionsMenu.settings = settings;
        }

        dataLoaded = true;
    }
}

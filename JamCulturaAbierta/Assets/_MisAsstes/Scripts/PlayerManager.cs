using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public IPlayer[] playerControllScripts;

    // Start is called before the first frame update
    void Start()
    {
        playerControllScripts = GetComponents<IPlayer>();
        Cursor.visible = false;
    }

    public void Pause()
    {
        foreach(IPlayer p in playerControllScripts)
        {
            p.EnterPainting();
        }
    }

    public void Resume()
    {
        foreach (IPlayer p in playerControllScripts)
        {
            p.ExitPainting();
        }
    }


    
}

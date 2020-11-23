using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraInteractions : MonoBehaviour
{
    public void AllowMinimap()
    {
        PlayerMinimap.canUseMap = true;
    }
}

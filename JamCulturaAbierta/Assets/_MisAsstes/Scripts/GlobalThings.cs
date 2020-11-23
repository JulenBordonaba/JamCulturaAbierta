using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalThings : MonoBehaviour
{

    public TMP_FontAsset globalFont;


    private void Awake()
    {
        foreach(TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            text.font = globalFont;
        }
    }



}

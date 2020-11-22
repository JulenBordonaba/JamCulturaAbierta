using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[SerializeField]
public class ConversationOptionsGroup
{
    public string id;
    public Animator animator;


    public void Open()
    {
        animator.SetTrigger("Open");
    }

    public void Close()
    {
        animator.SetTrigger("_Close");
        
    }


}

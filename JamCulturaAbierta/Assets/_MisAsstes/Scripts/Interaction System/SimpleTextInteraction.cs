using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTextInteraction : MonoBehaviour, IInteractable
{

    public string message;

    public void Interact()
    {
        GlobalUIElemnts.Instance.WriteMessage(message);
    }
}

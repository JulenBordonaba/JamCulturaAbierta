using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInteraction : MonoBehaviour, IInteractable
{

    public UnityEvent OnInteraction = new UnityEvent();

    public void Interact()
    {
        OnInteraction.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : Singleton<ConversationManager>
{
    public event Action<string> OnTriggerSet;

    

    public void SetTrigger(string trigger)
    {
        OnTriggerSet.Invoke(trigger);
    }
}

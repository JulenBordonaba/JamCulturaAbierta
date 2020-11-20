using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationState", menuName = "Conversation/State", order = 1)]
public class ConversationState : ScriptableObject
{
    public ConversationEvent[] events;
    public string finishTrigger;

    private int currentEvent = 0;

    public void Open()
    {
        CurrentEvent.Open();
    }

    public void Update()
    {

    }

    public void End()
    {
        CurrentEvent.Close();
    }

    public ConversationEvent CurrentEvent
    {
        get
        {
            if (events.Length <= 0) return null;
            if (currentEvent >= events.Length) return events[events.Length - 1];
            return events[currentEvent];
        }
    }

}

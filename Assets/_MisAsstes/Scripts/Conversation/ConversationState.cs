using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationState", menuName = "Conversation/State", order = 1)]
public class ConversationState : ScriptableObject
{
    public ConversationEvent[] events;
    public ConversationEvent auxiliarEvent;
    public string enterTrigger;

    private int currentEvent = 0;

    public void Open()
    {
        
        currentEvent = 0;
        CurrentEvent.Open();
    }

    public void Update()
    {
        CurrentEvent.Update();
    }

    public void End()
    {
        CurrentEvent.Close();
    }

    public void Next()
    {

        CurrentEvent.Close();

        if (!Unlocked)
        {
            ConversationView.currentConversation.CloseConversation();
            return;
        }



        currentEvent += 1;

        if (currentEvent >= events.Length)
        {
            currentEvent = events.Length;
            ConversationView.currentConversation.FinishState();
            return;
        }
        CurrentEvent.Open();
    }
    
    public ConversationEvent CurrentEvent
    {
        get
        {
            if (!Unlocked) return auxiliarEvent;
            if (events.Length <= 0) return null;
            if (currentEvent >= events.Length) return events[events.Length - 1];
            return events[currentEvent];
        }
    }

    public bool Unlocked
    {
        get
        {
            if (enterTrigger == "") return true;
            return ConversationView.currentConversation.triggers.Contains(enterTrigger);
        }
    }

}

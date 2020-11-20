using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationView : MonoBehaviour
{
    public static ConversationView currentConversation;

    [Header("State Data")]
    public ConversationState[] states;

    public int currentState = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI conversationText;
    public ConversationOptionsGroup[] optionGroups;

    private ConversationOptionsGroup currentOptionGroup = null;


    private void OnEnable()
    {
        ConversationManager.Instance.OnTriggerSet += SetTrigger;
        
    }

    public void OpenConversation()
    {
        if(currentConversation!=null)
        {
            currentConversation.CloseConversation();
        }
        currentConversation = this;
        CurrentState.Open();
    }

    public void CloseConversation()
    {

    }

    public void OpenOptionGroup(string id)
    {
        ConversationOptionsGroup og = GetOptionGroupById(id);

        if(og!=null)
        {
            if(currentOptionGroup!=null)
            {
                currentOptionGroup.Close();
            }
            og.Open();
            currentOptionGroup = og;
        }
    }

    private ConversationOptionsGroup GetOptionGroupById(string id)
    {
        foreach(ConversationOptionsGroup og in optionGroups)
        {
            if(og.id==id)
            {
                return og;
            }
        }

        return null;
    }

    private void OnDisable()
    {
        ConversationManager.Instance.OnTriggerSet -= SetTrigger;
    }
    
    private void Update()
    {
        CurrentState.Update();
    }

    private void SetTrigger(string trigger)
    {
        if (trigger == "") return;

        if(trigger==CurrentState.finishTrigger)
        {
            FinishState();
        }


    }

    private void FinishState()
    {
        CurrentState.End();
        currentState += 1;
        if (currentState >= states.Length)
        {
            currentState = states.Length - 1;
            return;
        }
        CurrentState.Open();
    }

    private ConversationState CurrentState
    {
        get
        {
            if (states.Length <= 0) return null;
            if (currentState >= states.Length) return states[states.Length - 1];
            return states[currentState];
        }
    }


}

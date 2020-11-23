using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationMessage", menuName = "Conversation/Message", order = 1)]
public class ConversationMessage : ConversationEvent
{
    public string emisor;
    public string message;

    public override void Open()
    {
        ConversationView.currentConversation.WriteMessage(message);
        ConversationView.currentConversation.emisorText.text = emisor;
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (TextWritter.isWritting)
            {
                ConversationView.currentConversation.textWritter.EndText();
            }
            else
            {
                ConversationView.currentConversation.CurrentState.Next();
            }
        }
    }

    public override void Close()
    {
        ConversationView.currentConversation.CloseMessage();
    }
}

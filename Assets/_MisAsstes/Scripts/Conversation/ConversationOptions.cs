using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationOptions", menuName = "Conversation/Options", order = 1)]
public class ConversationOptions : ConversationEvent
{
    public string optionId;
    public string emisor;
    public string message;


    public override void Open()
    {
        ConversationView.currentConversation.OpenOptionGroup(optionId);
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
        }
    }

    public override void Close()
    {
        ConversationView.currentConversation.CloseOptions();
        ConversationView.currentConversation.CloseMessage();
    }
}

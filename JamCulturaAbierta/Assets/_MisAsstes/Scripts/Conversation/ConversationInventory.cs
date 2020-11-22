using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationInventory", menuName = "Conversation/Inventory", order = 1)]
public class ConversationInventory : ConversationEvent
{
    public string emisor;
    public string message;
    public string requieredItemId;
    public string correctItemTrigger;

    public override void Open()
    {
        if (PlayerInventory.Instance.items.Count > 0)
        {
            ConversationView.currentConversation.WriteMessage(message);
            ConversationView.currentConversation.emisorText.text = emisor;
            ConversationView.currentConversation.requieredItemId = requieredItemId;
            ConversationView.currentConversation.correctItemTrigger = correctItemTrigger;
            ConversationView.currentConversation.OpenInventory();
        }
        else
        {

            ConversationView.currentConversation.WriteMessage("Mejor vuelve cuando tengas algo.");
            ConversationView.currentConversation.emisorText.text = emisor;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (PlayerInventory.Instance.items.Count <= 0)
            {
                Close();
            }
            if (TextWritter.isWritting)
            {
                ConversationView.currentConversation.textWritter.EndText();
            }
        }
    }

    public override void Close()
    {
        ConversationView.currentConversation.CloseMessage();
        ConversationView.currentConversation.CloseInventory();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ConversationInventoryItemView : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
{
    public InventoryItem item;

    private string[] wrongAnswers = new string[]
    {
        "No me sirve.", "Gracias pero mejor no.","Mejor prueba con alguna otra cosa.","Lo agradezco, pero tengo que rechazarlo."
    };

    public void ShowItem(InventoryItem _item)
    {
        item = _item;
        //itemImage.preserveAspect = false;
        if(image==null)
        {
            image = GetComponent<Image>();
        }

        image.color = new Color(1, 1, 1, Alpha);
        if (item != null)
        {
            image.sprite = Sprite.Create(item.icon, new Rect(0.0f, 0.0f, item.icon.width, item.icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        //itemImage.preserveAspect = true;
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (item == null) return;
        if (ConversationView.currentConversation.CheckInventoryItem(item.id))
        {

            PlayerInventory.Instance.Remove(item.id);
            ConversationView.currentConversation.CloseInventory();
            ConversationView.currentConversation.OpenCurrent();
        }
        else
        {
            ConversationView.currentConversation.WriteMessage(wrongAnswers[Random.Range(0,wrongAnswers.Length)]);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null) return;
        if (ConversationView.currentConversation.CheckInventoryItem(item.id))
        {
            PlayerInventory.Instance.Remove(item.id);
            ConversationView.currentConversation.CloseInventory();
            ConversationView.currentConversation.OpenCurrent();
        }
        else
        {
            ConversationView.currentConversation.WriteMessage(wrongAnswers[Random.Range(0, wrongAnswers.Length)]);
        }
    }

    private float Alpha
    {
        get
        {
            return item == null ? 0 : 1;
        }
    }
}

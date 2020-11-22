using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    public InventoryItem item;

    public Image itemImage;

    public void ShowItem(InventoryItem _item)
    {
        item = _item;
        //itemImage.preserveAspect = false;
        itemImage.color = new Color(1, 1, 1, Alpha);
        if (item != null)
        {
            itemImage.sprite =  Sprite.Create(item.icon, new Rect(0.0f, 0.0f, item.icon.width, item.icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        //itemImage.preserveAspect = true;
    }

    private float Alpha
    {
        get
        {
            return item == null ? 0 : 1;
        }
    }
}

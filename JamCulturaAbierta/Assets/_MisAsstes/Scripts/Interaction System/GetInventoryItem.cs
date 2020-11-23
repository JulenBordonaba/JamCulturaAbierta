using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInventoryItem : MonoBehaviour, IInteractable
{
    public InventoryItem item;

    public void Interact()
    {
        PlayerInventory.Instance.items.Add(item);
        StartCoroutine(HideOnEndOfFrame());
    }
    
    IEnumerator HideOnEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }

}

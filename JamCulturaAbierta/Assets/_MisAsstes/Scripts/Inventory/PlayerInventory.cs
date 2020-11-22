using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>, IPlayer
{

    public List<InventoryItem> items = new List<InventoryItem>();


    public InventoryView inventoryView;

    private bool isPlayerPaused;

    private bool isOpen = false;
    

    public void EnterPainting()
    {
        isPlayerPaused = true;
    }

    public void ExitPainting()
    {
        isPlayerPaused = false;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            SwitchInventory();
        }
    }

    private void SwitchInventory()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Remove(string itemId)
    {
        foreach(InventoryItem item in items)
        {
            if (item.id == itemId)
            {
                items.Remove(item);
                return;
            }
        }
    }

    public void Close()
    {
        if (IsPaused) return;
        isOpen = false;

        inventoryView.container.SetActive(false);

        inventoryView.Close();
    }

    public void Open()
    {
        if (IsPaused) return;
        if (items.Count <= 0) return;

        isOpen = true;

        inventoryView.container.SetActive(true);

        inventoryView.Open(items);

    }


    public bool IsPaused
    {
        get
        {
            return isPlayerPaused || PauseManager.Instance.onPause;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
public class InventoryItem : ScriptableObject
{
    public string id;
    public Texture2D icon;
}

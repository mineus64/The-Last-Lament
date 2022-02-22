using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Item", menuName="Inventory/Item", order=0)]
public class Item : ScriptableObject
{
#region Variables
    [Header("Item Attributes")]
    public ushort itemID;       // The unique ID of this item
    public string itemName;     // The name of this item
    public string itemDesc;     // The description of this item
    public Sprite itemSprite;   // The sprite for this item
#endregion
#region Constructors
    public Item(ushort itemID, string itemName = null, string itemDesc = null, Sprite itemSprite = null) {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDesc = itemDesc;
        this.itemSprite = itemSprite;
    }
#endregion
}

public class ItemSlot 
{
#region Variables
    public ushort itemID;
    public uint itemQuantity;
#endregion
}
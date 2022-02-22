using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Inventory/Consumable", order = 1)]
public class Consumable : Item 
{
#region Variables

#endregion
#region Constructors
    public Consumable(ushort itemID, string itemName = null, string itemDesc = null, Sprite itemSprite = null) : base(itemID, itemName, itemDesc, itemSprite)
    {
        // Item properties
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDesc = itemDesc;
        this.itemSprite = itemSprite;
        // Consumable properties
        
    }
#endregion
}


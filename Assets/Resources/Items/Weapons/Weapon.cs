using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Inventory/Weapon", order = 1)]
public class Weapon : Equipment
{
#region Variables

#endregion
#region Constructors
    public Weapon(ushort itemID, string itemName = null, string itemDesc = null, Sprite itemSprite = null, Sprite spritesheet = null) : base(itemID, itemName, itemDesc, itemSprite)
    {
        // Item properties
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDesc = itemDesc;
        this.itemSprite = itemSprite;
        // Equipment properties
        this.spritesheet = spritesheet;
        // Weapon properties
    }
#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Inventory/Equipment", order = 1)]
public class Equipment : Item
{
#region Variables
    [Header("Equipment Attributes")]
    public Sprite spritesheet;
#endregion
#region Constructors
    public Equipment(ushort itemID, string itemName = null, string itemDesc = null, Sprite itemSprite = null, Sprite spritesheet = null) : base(itemID, itemName, itemDesc, itemSprite)
    {
        // Item properties
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDesc = itemDesc;
        this.itemSprite = itemSprite;
        // Equipment properties
        this.spritesheet = spritesheet;
    }
#endregion
}

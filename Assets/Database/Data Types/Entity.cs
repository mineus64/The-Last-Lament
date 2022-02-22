#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
#region Variables
    [Header("Identification Variables")]
    public int entityID;
    public int characterID;
    [Header("Type Variables")]
    public EntityType entityType;
    [Header("Location Variables")]
    public ushort region;
    public byte interior;
    public Vector2 position;
    [Header("Appearance Variables")]
    public uint entityBase;
    public ushort[] entityClothes;
#endregion
#region Constructors
    // Constructor for a basic entity with no location given
    public Entity(int entityID, EntityType entityType = EntityType.Player, ushort region = 0, byte interior = 0, Vector2 position = default(Vector2), int characterID = 0, uint entityBase = 0, ushort[] entityClothes = null) 
    {
        // Set Identification Variables
        this.entityID = entityID;
        this.characterID = characterID;
        // Set Type Variables
        this.entityType = entityType;
        // Set Location Variables
        this.region = region;
        this.interior = interior;
        this.position = position;
        // Set Appearance Variables
        this.entityBase = entityBase;
        if (entityClothes == null) {
            entityClothes = new ushort[7];
        }
        else {
            this.entityClothes = entityClothes;
        }
    }
#endregion
#region Network Reader/Writers

#endregion
#region Methods

#endregion
#region Enums
    public enum EntityType 
    {
        Player,
        AI
    }
#endregion
}
#endif
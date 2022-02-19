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
#endregion
#region Constructors
    // Constructor for a basic entity with no location given
    public Entity(int entityID, EntityType entityType, ushort region = 0, byte interior = 0, Vector2 position = default(Vector2), int characterID = 0) 
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
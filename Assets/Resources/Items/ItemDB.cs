using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
#region Singleton Reference
    private static ItemDB current;

    public static ItemDB Current {
        get {
            return current;
        }
    }
#endregion
#region Variables

#endregion
#region Databases
    // Item database
    public Item[] itemDB;
    // Race DB
    public PlayerRace[] raceDB;
#endregion
#region General Methods
    void Awake() 
    {
        // Set this as the self-referential singleton reference
        if (current != null && current != this) {
            Destroy(this.gameObject);
        }
        else {
            current = this;
        }
    }
    void OnDestroy() 
    {
        // Safe removal
        if (this == current) {
            current = null;
        }
    }
#endregion
#region Specific Methods

#endregion
}

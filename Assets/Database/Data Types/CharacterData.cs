#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
#region Variables
    [Header("Identification Variables")]
    public int characterID;
    public int accountID;
    public int entityID;
    [Header("Player Cosmetic Variables")]
    public string charName;
    [Header("Player Equipment Variables")]
    public ushort[] equipment;
#endregion
#region Constructors
    public CharacterData(int characterID, int accountID, string charName, int entityID, ushort[] equipment) 
    {
        // Identification Variables
        this.characterID = characterID;                                 // Set characterID
        this.accountID = accountID;                                     // Set accountID
        Database.accountData[accountID].chars.Add(this.characterID);    // Add the character ID to the account's list of characters
        Database.characterNames.Add(charName);                          // Add the character name to the database character names list
        this.entityID = entityID;                                       // Set entityID
        // Player Cosmetic Variables
        this.charName = charName;                                       // Set charName
        // Player Equipment Varibles
        this.equipment = equipment;                                     // Set equipment
    }
#endregion
#region Network Reader/Writers

#endregion
#region Methods

#endregion
}
#region Enums

#endregion
#endif
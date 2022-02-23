using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Race", menuName = "World/Race", order = 1)]
public class PlayerRace : ScriptableObject
{
#region Variables
    #region Identifiers
    public byte raceID;
    public string raceName;
    #endregion
    #region Appearance Data
    public Sex[] sexes;
    #endregion
#endregion
#region Constructors

#endregion
}

// Class for individual characteristic (ie. hair, facial hair, eyes, etc.) data
[System.Serializable]
public class Characteristic 
{
#region Variables
    public string characteristicName;
    public Sprite[] characteristicSpritesheets;
#endregion
}

// Class for individual sex (male, female) data
[System.Serializable]
public class Sex
{
#region Variables
    public string sexName;
    public Sprite[] ethnicities;
    public Characteristic[] characteristics;
#endregion
}
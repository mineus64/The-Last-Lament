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
    [Header("Male Appearance")]
    public Sprite[] maleEthnicities = new Sprite[8];
    public Characteristic[] maleCharacteristics;
    [Header("Female Appearance")]
    public Sprite[] femaleEthnicities = new Sprite[8];
    public Characteristic[] femaleCharacteristics;
    #endregion
#endregion
#region Constructors

#endregion
}

[System.Serializable]
public class Characteristic 
{
#region Variables
    public string characteristicName;
    public Sprite[] characteristicSpritesheets;
#endregion
}
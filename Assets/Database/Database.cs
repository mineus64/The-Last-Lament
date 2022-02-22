#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
This class contains all of the dictionaries used to store server-side persistent data. It is a temporary implementation used to set up basic persistence features and other systems so that development that depends on these features can continue. It is ideally configured in a manner that enables it to be smoothly dropped in and pulled out as needs be. 

This file might be used later for API stuff once we get a proper database running. 

Refer to the README, specifically the section "Data Structure", for more information.
*/
public static class Database
{
#region Collections
    // Dictionary/ies related to account information
    public static Dictionary<int, AccountData> accountData = new Dictionary<int, AccountData>();
    public static Dictionary<string, int> usernameMap = new Dictionary<string, int>();
    // Dictionary/ies related to specific character information
    public static Dictionary<int, CharacterData> characterData = new Dictionary<int, CharacterData>();
    public static HashSet<string> characterNames = new HashSet<string>();
    // Dictionary/ies related to entity information
    public static Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
#endregion
#region Methods
    // Method to check if a username exists
    public static bool CheckForUsername(string username) 
    {
        bool output;
        try
        {
            output = usernameMap.ContainsKey(username);
        }
        catch (KeyNotFoundException)
        {
            output = false;
        }
        return output;
    }
    // Method to check if a specified account is already logged in
    public static bool CheckLogin(int accountID) 
    {
        bool output;
        output = accountData[accountID].loggedIn;

        return output;
    }
    // Method to check the password for a given account
    public static bool CheckPassword(int accountID, string password) 
    {
        bool output;
        string hashed;

        hashed = Crypto.SHA512Crypt(password, accountData[accountID].passSalt);

        output = (accountData[accountID].passHash == hashed);

        return output;
    }
    // Method to create a new account
    public static int CreateAccount(string username, string password) 
    {
        int accountID = accountData.Count;
        accountData.Add(accountID, new AccountData(accountID, username, password));
        return accountID;
    }
    // Method to create a new character
    public static void CreateCharacter(int accountID, string charName) 
    {
        // Get the IDs of all of the components necessary for the character
        int characterID = characterData.Count;
        int entityID = entities.Count;
        // Create the entity the character will use
        entities.Add(entityID, new Entity(entityID, Entity.EntityType.Player, 0, 0, Vector2.zero, characterID));
        // Create the character
        characterData.Add(characterID, new CharacterData(characterID, accountID, charName, entityID));
    }
#endregion
}
#endif
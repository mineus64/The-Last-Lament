#if UNITY_SERVER || UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountData
{
#region Variables
    public int accountID;

    public string username;
    public string passHash;
    public string passSalt;
    public bool loggedIn;

    public List<int> chars = new List<int>();

    public DateTime creationDate;
#endregion
#region Constructors
    public AccountData(int accountID, string username, string password) 
    {
        this.accountID = accountID;
        this.username = username;
        string salt = Crypto.SHA512Crypt(accountID.ToString(), "Celestis");
        this.passHash = Crypto.SHA512Crypt(password, salt);
        this.passSalt = salt;
        this.loggedIn = false;
        this.creationDate = DateTime.Now;
        Database.usernameMap.Add(username, accountID);
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
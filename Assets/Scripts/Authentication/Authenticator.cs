using Mirror;
using Mirror.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authenticator : NetworkAuthenticator
{
#region Variables
    [Header("Credentials")]
    public string username;     // The specified username
    public string password;     // The specified password
    public byte status;         // Status code to determine which authentication path to proceed with
    [Header("Authentication Settings")]
    public int minPassLen;      // The minimum length, in characters, for the password
    [Header("Objects")]
    public MainMenuController mainMenuController;

    #if UNITY_SERVER || UNITY_EDITOR
    readonly HashSet<NetworkConnection> connectionsPendingDisconnect = new HashSet<NetworkConnection>();
    #endif
#endregion
#region Start Methods
    #if UNITY_SERVER || UNITY_EDITOR
    public override void OnStartServer() 
    {
      base.OnStartServer();
      // Register handlers for messages from client to server
      NetworkServer.RegisterHandler<AcctCreationRequestMessage>(OnAcctCreationRequestMessage, false);
      NetworkServer.RegisterHandler<CharCreationRequestMessage>(OnCharCreationRequestMessage, false);
      NetworkServer.RegisterHandler<LoginRequestMessage>(OnLoginRequestMessage, false);
    }
    #endif
    #if !UNITY_SERVER
    public override void OnStartClient()
    {
      // Register handlers for messages from server to client
      NetworkClient.RegisterHandler<AcctCreationResponseMessage>((Action<AcctCreationResponseMessage>)OnAcctCreationResponseMessage, false);
      NetworkClient.RegisterHandler<CharCreationResponseMessage>((Action<CharCreationResponseMessage>)OnCharCreationResponseMessage, false);
      NetworkClient.RegisterHandler<LoginResponseMessage>((Action<LoginResponseMessage>)OnLoginResponseMessage, false);
    }
    #endif
#endregion
#region Server Methods
    #if UNITY_SERVER || UNITY_EDITOR
    // General method to reject a connection for some reason
    void RejectConnection(NetworkConnection conn, byte code, string type) 
    {
        // Add this connection to the list of connections pending disconnection
        connectionsPendingDisconnect.Add(conn);
        // Create the response message
        switch (type) 
        {
            case "Account Creation":
                // Craft the message
                AcctCreationResponseMessage ACresMsg = new AcctCreationResponseMessage
                {
                    code = code
                };
                // Send the response message to the client
                conn.Send(ACresMsg);
                break;
            case "Character Creation":
                // Craft the message
                CharCreationResponseMessage CCresMsg = new CharCreationResponseMessage 
                {
                    code = code
                };
                // Send the response message to the client
                conn.Send(CCresMsg);
                break;
            case "Login":
                // Craft the message
                LoginResponseMessage LresMsg = new LoginResponseMessage 
                {
                    code = code
                };
                // Send the response message to the client
                conn.Send(LresMsg);
                break;
            default:
                return;
        }
        // Set isAuthenticated to false
        conn.isAuthenticated = false;
        // Disconnect the client
        StartCoroutine(DelayedDisconnect(conn, 1f));
    }
    // General method to accept a connection

    // Called on server from OnServerAuthenticateInternal when a client needs to authenticate
    public override void OnServerAuthenticate(NetworkConnection conn)
    {
      // do nothing...wait for AuthRequestMessage from client
    }
    // Called on the server when the client requests to create an account
    public void OnAcctCreationRequestMessage(NetworkConnection conn, AcctCreationRequestMessage msg) 
    {
        // Return if this connection is about to disconnect
        if (connectionsPendingDisconnect.Contains(conn)) {
            return;
        }
        // Check if the username already exists
        if (Database.CheckForUsername(msg.authUsername) == true) {
            RejectConnection(conn, 001, "Account Creation");
            return;
        }
        // Check if the password exceeds the minimum length
        if (msg.authPassword.Length < minPassLen) {
            RejectConnection(conn, 003, "Account Creation");
            return;
        }
        // If it passes the checks, create the account
        int accountID = Database.CreateAccount(msg.authUsername, msg.authPassword);

        /*
        TEMPORARY: Create a generic character to go with the account. This will have to do for now until character creation is squared away.
        */
        Database.CreateCharacter(accountID, Crypto.SHA512Crypt(accountID.ToString(), "J. R. R. Tolkien"));

        // Send the response message
        AcctCreationResponseMessage acctCreResMsg = new AcctCreationResponseMessage 
        {
            code = 000
        };
        conn.Send(acctCreResMsg);
        // Disconnect the player
        StartCoroutine(DelayedDisconnect(conn, 1f));
    }
    // Called on the server when the client requests to create a character
    public void OnCharCreationRequestMessage(NetworkConnection conn, CharCreationRequestMessage msg) 
    {
        // Return if this connection is about to disconnect
        if (connectionsPendingDisconnect.Contains(conn)) {
            return;
        }
    }
    // Called on the server when the client requests to login
    public void OnLoginRequestMessage(NetworkConnection conn, LoginRequestMessage msg) 
    {
        // Return if this connection is about to disconnect
        if (connectionsPendingDisconnect.Contains(conn)) {
            return;
        }
        int accountID;
        // Check if the username exists
        if (Database.CheckForUsername(msg.authUsername) == false) {
            RejectConnection(conn, 002, "Login");
            return;
        }
        else {
            accountID = Database.usernameMap[msg.authUsername];
        }
        // Check if the account is already logged in
        if (Database.CheckLogin(accountID) == true) {
            RejectConnection(conn, 005, "Login");
            return;
        }
        // Check the password
        if (Database.CheckPassword(accountID, msg.authPassword) == false) {
            RejectConnection(conn, 004, "Login");
            return;
        }
        // If it passes the checks, log in the account
        LoginResponseMessage logRespMsg = new LoginResponseMessage 
        {
            code = 000
        };

        conn.Send(logRespMsg);

        // Set the authentication token to the appropriate character ID
        conn.authenticationData = Database.accountData[accountID].chars[0];
        
        /* 
        This is a very temporary implemenetation that just takes the first character in the characters list. Once character creation is in place, this will instead start the character selection process on the client which will then start the login process. But for now this will suffice.
        */
        ServerAccept(conn);
    }   
    #endif
#endregion
#region Client Methods
    //#if !UNITY_SERVER
    // Called on client from OnClientAuthenticateInternal when a client needs to authenticate
    public override void OnClientAuthenticate() 
    {
        Debug.Log("OnClientAuthenticate");
        switch(status) 
        {
            case 0:
                OnClientLogin();
                break;
            case 1:
                OnClientRegister();
                break;
            case 2:
                OnClientCharacterCreate();
                break;
            default:
                Debug.Log("Invalid status code passed to authenticator");
                break;
        }
    }
    // Method on client to send a login request
    void OnClientLogin() 
    {
        Debug.Log("OnClientLogin");
        LoginRequestMessage logReqMsg = new LoginRequestMessage 
        {
            authUsername = username,
            authPassword = password
        };

        NetworkClient.connection.Send(logReqMsg);
    }
    // Method on client to send a registration request
    void OnClientRegister() 
    {
        Debug.Log("OnClientRegister");
        AcctCreationRequestMessage acctCreReqMsg = new AcctCreationRequestMessage 
        {
            authUsername = username,
            authPassword = password
        };

        NetworkClient.connection.Send(acctCreReqMsg);
    }
    // Method on client to send a character creation request
    void OnClientCharacterCreate()
    {

    }
    // Called on client when the server's AcctCreationResponseMessage arrives
    void OnAcctCreationResponseMessage(AcctCreationResponseMessage msg) 
    {
        Debug.Log("OnAcctCreationResponseMessage");
        mainMenuController.RegisterResponse(msg.code);
    }
    // Called on client when the server's CharCreationResponseMessage arrives
    void OnCharCreationResponseMessage(CharCreationResponseMessage msg) 
    {

    }
    // Called on client when the server's LoginResponseMessage arrives
    void OnLoginResponseMessage(LoginResponseMessage msg) 
    {
        Debug.Log("OnLoginResponseMessage");
        mainMenuController.LoginResponse(msg.code);
        /*
        This code fixed bug #4, because I am an idiot that forgot that the client needs to actually accept the connection it is trying to make.
        Didn't fix #6 tho, which is disappointing. That one needs work.
        */
        if (msg.code == 000) {
            ClientAccept();
        }
        else {
            ClientReject();
        }
    }
    //#endif
#endregion
#region Coroutines
    #if UNITY_SERVER || UNITY_EDITOR
    // Coroutine to disconnect the connection after an unsuccessful authentication
    IEnumerator DelayedDisconnect(NetworkConnection conn, float waitTime)
    {
      yield return new WaitForSeconds(waitTime);
      // Reject the unsuccessful authentication
      ServerReject(conn);
      yield return null;
      // Remove the connection from the list of pending disconnections
      connectionsPendingDisconnect.Remove(conn);
    }
    #endif
#endregion
#region NetworkMessages
    #region Client -> Server
    // Message to request account creation
    public struct AcctCreationRequestMessage : NetworkMessage 
    {
        public string authUsername;
        public string authPassword;
    }
    // Message to request character creation
    public struct CharCreationRequestMessage : NetworkMessage 
    {

    }
    // Message to request login
    public struct LoginRequestMessage : NetworkMessage 
    {
        public string authUsername;
        public string authPassword;
    }
    #endregion
    #region Server -> Client
    // Message to respond to an account creation request
    public struct AcctCreationResponseMessage : NetworkMessage 
    {
        public byte code;
    }
    // Message to respond to a character creation request
    public struct CharCreationResponseMessage : NetworkMessage
    {
        public byte code;
    }
    // Message to respond to a login request
    public struct LoginResponseMessage : NetworkMessage 
    {
        public byte code;
    }
    #endregion
#endregion
}

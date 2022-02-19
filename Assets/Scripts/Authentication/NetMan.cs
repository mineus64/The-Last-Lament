using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetMan : NetworkManager
{
#region Variables
    [Scene] public string gameScene = "";
#endregion
#region Initialization Methods
    #if UNITY_SERVER || UNITY_EDITOR
    public override void OnStartServer() 
    {
        base.OnStartServer();
        // Register messages
        NetworkServer.RegisterHandler<ClientReady>(OnClientReady);
    }
    #endif
    #if !UNITY_SERVER || UNITY_EDITOR
    public override void OnStartClient() 
    {
        base.OnStartClient();
        
    }
    #endif
#endregion
#region Connection Methods
    #if !UNITY_SERVER || UNITY_EDITOR
    // Called on the client when the client passes the authentication process and connects to the server.
    public override void OnClientConnect() 
    {
        Console.WriteLine("OnClientConnect");
        base.OnClientConnect();
        // Poke the server to let it know the client is ready
        ClientReady readyMsg = new ClientReady 
        {

        };
        NetworkClient.connection.Send(readyMsg);
        
        /*
        In the future this will contain the client's character selection. But for the time being, just poke the server to start the process.
        */
    }
    #endif
    #if UNITY_SERVER || UNITY_EDITOR 
    // Called on the server when a ClientReady message is received
    public void OnClientReady(NetworkConnection conn, ClientReady msg) 
    {
        Console.WriteLine("OnClientReady");
        int characterID = (int)conn.authenticationData;

        GameObject playerObject = Instantiate(playerPrefab);

        playerObject.name = Database.characterData[characterID].charName;
        EntityController controller = playerObject.GetComponent<EntityController>();
        // Set tracking variables for this entity
        int entityID = Database.characterData[characterID].entityID;
        // Update this entity's position and rotation shared variables
        Entity entity = Database.entities[entityID];
        // Make this object available for the player
        NetworkServer.AddPlayerForConnection(conn, playerObject);
    }
    #endif
#endregion
#region Disconnect Methods
    #if UNITY_SERVER || UNITY_EDITOR
    public override void OnServerDisconnect(NetworkConnection conn) 
    {
        base.OnServerDisconnect(conn);
    }
    #endif
#endregion
}
#region Network Messages
public struct ClientReady : NetworkMessage
{

}
#endregion

using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
// Important variables
#region Variables
    [Header("Menus")]
    [SerializeField] GameObject[] menus;
    [SerializeField] GameObject[] errors;
    [Header("Input Fields")]
    [SerializeField] TextMeshProUGUI registerUsername;
    [SerializeField] TextMeshProUGUI registerPassword;
    [SerializeField] TextMeshProUGUI loginUsername;
    [SerializeField] TextMeshProUGUI loginPassword;
    [Header("Network Objects")]
    [SerializeField] Authenticator authenticator;
    [SerializeField] NetworkManager networkManager;
#endregion
#region Methods
    #region General Methods
    // Start is called before the first frame update
    void Start()
    {
        // Fallback to make sure the menu is set up properly
        LoadMenu(0);
        LoadError(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Specific Methods
    // LoadMenu is used to load a specific menu from a given key
    // Set id to -1 to clear all menus from the screen
    public void LoadMenu(int id) 
    {
        // Fallback check to make sure the loaded menu is valid
        if (id > menus.Length || id < -1) {
            Debug.Log("Error: Tried to load invalid menu #" + id);
            return;
        }
        // Loop through all of the menus in the array
        for (int i = 0; i < menus.Length; i++) {
            // If the current menu in the array matches the one we want to be active, set it to be so
            if (i != id || id == -1) {
                menus[i].SetActive(false);
            }
            // If the current menu in the array does not match the one we want, set it inactive
            else {
                menus[i].SetActive(true);
            }
        }
        // That way, we don't have multiple menus drawing at the same time
    }
    // LoadError is used to load an additional error message on top of the menus
    // Set id to -1 to clear all errors on screen
    public void LoadError(int id) 
    {
        // Fallback check to make sure the loaded menu is valid
        if (id > errors.Length || id < -1) {
            Debug.Log("Error: Tried to load invalid error #" + id);
            return;
        }
        // Loop through all of the menus in the array
        for (int i = 0; i < errors.Length; i++) {
            // If the current menu in the array matches the one we want to be active, set it to be so
            if (i != id || id == -1) {
                errors[i].SetActive(false);
            }
            // If the current menu in the array does not match the one we want, set it inactive
            else {
                errors[i].SetActive(true);
            }
        }
    }
    // These are methods to load specific scenes, called by menu buttons. They act as an interface between menu buttons and menu loading
    public void ToMainMenu() 
    {
        LoadMenu(0);
        LoadError(-1);
    }
    public void ToLoginMenu() 
    {
        LoadMenu(1);
        LoadError(-1);
    }
    public void ToRegisterMenu() 
    {
        LoadMenu(2);
        LoadError(-1);
    }
    public void ToQuit() 
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
    #endregion
    #region Authentication Methods
    // Method to login to the game
    public void OnLogin() 
    {
        authenticator.username = loginUsername.text;
        authenticator.password = loginPassword.text;
        authenticator.status = 0;

        networkManager.StartClient();

        LoadMenu(4);
        LoadError(-1);
    }
    // Method to register a new account
    public void OnRegister() 
    {
        // Check to make sure the password is long enough
        if (registerPassword.text.Length < authenticator.minPassLen) {
            LoadError(1);
            return;
        }
        // Set appropriate authenticator variables
        authenticator.username = registerUsername.text;
        authenticator.password = registerPassword.text;
        authenticator.status = 1;
        // Start the client
        networkManager.StartClient();
        // Load the loading text and clear any errors from the screen
        LoadMenu(3);
        LoadError(-1);
    }
    // Method to create a new character
    public void OnCharacterCreate() 
    {

    }
    #endregion
    #region Menu Text Methods
    public void RegisterResponse(byte code)
    {
        switch(code) 
        {   
            // Registration has been successful
            case 000:
                // Set the authenticator status to "login"
                authenticator.status = 0;
                // Start the login process
                authenticator.OnClientAuthenticate();
                // Display the "logging in" text
                LoadMenu(4);
                break;
            // Username already exists
            case 001:
                // Bring back the registration menu
                LoadMenu(2);
                // Display the appropriate error
                LoadError(0);
                break;
            // Username does not exist
            case 002:
                // Bring back the registration menu
                LoadMenu(2);
                break;
            // Password isn't long enough
            case 003:
                // Bring back the registration menu
                LoadMenu(2);
                // Display the appropriate error
                LoadError(1);
                break;
            // Password is incorrect
            case 004:
                // Bring back the registration menu
                LoadMenu(2);
                break;
            // Account is already logged in
            case 005:
                // Bring back the registration menu
                LoadMenu(2);
                break;
            default:
                break;
        }
    }
    public void LoginResponse(byte code)
    {
        switch(code) 
        {
            // Login successful
            case 000:
                // Get rid of all menus and errors on the screen
                LoadMenu(-1);
                LoadError(-1);
                break;
            // Username already exists
            case 001:
                // Bring back the login menu
                LoadMenu(1);
                break;
            // Username does not exist
            case 002:
                // Bring back the login menu
                LoadMenu(1);
                // Display the appropriate error
                LoadError(2);
                break;
            // Password isn't long enough
            case 003:
                // Bring back the login menu
                LoadMenu(1);
                break;
            // Password is incorrect
            case 004:
                // Bring back the login menu
                LoadMenu(1);
                // Display the appropriate error
                LoadError(3);
                break;
            // Account is already logged in
            case 005:
                // Bring back the login menu
                LoadMenu(1);
                // Display the appropriate error
                LoadError(4);
                break;
            default:
                break;
        }
    }
    #endregion
#endregion
}

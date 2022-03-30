using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if !UNITY_SERVER || UNITY_EDITOR
public class CharacterCreator : MonoBehaviour
{
#region Variables
    [Header("Output Image")]
    public GameObject preview;
    public SpriteRenderer outputImage;
    public Texture2D animBase;
    [Header("Race Selection")]
    public TextMeshProUGUI raceText;
    [Header("Character Customisation")]
    public TextMeshProUGUI[] characteristicTexts;
    [Header("Outputs")]
    /*
    The Unpacked Byte Array stores information as follows:
    0: Player race
    1: Player sex
    2: Player ethnicity
    3: Characteristic 1
    4: Characteristic 2
    5: Characteristic 3
    6: Characteristic 4
    7: Characteristic 5
    
    This then gets packed down into the byte that will get passed to the server and stored for the character.
    */
    public byte[] unpackedByteArray = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    public int packedCharByte = 0;
#endregion
#region General Methods
    void Start() 
    {
        outputImage = preview.GetComponent<SpriteRenderer>();

        UpdateText();
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
        UpdatePreview();
    }
#endregion
#region Specific Methods
    // Method to update the index for the race of the character
    // Pass 1 to increment and -1 to decrement
    public void UpdateRace(int input) 
    {
        if (input > 0) {
            if (unpackedByteArray[0] + input > ItemDB.Current.raceDB.Length) {
                unpackedByteArray[0] = 0;
            }
            else {
                unpackedByteArray[0] += 1;
            }
        }
        else {
            if (unpackedByteArray[0] == 0) {
                unpackedByteArray[0] = (byte)(ItemDB.Current.raceDB.Length - 1);
            }
            else {
                unpackedByteArray[0] -= 1;
            }
        }
        UpdateText();
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
        UpdatePreview();
    }
    // Method to update the index for the sex of the character
    // Pass 1 for male and 0 for female
    public void UpdateSex(int input) 
    {
        if((unpackedByteArray[1] == 1 && input == 1) || (unpackedByteArray[1] == 0 && input == 0)) {

        }
        else {
            unpackedByteArray[1] = (byte)input;
        }
        UpdateText();
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
        UpdatePreview();
    }
    // Method to update the index for the ethnicity of the character
    // Pass 1 to increment and -1 to decrement
    // I HATE this code, see also Issue #13
    public void UpdateEthnicity(int input) 
    {
        if (input > 0) {
            if (unpackedByteArray[2] + input > ItemDB.Current.raceDB[unpackedByteArray[0]].sexes[unpackedByteArray[1]].ethnicities.Length) {
                unpackedByteArray[2] = 0;
                return;
            }
            else {
                unpackedByteArray[2] += 1;
                return;
            }
        }
        else {
            if (unpackedByteArray[2] == 0) {
                unpackedByteArray[2] = (byte)(ItemDB.Current.raceDB[unpackedByteArray[0]].sexes[unpackedByteArray[1]].ethnicities.Length - 1);
            }
        }
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
        UpdatePreview();
    }
    // Method to increment the indices for the character's characteristics
    // Pass in 1-6 for which characteristic to update
    public void IncrementCharacteristic(int characteristic) 
    {
        // Get the characteristic to change
        Characteristic thisCharacteristic = ItemDB.Current.raceDB[unpackedByteArray[0]].sexes[unpackedByteArray[1]].characteristics[characteristic - 1];
        // Increment the characteristic
        unpackedByteArray[characteristic + 2] += 1;
        // Check for overflow
        if (unpackedByteArray[characteristic + 2] >= thisCharacteristic.characteristicSpritesheets.Length) {
            unpackedByteArray[characteristic + 2] = 0;
        }
        
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
        UpdatePreview();
    }
    // Method to decrement the indices for the character's characteristics
    // Pass in 1-6 for which characteristic to update
    public void DecrementCharacteristic(int characteristic) 
    {
        Characteristic thisCharacteristic = ItemDB.Current.raceDB[unpackedByteArray[0]].sexes[unpackedByteArray[1]].characteristics[characteristic - 1];
        if ((int)unpackedByteArray[characteristic + 2] - 1 < 0) {
            unpackedByteArray[characteristic + 2] = (byte)(thisCharacteristic.characteristicSpritesheets.Length - 1);
        }
        else {
            unpackedByteArray[characteristic + 2] -= 1;
        }
                
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
        UpdatePreview();
    }
    // Method to update the text fields based on the race data
    public void UpdateText() 
    {
        // Get the PlayerRace object
        PlayerRace currentRace = ItemDB.Current.raceDB[unpackedByteArray[0]];
        // Update the race name
        raceText.text = currentRace.raceName;
        // Update the characteristic names
        for (int i = 0; i < 6; i ++) {
            if (currentRace.sexes[unpackedByteArray[1]].characteristics.Length - 1 < i) {
                characteristicTexts[i].transform.gameObject.SetActive(false);
            }
            else {
                characteristicTexts[i].transform.gameObject.SetActive(true);
                characteristicTexts[i].text = currentRace.sexes[unpackedByteArray[1]].characteristics[i].characteristicName;
            }
        }
    }
    /*
    // Method to pack the byte
    public void UpdatePackedByte() 
    {
        // Reset the byte
        packedCharByte = 0;
        // Add the race data
        uint raceVal = 536870912 * (uint)(unpackedByteArray[0]);
        packedCharByte += raceVal;
        // Add the sex and ethnicity data
        uint sexEthVal = 0;
        sexEthVal += (uint)unpackedByteArray[2];
        sexEthVal += (uint)unpackedByteArray[1] * 16;
        packedCharByte += sexEthVal * 33554432;
        // Add the characteristic data
        for (int i = 3; i < 8; i++) {
            // 1, 5, 9, 13, 17, 21
            int pow = ((i - 3) * 4) + 1;
            packedCharByte += (uint)unpackedByteArray[i] * (uint)Mathf.Pow(2, pow);
        }
    }
    */
    // Method to update the character preview
    public void UpdatePreview() 
    {
        Debug.Log("Updating Preview");
        // Create the input spritesheet array
        Texture2D[] spriteLayers;
        // Get the relevant race characteristics

        // Get the number of iterations
        int iterations;
        iterations = ItemDB.Current.raceDB[unpackedByteArray[0]].sexes[unpackedByteArray[1]].characteristics.Length;
        // Set the length of spriteLayers
        spriteLayers = new Texture2D[iterations + 1];
        // Add the player ethnicity base to the spritelayers array
        spriteLayers[0] = ItemDB.Current.raceDB[unpackedByteArray[0]].sexes[unpackedByteArray[1]].ethnicities[unpackedByteArray[2]].texture;
        // Add the player characteristics to the spritelayers array
        for (int i = 0; i < iterations; i++) {
            spriteLayers[i + 1] = ItemDB.Current.raceDB[unpackedByteArray[0]].sexes[unpackedByteArray[1]].characteristics[i].characteristicSpritesheets[unpackedByteArray[i + 3]].texture;
        }
        // Do player sprite generation
        // outputImage.sprite = Sprite.Create(PlayerSpriteGenerator.GeneratePlayerSprite(animBase, spriteLayers), new Rect(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0.5f, 0.5f));
        outputImage.material.SetTexture("_MainTex", PlayerSpriteGenerator.GeneratePlayerSprite(animBase, spriteLayers));
    }
#endregion
}
#endif
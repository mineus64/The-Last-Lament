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
    public Image outputImage;
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
    }
    // Method to update the index for the ethnicity of the character
    // Pass 1 to increment and -1 to decrement
    // I HATE this code, see also Issue #13
    public void UpdateEthnicity(int input) 
    {
        // If we want to increment the value...
        if (input > 0) {
            // ...and if the character is male...
            if (unpackedByteArray[1] == 1) {
                // ...and if the change would overflow the value...
                if (unpackedByteArray[2] + input > ItemDB.Current.raceDB[unpackedByteArray[0]].maleEthnicities.Length) {
                    // ...reset the value.
                    unpackedByteArray[2] = 0;
                    return;
                }
                // ...and if the change would NOT overflow the value...
                else {
                    // ...increment the value.
                    unpackedByteArray[2] += 1;
                    return;
                }
            }
            // ...and if the character is female...
            else {
                // ...and if the change would overflow the value...
                if (unpackedByteArray[2] + input > ItemDB.Current.raceDB[unpackedByteArray[0]].femaleEthnicities.Length) {
                    // ...reset the value.
                    unpackedByteArray[2] = 0;
                    return;
                }
                // ...and if the change would NOT overflow the value...
                else { 
                    // ...reset the value.
                    unpackedByteArray[2] += 1;
                    return;
                }
            }
        }
        // If we want to decrement the value...
        else {
            // ...and the value would underflow...
            if (unpackedByteArray[2] == 0) {
                // ...and the character is male...
                if (unpackedByteArray[1] == 1) {
                    // ...wrap the value around to the highest possible.
                    unpackedByteArray[2] = (byte)(ItemDB.Current.raceDB[unpackedByteArray[0]].maleEthnicities.Length - 1);
                }
                // ...and the character is female...
                else {
                    // ...wrap the value around to the highest possible.
                    unpackedByteArray[2] = (byte)(ItemDB.Current.raceDB[unpackedByteArray[0]].femaleEthnicities.Length - 1);
                }
            }
        }
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
    }
    // Method to increment the indices for the character's characteristics
    // Pass in 1-6 for which characteristic to update
    public void IncrementCharacteristic(int characteristic) 
    {
        // ...and the character is male...
        if (unpackedByteArray[1] == 1) {
            // Get the characteristic to change
            Characteristic thisCharacteristic = ItemDB.Current.raceDB[unpackedByteArray[0]].maleCharacteristics[characteristic - 1];
            // Increment the characteristic
            unpackedByteArray[characteristic + 2] += 1;
            // Check for overflow
            if (unpackedByteArray[characteristic + 2] >= thisCharacteristic.characteristicSpritesheets.Length) {
                unpackedByteArray[characteristic + 2] = 0;
            }
        } 
        // ...and the character is female...
        else {
            // ...and the character is female...
            if (unpackedByteArray[1] == 0) {
                // Get the characteristic to change
                Characteristic thisCharacteristic = ItemDB.Current.raceDB[unpackedByteArray[0]].femaleCharacteristics[characteristic - 1];
                // Increment the characteristic
                unpackedByteArray[characteristic + 2] += 1;
                // Check for overflow
                if (unpackedByteArray[characteristic + 2] >= thisCharacteristic.characteristicSpritesheets.Length) {
                    unpackedByteArray[characteristic + 2] = 0;
                }
            }
        }
        
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
    }
    // Method to decrement the indices for the character's characteristics
    // Pass in 1-6 for which characteristic to update
    public void DecrementCharacteristic(int characteristic) 
    {
        // ...and the character is male...
        if (unpackedByteArray[1] == 1) {
            // Get the characteristic to change
            Characteristic thisCharacteristic = ItemDB.Current.raceDB[unpackedByteArray[0]].maleCharacteristics[characteristic - 1];
            // ...and the value would underflow...
            if ((int)unpackedByteArray[characteristic + 2] - 1 < 0) {
                unpackedByteArray[characteristic + 2] = (byte)(thisCharacteristic.characteristicSpritesheets.Length - 1);
            }
            // ...and the value will not underflow...
            else {
                unpackedByteArray[characteristic + 2] -= 1;
            }
        }
        // ...and the character is female...
        else {
            // Get the characteristic to change
            Characteristic thisCharacteristic = ItemDB.Current.raceDB[unpackedByteArray[0]].femaleCharacteristics[characteristic - 1];
            // ...and the value would underflow...
            if ((int)unpackedByteArray[characteristic + 2] - 1 < 0) {
                unpackedByteArray[characteristic + 2] = (byte)(thisCharacteristic.characteristicSpritesheets.Length - 1);
            }
            // ...and the value will not underflow...
            else {
                unpackedByteArray[characteristic + 2] -= 1;
            }
        }
                
        packedCharByte = IntPacker.PackInt(unpackedByteArray);
    }
    // Method to update the text fields based on the race data
    public void UpdateText() 
    {
        // Get the PlayerRace object
        PlayerRace currentRace = ItemDB.Current.raceDB[unpackedByteArray[0]];
        // Update the race name
        raceText.text = currentRace.raceName;
        // Update the characteristic names
        // If the character is male
        if (unpackedByteArray[1] == 1) {
            for (int i = 0; i < 6; i ++) {
                if (currentRace.maleCharacteristics.Length - 1 < i) {
                    characteristicTexts[i].transform.gameObject.SetActive(false);
                }
                else {
                    characteristicTexts[i].transform.gameObject.SetActive(true);
                    characteristicTexts[i].text = currentRace.maleCharacteristics[i].characteristicName;
                }
            }
        }
        // If the character is female
        else {
            for (int i = 0; i < 6; i ++) {
                if (currentRace.femaleCharacteristics.Length - 1 < i) {
                    characteristicTexts[i].transform.gameObject.SetActive(false);
                }
                else {
                    characteristicTexts[i].transform.gameObject.SetActive(true);
                    characteristicTexts[i].text = currentRace.femaleCharacteristics[i].characteristicName;
                }
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

    }
#endregion
}
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntPacker
{
    // Method to pack int
    public static int PackInt(byte[] unpackedByteArray) 
    {
        int output = 0;
        /*
        // Add the race data
        uint raceVal = 536870912 * (uint)(unpackedByteArray[0]);
        output += raceVal;
        // Add the sex and ethnicity data
        uint sexEthVal = 0;
        sexEthVal += (uint)unpackedByteArray[2];
        sexEthVal += (uint)unpackedByteArray[1] * 16;
        output += sexEthVal * 33554432;
        // Add the characteristic data
        for (int i = 3; i < 8; i++) {
            // 1, 5, 9, 13, 17, 21
            int pow = ((i - 3) * 4) + 1;
            output += (uint)unpackedByteArray[i] * (uint)Mathf.Pow(2, pow);
        }
        */
        
        // Create a new array for packing the nibbles
        byte[] packedNibbles = {0, 0, 0, 0};

        // Combine the upper and lower nibbles of the input bytes
        for (int i = 0; i < 4; i++) {
            int lni = 2 * i;       // 0, 2, 4, 6
            int uni = 2 * i + 1;   // 1, 3, 5, 7

            byte upperNibble = unpackedByteArray[lni];
            byte lowerNibble = unpackedByteArray[uni];

            /*
            15 * 16 = 00001111 * 00010000 = 11110000
            */

            packedNibbles[i] = (byte)((upperNibble * 16) + lowerNibble);
        }

        // Pack the combined bytes
        output = BitConverter.ToInt32(packedNibbles, 0);
        // Return the combined bytes
        return output;
    }
    // Method to unpack int
    public static byte[] UnpackInt(int input) 
    {
        byte[] output = {0, 0, 0, 0, 0, 0, 0, 0};
        byte[] packedBytes = {0, 0, 0, 0};
        // Unpack the int into packed bytes
        packedBytes = BitConverter.GetBytes(input);
        // Unpack the upper and lower nibbles of the packed bytes to get the final result
        for (int i = 0; i < 4; i++) {
            byte upperNibble = (byte)(packedBytes[i] & 0x0F);
            byte lowerNibble = (byte)((packedBytes[i] & 0xF0) >> 4);

            output[2 * i] = upperNibble;
            output[2 * i + 1] = lowerNibble;
        }

        // Return said final result
        return output;
    }
}

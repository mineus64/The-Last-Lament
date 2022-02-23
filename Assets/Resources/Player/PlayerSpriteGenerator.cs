using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if !UNITY_SERVER || UNITY_EDITOR
public static class PlayerSpriteGenerator
{
#region Variables
    static int spriteX =  832;
    static int spriteY = 1344;
#endregion
#region Methods
    // Method to combine textures
    public static Texture2D GeneratePlayerSprite(Texture2D animbase, Texture2D[] spriteLayers) 
    {
        // Create the output texture
        Texture2D output = new Texture2D(spriteX, spriteY);
        // Create temporary colour arrays
        Color32[] outputPixels = new Color32[spriteX * spriteY];
        // Color32[][] inputPixels = new Color32[spriteLayers.Length][];
        // Create arrays for the layers
        /*
        for (int i = 0; i < spriteLayers.Length; i++) {
            inputPixels[i] = new Color32[spriteX * spriteY];
            inputPixels[i] = spriteLayers[i].GetPixels32();
        }
        */
        // Combine the layers
        for (int i = 0; i < spriteLayers.Length; i++) {
            FillSprite(outputPixels, spriteLayers[i].GetPixels32());
        }
        // Set the pixels to the output texture
        output.SetPixels32(outputPixels);
        output.Apply();
        output.filterMode = FilterMode.Point;
        // Debug
        File.WriteAllBytes(Application.dataPath + "/Resources/debug.png", output.EncodeToPNG());
        // Return the output texture
        return output;
    }
    // Method to combine individual pixels
    // Thanks to SteveSmith on the Unity Developer Community Discord for help with this!
    static void FillSprite(Color32[] source, Color32[] dest) 
    {
      for (int i = 0; i < source.Length; i++) {
        switch(source[i].a) 
        {
          case 0:
            break;
          default:
            dest[i] = source[i];
            break;
        }
      }
    }
#endregion
}
#endif
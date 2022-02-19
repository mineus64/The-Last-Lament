using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LocalisationSystem : MonoBehaviour
{
    public enum Language 
    {
      EnglishUS,
      EnglishUK,
      Spanish,
      German,
      French,
      Japanese,
      Korean
    }

    public static Language language = Language.EnglishUK;
    public static Language fallbackLang = Language.EnglishUK;

    public static Dictionary<string, string> localisedEN_US;
    public static Dictionary<string, string> localisedEN_UK;
    public static Dictionary<string, string> localisedES;
    public static Dictionary<string, string> localisedDE;
    public static Dictionary<string, string> localisedFR;
    public static Dictionary<string, string> localisedJA;
    public static Dictionary<string, string> localisedKO;

    public static Dictionary<string, string> GetDictionaryForEditor() 
    {
      if (!isInit) {
        Init();
      }
      return localisedEN_UK;
    }

    public static bool isInit;

    public static CSVLoader csvLoader;

    public static void Init() 
    {
      csvLoader = new CSVLoader();
      csvLoader.LoadCSV();

      UpdateDictionaries();

      isInit = true;
    }

    public static void UpdateDictionaries() 
    {
      localisedEN_US = csvLoader.GetDictionaryValues("en_us");
      localisedEN_UK = csvLoader.GetDictionaryValues("en_uk");
      localisedES = csvLoader.GetDictionaryValues("es");
      localisedDE = csvLoader.GetDictionaryValues("de");
      localisedFR = csvLoader.GetDictionaryValues("fr");
      localisedJA = csvLoader.GetDictionaryValues("ja");
      localisedKO = csvLoader.GetDictionaryValues("ko");
    }

    public static string GetLocalisedValue(string key) 
    {
      if (!isInit) {
        Init();
      }

      string value = key;
      bool get;

      switch(language)
      {
        case Language.EnglishUS:
          get = localisedEN_US.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.EnglishUK:
          get = localisedEN_UK.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.Spanish:
          get = localisedES.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.German:
          get = localisedDE.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.French:
          get = localisedFR.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.Japanese:
          get = localisedJA.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.Korean:
          get = localisedKO.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
      }

      return value;
    }

    public static string GetLocalisedValue(string key, Language language) 
    {
      if (!isInit) {
        Init();
      }

      string value = key;
      bool get;

      switch(language)
      {
        case Language.EnglishUS:
          get = localisedEN_US.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.EnglishUK:
          get = localisedEN_UK.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.Spanish:
          get = localisedES.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.German:
          get = localisedDE.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.French:
          get = localisedFR.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.Japanese:
          get = localisedJA.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
        case Language.Korean:
          get = localisedKO.TryGetValue(key, out value);

          if (get == false) {
            if (language == fallbackLang) {
              Debug.Log("Error: No suitable value found for key " + key + " in fallback dictionary!");

              return key;
            }
            else {
              Debug.Log("Error: No suitable value found for key " + key + ", trying fallback language.");

              GetLocalisedValue(key, fallbackLang);
            }
          }
          break;
      }

      return value;
    }

#if UNITY_EDITOR
    public static void Add(string key, string value) 
    {
      if (value.Contains("\"")) {
        value.Replace('"', '\"');
      }

      if (csvLoader == null) {
        csvLoader = new CSVLoader();
      }

      csvLoader.LoadCSV();
      csvLoader.Add(key, value);
      csvLoader.LoadCSV();

      UpdateDictionaries();
    }

    public static void Replace(string key, string value) 
    {
      if (value.Contains("\"")) {
        value.Replace('"', '\"');
      }

      if (csvLoader == null) {
        csvLoader = new CSVLoader();
      }

      csvLoader.LoadCSV();
      csvLoader.Edit(key, value);
      csvLoader.LoadCSV();

      UpdateDictionaries();
    }

    public static void Remove(string key) 
    {
      if (csvLoader == null) {
        csvLoader = new CSVLoader();
      }

      csvLoader.LoadCSV();
      csvLoader.Remove(key);
      csvLoader.LoadCSV();

      UpdateDictionaries();
    }
#endif
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEditor;

[AddComponentMenu("Localisation/TextLocaliseUI")]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliseUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;

    public LocalisedString localisedString;

    // OnValidate is called whenever a value is changed in the inspector
    void OnValidate() {
      if (textField == null) {
        textField = GetComponent<TextMeshProUGUI>();
      }

      localisedString = textField.text;
    }

    // Start is called before the first frame update
    void Start()
    {
      if (textField == null) {
        textField = GetComponent<TextMeshProUGUI>();
      }
      // string value = LocalisationSystem.GetLocalisedValue(key);
      textField.text = localisedString.value;
    }
}

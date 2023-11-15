using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileMenuScript : MonoBehaviour
{
    public TMP_Text text;
    public string textToDisplay;
    
    void Update()
    {
        text.text = textToDisplay;
    }

}

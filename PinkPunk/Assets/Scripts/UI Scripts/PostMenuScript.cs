using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostMenuScript : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    public string textToDisplay1 = "Post 1";
    public string textToDisplay2 = "Post 2";
    public string textToDisplay3 = "Post 3";

    
    void Update()
    {
        text1.text = textToDisplay1;
        text2.text = textToDisplay2;
        text3.text = textToDisplay3;
    }
}

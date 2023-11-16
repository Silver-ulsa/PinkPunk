using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryMenuScript : MonoBehaviour
{

    public TMP_Text nameMisionText;
    public TMP_Text descriptionMisionText;
    public TMP_Text locationMisionText;
    public TMP_Text misionStatusText;

    public GameObject misionContainer;

    public void DisplayMisionInfo(string name, string description, string location, bool status)
    {
        nameMisionText.text = name;
        descriptionMisionText.text = description;
        locationMisionText.text = location;
        if (status)
        {
            misionStatusText.text = "En curso";
            misionContainer.SetActive(true);
        }
        else
        {
            misionStatusText.text = "";
            misionContainer.SetActive(false);
        }
    }
}

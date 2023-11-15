using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionsManagerScript : MonoBehaviour
{
    public GameObject[] misiones = new GameObject[3];

    public DeliveryMenuScript[] deliveryMenuScript;

    void Start()
    {
        for (int i = 0; i < misiones.Length; i++)
        {
            MisionDefaultScript misionDefaultScript = misiones[i].GetComponent<MisionDefaultScript>();

            if (misionDefaultScript != null)
            {
                Debug.Log("Si entre al if");

                string nameMision = misionDefaultScript.nameMision;
                string descriptionMision = misionDefaultScript.descriptionMision;
                string locationMision = misionDefaultScript.locationMision;
                bool misionStatus = misionDefaultScript.misionStatus;
                GameObject misionQuestMarkStart = misionDefaultScript.misionQuestMarkStart;
                GameObject misionQuestMarkEnd = misionDefaultScript.misionQuestMarkEnd;

                Debug.Log("Mision: " + nameMision);
                Debug.Log("Description: " + descriptionMision);
                Debug.Log("Location: " + locationMision);
                Debug.Log("Status: " + misionStatus);
                Debug.Log("Quest Mark Start: " + misionQuestMarkStart);
                Debug.Log("Quest Mark End: " + misionQuestMarkEnd);

                {
                    if (i < deliveryMenuScript.Length)
                    {
                        deliveryMenuScript[i].DisplayMisionInfo(nameMision, descriptionMision, locationMision, misionStatus);
                    }
                    else
                    {
                        Debug.Log("El objeto " + misiones[i].name + " no tiene el script DeliveryMenuScript adjunto.");
                    }

                }
            }
            else
            {
                Debug.Log("El objeto " + misiones[i].name + " no tiene el script MisionDefaultScript adjunto.");
            }
        }
    }
}

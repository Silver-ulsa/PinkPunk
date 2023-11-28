using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionsManagerScript : MonoBehaviour
{
    public GameObject[] misiones = new GameObject[3];
    public DeliveryMenuScript[] deliveryMenuScript;
    public int misionIndex = 0;
    public GameObject scren; 

    void Update()
    {
        for (int i = 0; i < misiones.Length; i++)
        {
            MisionDefaultScript misionDefaultScript = misiones[i].GetComponent<MisionDefaultScript>();

            if (misionDefaultScript != null)
            {
                string nameMision = misionDefaultScript.nameMision;
                string descriptionMision = misionDefaultScript.descriptionMision;
                string locationMision = misionDefaultScript.locationMision;
                bool misionStatus = misionDefaultScript.misionStatus;
                GameObject misionQuestMarkStart = misionDefaultScript.misionQuestMarkStart;
                GameObject misionQuestMarkEnd = misionDefaultScript.misionQuestMarkEnd;

                if (i < deliveryMenuScript.Length && misionStatus)
                {
                    deliveryMenuScript[i].DisplayMisionInfo(nameMision, descriptionMision, locationMision, misionStatus);
                }
                else if (i < deliveryMenuScript.Length && !misionStatus)
                {
                    deliveryMenuScript[i].DisplayMisionInfo("", "", "", false);
                }
                else
                {
                    Debug.Log("El objeto " + misiones[i].name + " no tiene el script DeliveryMenuScript adjunto.");
                }
            }
            else
            {
                Debug.Log("El objeto " + misiones[i].name + " no tiene el script MisionDefaultScript adjunto.");
            }
        }

        if (misionIndex == 3)
        {
            scren.SetActive(true);
            Time.timeScale = 1;
        }
        else
        {
            scren.SetActive(false);
        }

        
    }
}

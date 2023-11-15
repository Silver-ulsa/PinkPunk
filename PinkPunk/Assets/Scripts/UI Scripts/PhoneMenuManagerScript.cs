using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManagerScript : MonoBehaviour
{
    public GameObject mapSection;
    public GameObject profileSection;
    public GameObject deliverySection;
    public GameObject postsSection;

    private void SetActiveSelection(GameObject section)
    {
        mapSection.SetActive(false);
        profileSection.SetActive(false);
        deliverySection.SetActive(false);
        postsSection.SetActive(false);

        section.SetActive(true);
    }

    public void Mapa(){
        Debug.Log("Soy el mapa");
        SetActiveSelection(mapSection);
    }

        public void profile(){
        Debug.Log("Soy el perfil");
        SetActiveSelection(profileSection);
    }

        public void delivery(){
        Debug.Log("Soy las entregas");
        SetActiveSelection(deliverySection);
    }

        public void post(){
        Debug.Log("Soy las publicaciones");
        SetActiveSelection(postsSection);
    }
}

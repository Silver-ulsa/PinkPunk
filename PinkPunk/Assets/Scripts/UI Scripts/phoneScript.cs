using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class phoneScript : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject firstButton;
    public  PlayerInput actionMan; 
    [SerializeField] private GameObject phoneFrame;
    [SerializeField] bool  phoneMenu;

    void Start()
    {
        actionMan = GetComponent<PlayerInput>();
        phoneMenu = false;
    }

    void Update()
    {
        if (actionMan.actions["PhoneMenu"].triggered && phoneMenu == false)
        {
            phoneMenu = true;
            phoneFrame.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            Debug.Log("Selected");
            
            var eventSystem = EventSystem.current;

            eventSystem.SetSelectedGameObject(firstButton, new BaseEventData(eventSystem));
        }
        else if (actionMan.actions["PhoneMenu"].triggered && phoneMenu == true)
        {
            phoneMenu = false;
            phoneFrame.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}

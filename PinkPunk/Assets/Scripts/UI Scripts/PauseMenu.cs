using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MenuPausa : MonoBehaviour
{
    public  PlayerInput actionMan; 
    public EventSystem eventSystem;

    public GameObject firstButton;
    [SerializeField] private GameObject menuPausa;

    private bool EstaEnPausa;

    public void Start()
    {
        actionMan = GetComponent<PlayerInput>();
        EstaEnPausa = false;
    }
    public void Update()
    {
        if (actionMan.actions["PauseMenu"].triggered && EstaEnPausa == false )
        {
            Pausa();

        }

        else if (actionMan.actions["PauseMenu"].triggered && EstaEnPausa == true)
        {
            Reanudar();
        }
    }
    public void Pausa()
    {
        Debug.Log("Estoy en pausa");
        Time.timeScale = 0;
        menuPausa.SetActive(true);
        EstaEnPausa = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var eventSystem = EventSystem.current;

            eventSystem.SetSelectedGameObject(firstButton, new BaseEventData(eventSystem));

    }

    public void Reanudar()
    {
        Debug.Log("Me Reanude");
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        EstaEnPausa = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void MenuDeInicio()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
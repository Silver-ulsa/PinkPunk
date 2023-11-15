using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("GameScene");

        Debug.Log("El juego iniciará");
        Time.timeScale = 1;
    }

    public void quit()
    {
        Application.Quit();
    }
}

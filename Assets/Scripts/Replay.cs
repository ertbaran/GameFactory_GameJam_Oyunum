using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    GameManager gameManager;

    public void Restart()
    {
        gameManager = FindObjectOfType<GameManager>();
        SceneManager.LoadScene("PlayingScreen");
    }
    public void MainMenu()
    {
        Destroy(GameObject.Find("Music"));
        SceneManager.LoadScene("MainMenu");
    }
    public void Yonlendirme()
    {
        Application.OpenURL("http://www.tema.org.tr/");
    }
}

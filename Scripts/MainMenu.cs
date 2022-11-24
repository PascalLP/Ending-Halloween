using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Play Button
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        
    }

    // Quit Button
    public void QuitGame()
    {
        Debug.Log("Game Quitted");
        Application.Quit();
    }
}

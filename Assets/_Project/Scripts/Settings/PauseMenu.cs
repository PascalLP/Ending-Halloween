using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Hide pause canvas
        pauseMenuUI.SetActive(false);

        // Return time to normal
        Time.timeScale = 1f;

        isPaused = false;
    }

    void Pause()
    {
        // Display pause canvas
        pauseMenuUI.SetActive(true);

        // Freeze time
        Time.timeScale = 0f;

        isPaused = true;

    }

    public void LoadSettingsMenu()
    {
        Debug.Log("Loading settings");

        // Hide pause canvas
        pauseMenuUI.SetActive(false);
        // Show settings canvas
        settingsMenuUI.SetActive(true);

    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading main menu");
        // Return time to normal
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

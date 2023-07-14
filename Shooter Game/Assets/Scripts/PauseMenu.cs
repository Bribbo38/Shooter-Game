using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public static bool paused;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && paused)
        {
            Application.Quit();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(value: false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(value: true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}

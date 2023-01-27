using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameEvent unPauseEvent;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        unPauseEvent.Raise();
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        return;
#else
        Application.Quit();
#endif
    }
}

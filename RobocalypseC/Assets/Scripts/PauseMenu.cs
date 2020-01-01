
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject helpWindow;
    public GameObject exitConfirmationWindow;
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            if (!gamePaused)
                PauseGame();
            else
                ResumeGame();
        }
    }
    public void Resume()
    {
        ResumeGame();
    }
    public void Help()
    {
        ShowHelp();
    }
    public void BackFromHelp()
    {
        CloseHelp();
    }
    public void Exit()
    {
        ShowExitConfirmationBox();
    }
    public void ExitConfirmationYes()
    {
        Debug.Log("Exit");
    }
    public void ExitConfirmationNo()
    {
        CloseExitConfirmationBox();
    }
    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gameManager.Paused = true;
    }
    void ResumeGame() {
        pauseMenu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
        gameManager.Paused = false;
    }
    void ShowHelp()
    {
        helpWindow.SetActive(true);
    }
    void CloseHelp()
    {
        helpWindow.SetActive(false);
    }
    void ShowExitConfirmationBox()
    {
        exitConfirmationWindow.SetActive(true);
    }
    void CloseExitConfirmationBox()
    {
        exitConfirmationWindow.SetActive(false);
    }
}

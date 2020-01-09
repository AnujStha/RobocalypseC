using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject helpWindow;
    public GameObject exitConfirmationWindow;
    public GameObject gunInfo;
    public GameObject controls;
    public sceneLoader SceneLoader;
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
        Time.timeScale = 1;
        gameManager.Paused = false;
        save();
        SceneLoader.loadScene(0);
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
    public void GunInfo() {
        gunInfo.SetActive(true);
        controls.SetActive(false);
    }
    public void Controls(){
        gunInfo.SetActive(false);
        controls.SetActive(true);
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
    void save() {
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            GameObject player = gameManager.player;
            SaveSystem.SaveGame(player, player.GetComponentInChildren<GunPlaceHolderPlayer>(), player.GetComponent<HealthAndShield>(), SceneManager.GetActiveScene().buildIndex);
        }
        }
}

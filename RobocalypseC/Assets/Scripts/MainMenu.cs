using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject helpWindow;
    public GameObject creditsWindow;
    public GameObject exitConfirmationWindow;
    public GameObject levelChoice;
    public GameObject gunInfo;
    public GameObject controls;
    public sceneLoader sceneLoader;
    public Continue messagePass;
    public void StartGame(){
        ShowLevelChoice();
    }
    public void Level1() {
        StartLevel1();
    }
    public void Level2() {
        StartLevel2();
    }
    public void BackFromLevelChoice() {
        ExitLevelChoice();
    }
    public void Help() {
        ShowHelp();
    }
    public void BackFromHelp(){
        CloseHelp();
    }
    public void Credits() {
        ShowCredits();   
    }
    public void BackFromCredits(){
        ExitFromCredits();
    }
    public void Practice() {
        StartPracticeLevel();
    }
    public void Exit() {
        ShowExitConfirmationBox();
    }
    public void ExitConfirmationYes() {
        Application.Quit();
        Debug.Log("Exit");
    }
    public void ExitConfirmationNo() {
        CloseExitConfirmationBox();
    }
    public void Guninfo() {
        gunInfo.SetActive(true);
        controls.SetActive(true);
    }
    public void Controls() {
        controls.SetActive(false);
        gunInfo.SetActive(false);
    }
    public void Continue(){
        SaveData data = SaveSystem.loadPlayer();
        if (data != null) {
            int level = data.level;
            messagePass.continueLevel = true;
            sceneLoader.loadScene(level);
        }
    }
    void ShowLevelChoice() {
        levelChoice.SetActive(true);
    }
    void ExitLevelChoice() {
        levelChoice.SetActive(false);
    }
    void ShowHelp() {
        helpWindow.SetActive(true);
    }
    void CloseHelp() {
        helpWindow.SetActive(false);
    }
    void ShowCredits() {
        creditsWindow.SetActive(true);
    }
    void ExitFromCredits() {
        creditsWindow.SetActive(false);
    }
    void ShowExitConfirmationBox() {
        exitConfirmationWindow.SetActive(true);
    }
    void CloseExitConfirmationBox() {
        exitConfirmationWindow.SetActive(false);
    }
    void StartLevel1() {
        messagePass.continueLevel = false;
        sceneLoader.loadScene(1);
        Debug.Log("Start level 1");
    }
    void StartLevel2() {
        messagePass.continueLevel = false;
        sceneLoader.loadScene(2);
        Debug.Log("Start level 2");
    }
    void StartPracticeLevel() {
        messagePass.continueLevel = false;
        sceneLoader.loadScene(3);
        Debug.Log("startPracticeLevel");
    }
}

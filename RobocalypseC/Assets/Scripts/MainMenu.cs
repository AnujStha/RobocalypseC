using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject helpWindow;
    public GameObject creditsWindow;
    public GameObject exitConfirmationWindow;
    public GameObject levelChoice;
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
        Debug.Log("Start level 1");
    }
    void StartLevel2() {
        Debug.Log("Start level 2");
    }
    void StartPracticeLevel() {
        Debug.Log("startPracticeLevel");
    }
}

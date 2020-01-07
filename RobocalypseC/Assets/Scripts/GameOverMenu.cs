
using System.Collections;
using UnityEngine;
public class GameOverMenu : MonoBehaviour
{
    public GameObject menu,buttons;
    private PlayerController player;
    public sceneLoader sceneLoader;
    public float delayTime;
    private void Start()
    {
        player = gameManager.player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (!player.isAlive) {
            menu.SetActive(true);
            StartCoroutine(buttondelay());   
        }
    }
    public void MainMenu() {
        Debug.Log("MainMenu");
        sceneLoader.loadScene(0);
    }
    public void Restart() {
        Debug.Log("Restart");
    }
    public void Exit() {
        Debug.Log("Exit");
        Application.Quit();
    }
    IEnumerator buttondelay() {
        yield return new WaitForSeconds(delayTime);
        Activate();
    }
    void Activate() {
        buttons.SetActive(true);
    }
}

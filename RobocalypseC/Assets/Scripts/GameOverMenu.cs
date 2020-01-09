using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
public class GameOverMenu : MonoBehaviour
{
    public GameObject menu,buttons;
    private PlayerController player;
    private bool started = false;
    public sceneLoader sceneLoader;
    public float delayTime;
    private void Start()
    {
        player = gameManager.player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (!player.isAlive&&started==false) {
            started = true;
            menu.SetActive(true);
            StartCoroutine(buttondelay());   
        }
    }
    public void MainMenu() {
        sceneLoader.loadScene(0);
    }
    public void Restart() {
        sceneLoader.loadScene(SceneManager.GetActiveScene().buildIndex);
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

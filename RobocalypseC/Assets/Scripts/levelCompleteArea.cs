using System.Collections;
using UnityEngine;

public class levelCompleteArea : TouchDamage1
{
    private bool complete = false;
    public GameObject boss;
    public GameObject levelCompleteCanvas;
    public float offTime;
    public sceneLoader loader;
    public override void enter()
    {
        if (!complete) {
            if (boss == null) {
                CompleteArea();
            }
        }
    }
    void CompleteArea() {
        levelCompleteCanvas.SetActive(true);
        StartCoroutine(Loader());
    }
    IEnumerator Loader() {
        yield return new WaitForSeconds(offTime);
        loader.loadScene(0);
    }
}

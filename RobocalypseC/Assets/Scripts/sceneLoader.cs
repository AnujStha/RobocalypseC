using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class sceneLoader : MonoBehaviour {
	public GameObject loadingScreen;
	public Image LoadingSlider;

	// Use this for initialization
	void Start () {
		loadingScreen.SetActive (false);

	}
	
	// Update is called once per frame
	public void loadScene(int sceneIndex){
		StartCoroutine (LoadAsynchronously (sceneIndex));
	}

	IEnumerator LoadAsynchronously(int sceneIndex){
		
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);
		loadingScreen.SetActive ( true);
		while (!operation.isDone) {
			float progress =Mathf.Clamp01( operation.progress/0.9f);
			LoadingSlider.fillAmount = progress;
			yield return null;
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour {

	public void OpenMenu(){
		// 現在のScene名を取得する
		Scene loadScene = SceneManager.GetActiveScene();
		// Sceneの読み直し
		SceneManager.LoadScene(loadScene.name);
		/*
		if(Time.timeScale != 0){
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1.0f;
		}
		*/
	}
}

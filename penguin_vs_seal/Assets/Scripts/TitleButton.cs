using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reload(){
		//スタート
		Time.timeScale = 1.0f;
		// 現在のScene名を取得する
		//cene loadScene = SceneManager.GetActiveScene();
		// Sceneの読み直し
		SceneManager.LoadScene("SampleScene");
	}
}

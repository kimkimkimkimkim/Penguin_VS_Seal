﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reload(){
		// 現在のScene名を取得する
		Scene loadScene = SceneManager.GetActiveScene();
		// Sceneの読み直し
		SceneManager.LoadScene(loadScene.name);
	}
}

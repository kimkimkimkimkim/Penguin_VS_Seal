using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

	public GameObject GameOverView;

	public void GameOver(){
		GameOverView.SetActive(true); //ゲームオーバービュー表示
		if(Time.timeScale != 0){
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1.0f;
		}

	}
}

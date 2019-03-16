using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour {

	public GameObject GameOverView;
	public GameObject GameManager;
	public GameObject txt_score;
	public GameObject txt_best;

	public void GameOver(){
		int score = (int)GameManager.GetComponent<GameManager>().score;
		int best = PlayerPrefs.GetInt("best",0);
		if(score > best){
			PlayerPrefs.SetInt("best",score);
		}
		txt_score.GetComponent<TextMeshProUGUI>().text = score.ToString() + " M";
		txt_best.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("best",0).ToString() + " M";

		//GameOverView.SetActive(true); //ゲームオーバービュー表示
		naichilab.RankingLoader.Instance.SendScoreAndShowRanking (score);
		PlayerPrefs.SetInt("gamestart",0);
		/*
		if(Time.timeScale != 0){
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1.0f;
		}
		*/

	}
}

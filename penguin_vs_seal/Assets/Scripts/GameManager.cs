using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

	public GameObject txt_score;
	public GameObject player;
	public GameObject header;
	public GameObject title;

	float time = 0;
	public float score = 0;
	float max_timescale = 1.5f;
	private bool startflag = true;

	// Use this for initialization
	void Start () {
		//Time.timeScale = 0;
		PlayerPrefs.SetInt("gamestart",0);
		PlayerPrefs.SetInt("power",1);
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.anyKey || PlayerPrefs.GetInt("restart",0) == 1) && startflag){
			startflag = false;
			iTween.MoveTo(this.gameObject,iTween.Hash("x",0,"time",3,"oncomplete","GameStart",
			"oncompletetarget",gameObject,"easetype",iTween.EaseType.linear));
			title.SetActive(false);
		}
	}

	public void GameStart(){
		PlayerPrefs.SetInt("restart",0);
		PlayerPrefs.SetInt("gamestart",1);
		player.GetComponent<Animator>().SetBool("isRunning",true);
		header.SetActive(true);
	}

	void setScore(){

		txt_score.GetComponent<TextMeshProUGUI>().text = "Score: " + (Mathf.Round(score)).ToString();
	}

	void FixedUpdate(){
		if(PlayerPrefs.GetInt("gamestart",0) == 0)return;
		if(Time.timeScale == 0)return;
		time += Time.deltaTime;
		score += Time.deltaTime;
		setScore();
		//Debug.Log(time);
		if(time/15 >= 1 && Time.timeScale < max_timescale){
			time/=15;
			Time.timeScale += 0.1f;
			//Debug.Log(Time.timeScale);
			
		}
		if(time/30 >= 1){
			time/=30;
			Time.timeScale += 0.1f;
		}
	}
}

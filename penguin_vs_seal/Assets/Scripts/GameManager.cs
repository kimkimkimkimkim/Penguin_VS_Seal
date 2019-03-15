using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

	public GameObject txt_score;

	float time = 0;
	public float score = 0;
	float max_timescale = 1.5f;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("power",1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setScore(){

		txt_score.GetComponent<TextMeshProUGUI>().text = "Score: " + (Mathf.Round(score)).ToString();
	}

	void FixedUpdate(){
		if(Time.timeScale == 0)return;
		time += Time.deltaTime;
		score += Time.deltaTime;
		setScore();
		Debug.Log(time);
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

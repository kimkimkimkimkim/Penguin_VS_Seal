using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFieldManager : MonoBehaviour {

	public List<GameObject> patterns = new List<GameObject>();

	private int first_count = 3;
	private float width_pattern = 5.6f; //patternの幅

	// Use this for initialization
	void Start () {
		//int rand = new System.Random(1000).Next(patterns.Count);
		for(int i=0;i<first_count;i++){
			CreatePattern();
		}
	}

	public void CreatePattern(){
		GameObject t = this.transform.GetChild(this.transform.childCount - 1).gameObject;
		Vector3 pos_t = t.GetComponent<Transform>().position;

		int rand = new System.Random().Next(patterns.Count);
		GameObject pattern = (GameObject)Instantiate(patterns[rand]);
		pattern.transform.parent = this.transform;
		pattern.GetComponent<Transform>().localScale = new Vector3(1,1,1);
		pattern.GetComponent<Transform>().position = new Vector3(pos_t.x + width_pattern * 5 - 1,0,0);

		switch(rand){
		case 0:
			//３階建て
			SetPara(pattern,9);
			return;
		case 1:
			//２階建て
			SetPara(pattern,6);
			return;
		case 2:
			//穴あき
			SetPara(pattern,3);
			return;
		case 3:
			//平地
			SetPara(pattern,3);
			return;
		default:
			return;
		}
	}

	private void SetPara(GameObject obj,int num){
		//パラメーター設定
		int[,] array = new int[num,2];
		for(int i=0;i<num;i++){
			int cate = UnityEngine.Random.Range(1,3); //0:none , 1:Ally , 2:Enemy
			int pow = UnityEngine.Random.Range(1,11); //power
			array[i,0] = cate;
			array[i,1] = pow;
		}
		//オブジェクトに反映
		for(int i=0;i<num;i++){
			int cate = array[i,0];
			if(cate == 0){
				//none

			}else if(cate == 1){
				//Ally
				GameObject allys = obj.transform.Find("Ally").gameObject;
				GameObject ally = allys.transform.GetChild(i).gameObject;
				ally.SetActive(true);
				ally.GetComponent<SubPlayerManager>().power = array[i,1];
			}else{
				//Enemy
				GameObject enemies = obj.transform.Find("Enemy").gameObject;
				GameObject enemy = enemies.transform.GetChild(i).gameObject;
				enemy.SetActive(true);
				enemy.GetComponent<EnemyManager>().attack = array[i,1];
			}
		}
	}
	
	private void SetPara_A(GameObject obj){
		//３階建て
		int[,] array = new int[9,2];
		for(int i=0;i<9;i++){

		}
	}

	private void SetPara_B(GameObject obj){
		//２階建て
	}

	private void SetPara_C(GameObject obj){
		//穴あき
	}

	private void SetPara_D(GameObject obj){
		//平地
	}
	
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFieldManager : MonoBehaviour {

	public List<GameObject> patterns = new List<GameObject>();

	private int first_count = 2;
	private float width_pattern = 5.6f; //patternの幅
	private int create_count = 0;

	// Use this for initialization
	void Start () {
		//int rand = new System.Random(1000).Next(patterns.Count);
		for(int i=0;i<first_count;i++){
			CreatePattern();
		}
	}

	public void CreatePattern(){
		create_count++;
		GameObject t = this.transform.GetChild(this.transform.childCount - 1).gameObject;
		Vector3 pos_t = t.GetComponent<Transform>().position;

		//int rand = new System.Random().Next(patterns.Count);
		int rand;
		if(create_count % 3 == 0){
			rand = 0;
		}else{
			rand = UnityEngine.Random.Range(1,4);
		}
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
		int ratio;
		if(create_count <= 10){
			ratio = 50;
		}else{
			ratio = 80;
		}
		if(num == 9){
			//３階建て
			int num_strong = 0;
			int power = PlayerPrefs.GetInt("power");
			for(int i=0;i<num;i++){
				int cate_racio = UnityEngine.Random.Range(1,101);
				int cate;
				if(cate_racio <= ratio){
					cate = 2;
				}else{
					cate = 1;
				}
				//int cate = UnityEngine.Random.Range(1,3); //0:none , 1:Ally , 2:Enemy
				if(i%3 == 0){
					int isStrong = UnityEngine.Random.Range(0,2); //0:week,1:strong
					int pow;
					if(isStrong == 1){
						//敵が強い
						pow = power + UnityEngine.Random.Range(1,11);
						num_strong++;
					}else{
						if(power >= 50){
							pow = power - UnityEngine.Random.Range(10,20);
						}else{
							pow = UnityEngine.Random.Range(1,11);
						}
					}

					if(i == 6){
						if(num_strong == 0){
							//まだ誰も強くない
							if(UnityEngine.Random.Range(0,2) == 1){
								pow = power + UnityEngine.Random.Range(1,11);
							}
						}else if(num_strong == 3){
							//みんな強い
							pow = UnityEngine.Random.Range(1,11);
						}
					}
					array[i,0] = 2;
					array[i,1] = pow;
				}else{
					int pow = UnityEngine.Random.Range(1,11); //power
					array[i,0] = cate;
					array[i,1] = pow;
				}
			}
		}else{
			for(int i=0;i<num;i++){
				int cate_racio = UnityEngine.Random.Range(1,101);
				int cate;
				if(cate_racio <= ratio){
					cate = 2;
				}else{
					cate = 1;
				}
				//int cate = UnityEngine.Random.Range(1,3); //0:none , 1:Ally , 2:Enemy
				int pow = UnityEngine.Random.Range(1,11); //power
				array[i,0] = cate;
				array[i,1] = pow;
			}
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

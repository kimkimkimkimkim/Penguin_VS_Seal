using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutomaticGenerateStage : MonoBehaviour {

	public GameObject BlockPrefab;
	public GameObject SubPlayerPrefab;

	// Use this for initialization
	void Start () {
		InitialGenerate();
		GenerateSubPlayer();
		GenerateBlock();
	}

	//最初の生成
	void InitialGenerate(){
		for(int i=0;i<4;i++){
			GameObject block = (GameObject)Instantiate(BlockPrefab);
			block.transform.parent = this.transform.GetChild(0);
			Transform transform = block.GetComponent<Transform>();
			transform.localScale = new Vector3(1,1,1);

			float width = block.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().bounds.size.x;
			float space = 2;
			if(i == 0){
				transform.position = new Vector3(i * width * 6,0,-1);
			}else{
				transform.position = new Vector3(i * (width * 6 + space),0,-1);
			}
		}
	}

	//ブロックの生成
	void GenerateBlock(){
		StartCoroutine(DelayMethod(2.8f, () =>
		{
			GameObject block = (GameObject)Instantiate(BlockPrefab);
			block.transform.parent = this.transform.GetChild(0);
			Transform transform = block.GetComponent<Transform>();
			transform.localScale = new Vector3(1,1,1);
			float width = block.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().bounds.size.x;
			transform.position = new Vector3(20,0,-1);
			GenerateBlock();
		}));
	}

	//サブプレイヤーの作成
	void GenerateSubPlayer(){
		StartCoroutine(DelayMethod(2.8f, () =>
		{
			GameObject s_player = (GameObject)Instantiate(SubPlayerPrefab);
			s_player.transform.parent = this.transform;
			s_player.transform.position = new Vector3(14,-2.24f,-1); 
			GenerateSubPlayer();
		}));
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}

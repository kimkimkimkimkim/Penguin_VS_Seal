using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Const;

public class ChasePlayer : MonoBehaviour {

	private Animator animator;
	private GameObject player;
	private GameObject target;
	private bool canChase = false;
	private bool isEnter = false;
	private float delay_time = 0.4f; 
	private float offset_x = 1f;
	private int c_count; //親の子要素の個数

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		animator.SetBool("isRunning",false);
	}
	
	// Update is called once per frame
	void Update () {
		if(canChase){
			//c_count = this.transform.parent.childCount;
			Chase(player);
		}
	}

	void Chase(GameObject p){
		Vector3 p_pos = p.GetComponent<Transform>().position;
		//3.5秒後に実行する
		StartCoroutine(DelayMethod(delay_time * (c_count), () =>
		{
			this.transform.parent = player.transform;
			this.GetComponent<Transform>().position = new Vector3(p_pos.x - (offset_x * (c_count)),p_pos.y,p_pos.z);
		}));
	}

	void OnTriggerEnter2D(Collider2D col)
  {	
		if(col.tag == "Player" && !isEnter){
			player = col.gameObject;
			TextMesh textmesh = player.transform.Find("etc").Find("text_power").gameObject.GetComponent<TextMesh>();
			Debug.Log(textmesh.text);
			int power = int.Parse(textmesh.text)+1;
			textmesh.text = power.ToString();
			c_count = col.gameObject.transform.childCount;
			animator.SetBool("isRunning",true);
			canChase = true;
			isEnter = true;
		}
  }

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}

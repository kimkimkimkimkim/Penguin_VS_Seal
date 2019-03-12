using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Const;

public class ChasePlayer : MonoBehaviour {

	public bool isChasePlayer = false;

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
		if(isChasePlayer)StartChase();
	}

	public void StartChase(){
		player = this.transform.parent.gameObject; 
		for(int i=0;i<player.transform.childCount;i++){
			if(this.gameObject == player.transform.GetChild(i).gameObject){
				c_count = i;
			}
		}
		canChase = true;
		animator.SetBool("isRunning",true);
		this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
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
			this.GetComponent<Transform>().position = new Vector3(p_pos.x - (offset_x * (c_count)),p_pos.y,p_pos.z);
		}));
	}

	void OnTriggerEnter2D(Collider2D col)
	{	
		if(col.tag == "Player" && !isEnter){
			player = col.gameObject;
			int subp_power = this.gameObject.GetComponent<SubPlayerManager>().power;
			//player.GetComponent<PlayerPower>().power = player.GetComponent<PlayerPower>().power + 1;
			int power = player.GetComponent<PlayerPower>().power;
			player.GetComponent<PlayerPower>().setPower(power+subp_power);
			//StartChase(player);
			isEnter = true;
			Destroy(this.gameObject);
		}
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}

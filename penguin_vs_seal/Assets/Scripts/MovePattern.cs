using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class MovePattern : MonoBehaviour {

	private GameObject autofield;
	private float velocity = Const.CO.SPEED; //移動速度
	private float limit = -40; //最左端
	private float start = 20; //最右端

	// Use this for initialization
	void Start () {
		autofield = this.transform.parent.gameObject;
	}
	
	//移動
	void FixedUpdate(){
		Vector3 pos = this.GetComponent<Transform>().position;
		if(pos.x <= limit){
			Destroy(this.gameObject);
			autofield.GetComponent<AutoFieldManager>().CreatePattern();
			return;
		}
		this.GetComponent<Transform>().position = new Vector3(pos.x - velocity,pos.y,pos.z);
	}
}

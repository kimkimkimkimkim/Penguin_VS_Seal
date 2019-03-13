using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class MoveField : MonoBehaviour {

	private float velocity = Const.CO.SPEED; //移動速度
	private float limit = -20; //最左端
	private float start = 20; //最右端
	private float width; //横幅
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		width = this.GetComponent<SpriteRenderer>().bounds.size.x;
		sr = this.GetComponent<SpriteRenderer>();
	}
	
	//移動
	void FixedUpdate(){
		Vector3 pos = this.GetComponent<Transform>().position;
		if(pos.x <= limit){
			Destroy(this.gameObject);
			return;
		}else if(pos.x >= start){
			sr.color = new Color(1,1,1,0);
		}else{
			sr.color = new Color(1,1,1,1);
		}
		this.GetComponent<Transform>().position = new Vector3(pos.x - velocity,pos.y,pos.z);
	}
}

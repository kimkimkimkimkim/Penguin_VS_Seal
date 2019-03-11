using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour {

	public float velocity = 1.0f; //背景の移動速度

	private float offset = 0.1f; //背景画像のずらし
	private int pivot = 0; //一番左の背景画像
	private float width_bg; //背景画像の横幅
	private int num; //背景画像の個数

	// Use this for initialization
	void Start () {
		width_bg = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
		num = this.transform.childCount;
		SettingBackGround();
	}

	void SettingBackGround(){
		for(int i=1;i<num;i++){
			GameObject bg = this.transform.GetChild(i).gameObject;
			Vector3 target_pos = this.transform.GetChild(i-1).gameObject.GetComponent<Transform>().position;
			bg.GetComponent<Transform>().position = new Vector3(target_pos.x + width_bg - offset,target_pos.y,target_pos.z);
		}
	}
	
	//背景画像を移動させる
	void FixedUpdate(){
		float xPos = this.transform.GetChild(pivot).gameObject.GetComponent<Transform>().position.x;
		//左まで行ったら右に戻ってくる
		if(xPos <= -1*(width_bg*1.5f)) {
			GameObject bg = this.transform.GetChild(pivot).gameObject;
			int target = pivot == 0 ? num - 1 : pivot - 1;
			GameObject target_bg = this.transform.GetChild(target).gameObject;
			Vector3 target_pos = target_bg.GetComponent<Transform>().position;
			bg.GetComponent<Transform>().position = 
				new Vector3(target_pos.x + width_bg - offset,target_pos.y,target_pos.z);
			pivot = pivot + 1 == num ? 0 : pivot + 1; //pivot変更
		}
		//左に移動
		for(int i=0;i<num;i++){
			GameObject bg = this.transform.GetChild(i).gameObject;
			Transform transform = bg.GetComponent<Transform>();
			Vector3 pos = transform.position;
			transform.position = new Vector3(pos.x - velocity,pos.y,pos.z);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour {

	public int power = 1;

	private int limit_num = 7;

	public void setPower(int pow){
		CreateSubPlayer(pow);
		power = pow;
		this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = power.ToString();

	}

	void CreateSubPlayer(int now_pow){
		if(now_pow <= limit_num){
			for(int i=1;i<now_pow;i++){
				this.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
			}
			for(int i=now_pow;i<=limit_num;i++){
				this.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
			}
		}else{
			for(int i=1;i<=limit_num;i++){
				this.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
			}
		}
	}

}

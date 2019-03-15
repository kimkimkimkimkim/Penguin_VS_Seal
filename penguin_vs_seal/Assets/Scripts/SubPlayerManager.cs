using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayerManager : MonoBehaviour {

	public int power;

	void Start(){
		this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = power.ToString();
	}

	public void setPower(int pow){
		power = pow;
		GameObject txt_power = this.transform.GetChild(0).GetChild(0).gameObject;
		txt_power.GetComponent<TextMesh>().text = power.ToString();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public int attack ;
	public bool canBattle = true;

	private void Start(){
		this.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = attack.ToString();
	}

	public void setAttack(int atk){
		attack = atk;
		GameObject txt_atk = this.transform.GetChild(0).gameObject;
		txt_atk.GetComponent<TextMesh>().text = attack.ToString();
	}
}

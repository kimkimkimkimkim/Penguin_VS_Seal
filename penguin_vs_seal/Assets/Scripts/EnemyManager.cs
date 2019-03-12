using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public int attack ;
	public bool canBattle = true;

	public void setAttack(int atk){
		attack = atk;
		GameObject txt_atk = this.transform.GetChild(0).gameObject;
		txt_atk.GetComponent<TextMesh>().text = attack.ToString();
	}
}

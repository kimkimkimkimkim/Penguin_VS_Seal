using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

	public GameObject GameOverArea;

	public void Battle(int my_pow,int ene_pow,GameObject ene_obj){
		if(my_pow <= ene_pow){
			//負け
			GameOverArea.GetComponent<GameOverManager>().GameOver();
		}else{
			//勝ち
			this.gameObject.GetComponent<PlayerPower>().setPower(my_pow - ene_pow);
			Animator animator = ene_obj.GetComponent<Animator>();
			animator.SetBool("deathFlag",true);
			ene_obj.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour {

	float time = 0;
	float freaquency = 1f;

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		time += Time.deltaTime;
		if(time >= freaquency){
			time -= freaquency;
			GetComponent<TextMeshProUGUI>().enabled = !GetComponent<TextMeshProUGUI>().enabled;
		}
	}
}

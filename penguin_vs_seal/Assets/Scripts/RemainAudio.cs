using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RemainAudio : MonoBehaviour {

	#region Singleton

	private static RemainAudio instance;

	public static RemainAudio Instance {
		get {
			if (instance == null) {
				instance = (RemainAudio)FindObjectOfType (typeof(RemainAudio));

				if (instance == null) {
					Debug.LogError (typeof(RemainAudio) + "is nothing");
				}
			}

			return instance;
		}
	}

	#endregion Singleton

	public bool DontDestroyEnabled = true;

	public AudioClip jumpSE;
	public AudioClip jump2SE;
	public AudioClip allySE;
	public AudioClip enemySE;

    public void Awake ()
	{
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void ChangeBgm(int num){
		AudioSource[] audio;
		audio = this.GetComponents<AudioSource>();

		int len = audio.Length;
		for(int i=0;i<len;i++){
			audio[i].enabled = false;
		}
		audio[num].enabled = true;

	}

	public void Play(){
		Debug.Log("Play");
		AudioSource[] audio = GetComponents<AudioSource>();
		int n = audio.Length;
		int target = 0;
		for(int i=0;i<n;i++){
			if(audio[i].enabled)target = i;
		}
		audio[target].Play();	
	}

	public void Pause(){
		Debug.Log("Pause");
		AudioSource[] audio = GetComponents<AudioSource>();
		int n = audio.Length;
		int target = 0;
		for(int i=0;i<n;i++){
			if(audio[i].enabled)target = i;
		}
		audio[target].Pause();	
	}

	public void Stop(){
		Debug.Log("Stop");
		AudioSource[] audio = GetComponents<AudioSource>();
		int n = audio.Length;
		int target = 0;
		for(int i=0;i<n;i++){
			if(audio[i].enabled)target = i;
		}
		audio[target].Stop();	
	}

	public void PlaySE(string str){
		AudioSource[] audio = GetComponents<AudioSource>();
		int n = audio.Length;
		int target = 0;
		for(int i=0;i<n;i++){
			if(audio[i].enabled)target = i;
		}
		switch(str){
		case "jump":
			audio[target].PlayOneShot(jumpSE,2);
			break;
		case "jump2":
			audio[target].PlayOneShot(jump2SE,2);
			break;
		case "ally":
			audio[target].PlayOneShot(allySE,2);
			break;
		case "enemy":
			audio[target].PlayOneShot(enemySE,2);
			break;
		default:
			Debug.Log("そんなSEありません");
			break;
		}
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
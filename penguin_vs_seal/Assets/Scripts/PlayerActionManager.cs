using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerActionManager : MonoBehaviour {

	//オブジェクト参照
	public GameObject gameManager; //ゲームマネージャー
	public GameObject GameOverArea; //ゲームオーバー
	public GameObject ClearView; //クリア画面

	//グローバル変数
	public LayerMask blockLayer; //ブロックレイヤー

	//メンバ変数
	private Animator animator; //プレイヤーのアニメーター
	private Rigidbody2D rbody; //プレイヤー制御用のrigidbody2d
	private Vector3 nowPos; //プレイヤーの現在の位置
	private const float MOVE_SPEED = 3f; //移動速度固定値
	private float moveSpeed; //プレイヤーの移動速度
	private float jumpPower = 600; //ジャンプの力
	private float time_down = 0.4f; //下降中コライダーをoffにする時間
	private bool isDown = false; //ダウンしているかどうか
	private bool goJump = false; //ジャンプしたかどうか
	private bool canJump = false; //ブロックに設置しているかどうか
	private bool canDown = false;
	private bool goFlag = false; //ゲームオーバー
	private const int MAX_JUMP_COUNT = 2;	// ジャンプできる回数。 
	private int jumpCount = 0; 
	private bool isJump = false; 
	private BoxCollider2D b_col;
	private CircleCollider2D c_col;
	private Vector3 touchStartPos;
	private Vector3 touchEndPos;
	private string Direction = "";

	public enum MOVE_DIR //移動方向定義
	{
		STOP,
		LEFT,
		RIGHT,
	};

	private MOVE_DIR moveDirection = MOVE_DIR.STOP; //移動方向

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D> ();
		b_col = GetComponent<BoxCollider2D>();
		c_col = GetComponent<CircleCollider2D>();
		animator.SetBool("isRunning",false);
	}
	
	// Update is called once per frame
	void Update () {
		canJump = Physics2D.Linecast (transform.position - (transform.right * 0.1f),
			transform.position - (transform.up * 0.1f),blockLayer) ||
			Physics2D.Linecast (transform.position + (transform.right * 0.1f),
				transform.position - (transform.up * 0.1f),blockLayer);
		if(canJump)jumpCount = 0;

		//何も押してないとき
		moveDirection = MOVE_DIR.STOP;

		//右方向キー
		if (Input.GetKey (KeyCode.RightArrow)) {
			//moveDirection = MOVE_DIR.RIGHT;
		}

		//左方向キー
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//moveDirection = MOVE_DIR.LEFT;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0)){
			touchStartPos = new Vector3(Input.mousePosition.x,
										Input.mousePosition.y,
										Input.mousePosition.z);
		}

		if (Input.GetKeyUp(KeyCode.Mouse0)){
			touchEndPos = new Vector3(Input.mousePosition.x,
									Input.mousePosition.y,
									Input.mousePosition.z);
			GetDirection();
		}

		//上方向キー
		if (Input.GetKeyDown (KeyCode.UpArrow) || Direction == "touch") {
			Direction = "";
			/*
			if (canJump) {
				goJump = true;
			}
			*/
			//Debug.Log("before:" + jumpCount);
			if(jumpCount < MAX_JUMP_COUNT){
				goJump = true;
			}
		}

		//下方向キー
		if( Input.GetKeyDown(KeyCode.DownArrow) || Direction == "down"){
			Direction = "";
			if(canDown){
				canDown = false;
				isDown = true;
				SwitchColliderActive(false);
				StartCoroutine(DelayMethod(time_down,()=> {
					SwitchColliderActive(true);
					isDown = false;
				}));
			}
		}

		//y軸速度取得
		float y_velocity = rbody.velocity.y;
		//Debug.Log(y_velocity);
		if(y_velocity == 0){
			if(animator.GetInteger("JumpFlag") == 0)return;
			animator.SetInteger("JumpFlag",0);
		}else if(y_velocity > 0){
			//上昇
			if(animator.GetInteger("JumpFlag") == 1)return;
			SwitchColliderActive(false);
			animator.SetInteger("JumpFlag",1);
		}else if(y_velocity < -0.1f){
			//下降
			if(animator.GetInteger("JumpFlag") == -1)return;
			if(!isDown)SwitchColliderActive(true);
			animator.SetInteger("JumpFlag",-1);
		}else{
			animator.SetInteger("JumpFlag",0);
		}

		float y = this.GetComponent<Transform>().position.y;
		//Debug.Log(y);
		if(y >= 2){
			SwitchColliderActive(true);
		}
	}

	void GetDirection(){
		float directionX = touchEndPos.x - touchStartPos.x;
		float directionY = touchEndPos.y - touchStartPos.y;

		if (Mathf.Abs(directionY) < Mathf.Abs(directionX)){
			if (30 < directionX){
				//右向きにフリック
				Direction = "right";
			}else if (-30 > directionX){
				//左向きにフリック
				Direction = "left";
			}
			
		}else if (Mathf.Abs(directionX)<Mathf.Abs(directionY)){
			if (30 < directionY){
				//上向きにフリック
				Direction = "up";
			}else if (-30 > directionY){
				//下向きのフリック
				Direction = "down";
			}
		}else{
				//タッチを検出
				Direction = "touch";
    	}
  }
	

	void SwitchColliderActive(bool b){
		/* 
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		if(b){
			sr.color = new Color(1,1,1,1);
		}else{
			sr.color = new Color(0,0,0,1);
		}
		*/
		b_col.enabled = b;
		c_col.enabled = b;
	}

	void FixedUpdate(){
		//移動方向で処理を分岐
		switch (moveDirection) {
		case MOVE_DIR.STOP: //停止
			moveSpeed = 0;
			break;
		case MOVE_DIR.LEFT: //左に移動
			moveSpeed = MOVE_SPEED * -1;
			transform.localScale = new Vector2 (-5f, 5f);
			break;
		case MOVE_DIR.RIGHT: //右に移動
			moveSpeed = MOVE_SPEED;
			transform.localScale = new Vector2 (5f, 5f);
			break;
		}

		rbody.velocity = new Vector2 (moveSpeed, rbody.velocity.y);

		//ジャンプ処理
		if (goJump) {
			//Debug.Log("jumpCount:" + jumpCount);
			if(jumpCount==0){
				RemainAudio.Instance.PlaySE("jump");
			}else{
				RemainAudio.Instance.PlaySE("jump2");
			}
			jumpCount++;
			Vector3 v = rbody.velocity;
			rbody.velocity = new Vector3(v.x,0,v.z); //y方向の速度を初期化
			rbody.AddForce (Vector2.up * jumpPower);
			goJump = false;
		}

		float y = this.GetComponent<Transform>().position.y;
		//Debug.Log(y);
		if(y >= 2){
			SwitchColliderActive(true);
		}
	}

	//地面に着地している
	void OnCollisionStay2D(Collision2D col){
		float y = Mathf.Round(col.gameObject.GetComponent<Transform>().position.y*10);
		//jumpCount = 0;
		if(y != -32)canDown = true;
		//canJump = true;
	}

	void OnCollisionExit2D(Collision2D col){
		canDown = false;
		//canDown = false;
		//canJump = false;
	}
	

	//衝突処理
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "GameOver" && !goFlag){
			GameOverArea.GetComponent<GameOverManager>().GameOver();
			goFlag = true;
		}

		if(col.gameObject.tag == "Frog"){
			RemainAudio.Instance.PlaySE("enemy");
			bool canBattle = col.gameObject.GetComponent<EnemyManager>().canBattle;
			if(canBattle){
				col.gameObject.GetComponent<EnemyManager>().canBattle = false;
				int my_pow = this.GetComponent<PlayerPower>().power;
				GameObject ene_object = col.gameObject;
				int ene_pow = col.gameObject.GetComponent<EnemyManager>().attack;
				this.GetComponent<BattleManager>().Battle(my_pow,ene_pow,ene_object);
			}
		} 

		if(col.gameObject.tag == "House"){
			Time.timeScale = 0;
			/*
			if(Time.timeScale != 0){
				Time.timeScale = 0;
			}else{
				Time.timeScale = 1.0f;
			}
			*/
			ClearView.SetActive(true);
		}
		/*
		//プレイ中でなければ衝突判定は行わない
		if (gameManager.GetComponent<GameManager> ().gameMode != GameManager.GAME_MODE.PLAY) {
			return;
		}

		if(col.gameObject.tag == "Trap"){
			gameManager.GetComponent<GameManager> ().GameOver ();
			DestroyPlayer ();
		}

		if (col.gameObject.tag == "Goal") {
			gameManager.GetComponent<GameManager> ().GameClear ();
		}

		if (col.gameObject.tag == "Snow") {
			col.gameObject.GetComponent<SnowManager> ().GetSnow ();
		}
		*/
	}

	//プレイヤーオブジェクト削除処理
	void DestroyPlayer(){
		Destroy (this.gameObject);
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
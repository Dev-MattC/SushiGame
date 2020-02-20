﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player4Script : MonoBehaviour {


	bool onGround;
	public bool onWall;
	public bool jump = false;
	public bool wallJump = true;
	bool facingRight = true;
	bool canKillP1 = false;
	bool canKillP3 = false;
	bool canKillP2 = false;
	public bool riceThrow;
	public bool p4Throw;

	public bool stuck;
	public bool player4Attacking;

	public GameObject riceSpawn;
	public GameObject throwingRice;

	public Rigidbody2D rigidBody2D;

	public Transform groundCheck;
	public Transform wallCheck;

	int jumpPower = 325;
	float maxSpeed = 3.25f;
	float moveForce = 32.5f;

	public float horizontal = 0f;

	Animator anim;

	void Awake () {
		//rigidBody2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();  
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		horizontal = Input.GetAxisRaw("Horizontal4");

		if (stuck) {
			StartCoroutine (UnStick ());
		}

		//Checks to see if the player is on the ground or against the wall
		onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		onWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer ("Wall"));

		GameObject gameData = GameObject.Find ("GameData");
		if (gameData != null) {
			GameDataScript gameDataScript = gameData.GetComponent<GameDataScript> ();
			if (!facingRight) {
				gameDataScript.p4riceSpeed = -300f;
			}
			if (facingRight) {
				gameDataScript.p4riceSpeed = 300f;
			}
		}

		if (Input.GetButtonDown ("Jump4")) {
			if ((onGround) && (jump == false)) {
				wallJump = true;
				jump = true;
				anim.SetBool("Jump", true);
				StartCoroutine (Jump ());
			}
			if (((onWall) && (!jump) && (wallJump)))
			{
				anim.SetBool("Jump", false);
				anim.SetBool("Jump", true);
				StartCoroutine (Jump ());
				jump = true;
				wallJump = false;
			}
		}

		if (Input.GetButtonDown("Fire4"))
		{
			anim.SetBool ("Attack", true);
			player4Attacking = true;
			StartCoroutine (Attack ());
			GameObject player1 = GameObject.Find ("Player1");
			if (player1 != null) {
				StartCoroutine (DestroyPlayer1 (player1));
			}

			GameObject player3 = GameObject.Find ("Player3");
			if (player3 != null) {
				StartCoroutine (DestroyPlayer3 (player3));
			}

			GameObject player2 = GameObject.Find ("Player2");
			if (player2 != null) {
				StartCoroutine (DestroyPlayer2 (player2));
			}
		}

		if (Input.GetButtonDown ("Secondary4")) {
			if (riceThrow) {
				p4Throw = true;
				StartCoroutine (Rice ());
				riceThrow = false;
			}
		}

	}

	void FixedUpdate() {
		

		if (horizontal != 0f && anim.GetBool("Jump") == false) {
			anim.SetBool ("Moving", true);
		} else {
			anim.SetBool ("Moving", false);
		}

		//Handles the player movement
		if (jump == true && (!stuck)) {
			moveForce = 2f;
		} else {
			moveForce = 10f;
		}

		if ((horizontal * rigidBody2D.velocity.x) < maxSpeed && (!stuck))
			rigidBody2D.AddForce(Vector3.right * horizontal * moveForce);

		if (Mathf.Abs (rigidBody2D.velocity.x) > maxSpeed && (!stuck))
			rigidBody2D.velocity = new Vector3(Mathf.Sign (rigidBody2D.velocity.x) * maxSpeed, rigidBody2D.velocity.y, 0.0f);



		if (horizontal > 0 && !facingRight)
			Flip();
		else if (horizontal < 0 && facingRight)
			Flip();


		if (jump == true) {
			rigidBody2D.AddForce (new Vector3 (0.0f, jumpPower, 0.0f));
			jump = false;
		}


	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name.Equals("Player1"))
		{
			canKillP1 = true;
		}

		if(other.gameObject.name.Equals("Player3"))
		{
			canKillP3 = true;
		}

		if(other.gameObject.name.Equals("Player2"))
		{
			canKillP2 = true;
		}

		if (other.gameObject.name.Equals ("Rice(Clone)")) {
			riceThrow = true;
			Destroy (GameObject.Find ("Rice(Clone)"));
		}
		if (other.gameObject.name.Equals ("SoySauce(Clone)")) {
			maxSpeed = 8f;
			moveForce = 80f;
			StartCoroutine (NormalSpeed ());
			Destroy (GameObject.Find ("SoySauce(Clone)"));
		}
		if (other.gameObject.name.Equals ("Wasabi(Clone)")) {
			maxSpeed = 2f;
			moveForce = 20f;
			StartCoroutine (NormalSpeed ());
			Destroy (GameObject.Find ("Wasabi(Clone)"));
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.name.Equals("Player1"))
		{
			canKillP1 = false;
		}

		if(other.gameObject.name.Equals("Player3"))
		{
			canKillP3 = false;
		}

		if(other.gameObject.name.Equals("Player2"))
		{
			canKillP2 = false;
		}
	}

	IEnumerator Attack()
	{
		//player2Attacking = true;
		yield return new WaitForSeconds (0.3f);
		anim.SetBool ("Attack", false);
		player4Attacking = false;
	}

	IEnumerator DestroyPlayer1(GameObject a)
	{
		yield return new WaitForSeconds (0.2f);
		PlayerScript playerScript = a.GetComponent<PlayerScript> ();
		if ((canKillP1) && (playerScript.player1Attacking == false)) {
			Destroy (GameObject.Find ("Player1"));
			canKillP1 = false;
		}
		if ((canKillP1) && (playerScript.player1Attacking == true)) {
			if (facingRight) {
				a.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (200f, 0.0f));
				rigidBody2D.AddForce (new Vector2 (-400f, 0.0f));
			}
			if (!facingRight) {
				a.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-200f, 0.0f));
				rigidBody2D.AddForce (new Vector2 (400f, 0.0f));
			}

		}
	}

	IEnumerator DestroyPlayer3(GameObject a)
	{
		yield return new WaitForSeconds (0.2f);
		Player3Script player3Script = a.GetComponent<Player3Script> ();
		if ((canKillP3) && (player3Script.player3Attacking == false)) {
			Destroy (GameObject.Find ("Player3"));
			canKillP3 = false;
		}
		if ((canKillP3) && (player3Script.player3Attacking == true)) {
			if (facingRight) {
				a.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (200f, 0.0f));
				rigidBody2D.AddForce (new Vector2 (-400f, 0.0f));
			}
			if (!facingRight) {
				a.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-200f, 0.0f));
				rigidBody2D.AddForce (new Vector2 (400f, 0.0f));
			}

		}
	}

	IEnumerator DestroyPlayer2(GameObject a)
	{
		yield return new WaitForSeconds (0.2f);
		Player2Script player2Script = a.GetComponent<Player2Script> ();
		if ((canKillP2) && (player2Script.player2Attacking == false)) {
			Destroy (GameObject.Find ("Player2"));
			canKillP2 = false;
		}
		if ((canKillP2) && (player2Script.player2Attacking == true)) {
			if (facingRight) {
				a.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (200f, 0.0f));
				rigidBody2D.AddForce (new Vector2 (-400f, 0.0f));
			}
			if (!facingRight) {
				a.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-200f, 0.0f));
				rigidBody2D.AddForce (new Vector2 (400f, 0.0f));
			}

		}
	}

	IEnumerator Jump()
	{
		yield return new WaitForSeconds (0.3f);
		anim.SetBool ("Jump", false);
	}

	IEnumerator Rice() {
		//if (facingRight) {
		GameObject Rice = (GameObject)Instantiate (throwingRice);
		Rice.transform.position = riceSpawn.transform.position;
		Rice.name = "Sticky Rice";
		yield return new WaitForSeconds (0.5f);
		/*else {
			GameObject Projectile = (GameObject)Instantiate (projectileLeft);
			Projectile.transform.position = projectileSpawn.transform.position;
			Projectile.name = "bullet";
		}
		yield return new WaitForSeconds (0.5f);
		//anim.SetBool ("Throw", false);*/
	}

	IEnumerator UnStick()
	{
		yield return new WaitForSeconds (5f);
		stuck = false;
	}

	IEnumerator NormalSpeed()
	{
		yield return new WaitForSeconds (5f);
		maxSpeed = 4f;
		moveForce = 40f;
	}
}
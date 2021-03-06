﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	public float jump;
	float movementVelocity;
	public bool grounded = false;
	public bool facingRight = true;
	Rigidbody2D rb;
	public bool onRope = false;
	public bool jumpingPermission;
	public bool walkingPermission;

	public float dashForce;
	public float dashTimer;
	public bool isDashing;
	public float dashDuration;
	bool dashDirection;
	public float dashCDTime;
	float dashCD;

	Zombear zb;
	public bool playerIsHit;
	public int playerHP;
	//public BoxCollider2D bcR;
	//public BoxCollider2D bcL;

	//public object Bunny1 { get; private set; }


	//public Sprite playerHit;
	//public Sprite bunnyNormal;
	//SpriteRenderer psr;
	//public float pRecovery;
	//public float pRecoveryCDT;
	//public float pHitForce;

	public AudioSource mockery;
	public AudioSource death;
	public bool isDead;

	public AudioSource ouch;

	public Text HPtext;
	public Text GameOverText;
	bool isPause;


	void Awake() {
		rb = GameObject.Find("Bunny").GetComponent<Rigidbody2D>();
		zb = GameObject.Find("Zombear").GetComponent<Zombear>();
		//psr = GetComponent<SpriteRenderer>();
		//bunnyNormal = psr.sprite;
		//bcR = GameObject.Find("AreaOfEffectRight").GetComponent<BoxCollider2D>();
		//bcL = GameObject.Find("AreaOfEffectLeft").GetComponent<BoxCollider2D>();
		jumpingPermission = true;
		//aOER = GameObject.Find("AreaOfEffectRight").GetComponent<AreaOfEffect>();
		//aOEL = GameObject.Find("AreaOfEffectLeft").GetComponent<AreaOfEffect>();
	}



	void Update() {

		HPtext.text = "HP - " + playerHP;

		if (isDashing == true && dashCD < 0) {
			dashTimer += Time.deltaTime;



			if (dashTimer < dashDuration && dashDirection == true) {
				rb.MovePosition(transform.position + Vector3.right * dashForce * Time.deltaTime);
				GetComponent<SpriteRenderer>().flipX = false;
				walkingPermission = false;
				jumpingPermission = false;
			}
			else if (dashTimer < dashDuration && dashDirection == false) {
				rb.MovePosition(transform.position + Vector3.left * dashForce * Time.deltaTime);
				GetComponent<SpriteRenderer>().flipX = true;
				walkingPermission = false;
				jumpingPermission = false;
			}
			else {
				isDashing = false;
				dashTimer = 0;

				dashCD = dashCDTime;
			}
		}
		else {
			dashCD -= Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.C)) {

			dashDirection = facingRight;

			if (onRope == false && dashCD < 0) {
				isDashing = true;
			}
		}
		if (playerIsHit == true) {
			playerHP -= zb.enemyDMG;
			ouch.Play();
		}

		if (Input.GetKeyDown(KeyCode.Escape) && playerHP <= 0) {
			SceneManager.LoadScene(0);
		}
		if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
			if (isPause) {
				Time.timeScale = 1;
				isPause = false;
			}
			else {
				Time.timeScale = 0;
				isPause = true;
			}
		}
		if (playerHP <= 0) {
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			if (isDead == false) {
				zb.isChasing = false;
				death.Play();
				mockery.Play();
				isDead = true;
				GameOverText.text = ("GAME OVER \n press Esc to reset");
			}
		}

	}

	void FixedUpdate() {
		if (Input.GetKey(KeyCode.Space) && jumpingPermission == true) {
			onRope = false;
			rb.isKinematic = false;
			if (grounded) {
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);

				//  run.Play("Jump animatio");

			}
		}
		movementVelocity = 0;
		if (onRope == false) {

			if (Input.GetKey(KeyCode.LeftArrow) && walkingPermission == true) {
				movementVelocity = -speed;
				facingRight = false;
				GetComponent<SpriteRenderer>().flipX = true;

			}
			if (Input.GetKey(KeyCode.RightArrow) && walkingPermission == true) {
				movementVelocity = speed;
				facingRight = true;
				GetComponent<SpriteRenderer>().flipX = false;
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(movementVelocity, GetComponent<Rigidbody2D>().velocity.y);

				if (Input.GetKeyDown(KeyCode.Space) && jumpingPermission == true) {

					onRope = false;
					rb.isKinematic = false;
					//aOEL.enabled = false;
					//aOER.enabled = false;
					if (grounded) {
						GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
					}
				}
		
		   
				
			}

			if (onRope == true) {
                movementVelocity = 0;
				if (Input.GetKey(KeyCode.UpArrow)) {
					movementVelocity = speed;

				}
				if (Input.GetKey(KeyCode.DownArrow)) {
					movementVelocity = -speed;

					//aOEL.enabled = false;
					//aOER.enabled = false;

				}
					GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, movementVelocity);
                }
		}
				void OnTriggerStay2D(Collider2D other) {
					if (other.tag == ("Rope") && Input.GetKey(KeyCode.UpArrow)) {
			            rb.isKinematic = true;
						onRope = true;

					}
					if (other.tag != ("Rope") && other.tag != ("Enemy") && other.tag != ("Enemy2")) {
					grounded = true;
		            }

				}

				void OnTriggerExit2D(Collider2D other) {
					if (other.tag == ("Rope")) {
			            rb.isKinematic = false;
						onRope = false;

					}
			            grounded = false;
				}
				void OnTriggerEnter2D(Collider2D other) {

					if (other.tag == ("Rope") && Input.GetKey(KeyCode.UpArrow)) {
			            rb.isKinematic = true;
						onRope = true;

					}
				}
			}
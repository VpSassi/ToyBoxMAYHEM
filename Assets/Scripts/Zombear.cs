using UnityEngine;
using System.Collections;

public class Zombear : MonoBehaviour {

	public int HP = 3;
	PlayerMovement pm;
	AreaOfEffect areaOfEffect;

	public bool isHit;
	public bool getsHit;
	public float recovery;
	public float recoveryCDT;
	public float hitForce;

	public int enemyDMG;
	public float attack;
	public float attackCDT;
	public bool attacking;
	public float pPushForce;
	public float deathTime;

	public Sprite enemyHit;
	Sprite enemyNormal;
	SpriteRenderer sr;
	AreaOfEffect aOER;
	AreaOfEffect aOEL;
	BoxCollider2D bcR;
	BoxCollider2D bcL;

	Rigidbody2D rb;

	public float enemySpeed;
	public Transform target;
	public bool isChasing;
	Vector3 facingDirection;

	public Animator zombear;

	// Enemy AI variables

	public LayerMask idleMask;
	public float movSpeed;
	public float sightRange;
	Vector2 idleDir;
	Transform player;



	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		pm = GameObject.Find("Bunny").GetComponent<PlayerMovement>();
		enemyNormal = sr.sprite;
		target = pm.transform;
	}

	void Start() {
		idleDir = Vector2.right;
		player = GameObject.Find("Bunny").transform;
	}

	void Update() {

		// Idle
		if (!isChasing && pm.playerHP > 0) {

			RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(idleDir.x, 0, 0), Vector2.down, 1f, idleMask);
			RaycastHit2D front = Physics2D.Raycast(transform.position, idleDir, 1f, idleMask);
			if (rayHit.collider == null || front.collider != null) {
				idleDir = -idleDir;
			}

			//if (bot.collider != null) {
			rb.MovePosition((Vector2)transform.position + idleDir * movSpeed);
			//}

			// Detection
			if (Vector2.Distance(transform.position, player.position) < sightRange) {
				isChasing = true;
			}
		}

		// Chasing
		if (isChasing == true && pm.playerHP > 0) {
			float step = enemySpeed * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, target.position, step);
		}

		facingDirection = (target.position - transform.position).normalized;

		if (!isChasing && idleDir.x > 0) {
			sr.flipX = false;
		} else {
			sr.flipX = true;
		}

		if (facingDirection.x > 0 & isChasing) {
			sr.flipX = false;
		} else {
			sr.flipX = true;
		}

		// Attacking
		if (attacking == true) {

			attack += Time.deltaTime;
			if (attack > attackCDT) {
				pm.playerIsHit = true;
				attack -= attackCDT;
			} else {
				pm.playerIsHit = false;
			}
		}

		// Took a hit
		if (isHit == true) {
			recovery += Time.deltaTime;
			if (recovery > recoveryCDT) {
				recovery -= recoveryCDT;
				isHit = false;
				sr.sprite = enemyNormal;
				GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, hitForce);
			}
		}

		// Death
		if (HP <= 0) {
			isChasing = false;
			attacking = false;
			deathTime += Time.deltaTime;

			if (deathTime > 1.5) {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.tag == "Player") {
			attacking = true;
		}
	}

	void OnTriggerExit2D(Collider2D c) {
		if (c.tag == "Player") {
			attacking = false;
		}
	}
}
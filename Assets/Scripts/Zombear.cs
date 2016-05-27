using UnityEngine;
using System.Collections;

public class Zombear : MonoBehaviour {

    public int HP = 3;
    PewPew pewPew;
    PlayerMovement pm;

    public bool isHit;
    public float recovery;
    public float recoveryCDT;
    public float hitForce;

    public int enemyDMG;
    public float attack;
    public float attackCDT;
    public bool attacking;
    public float pPushForce;

    public Sprite enemyHit;
    Sprite enemyNormal;
    SpriteRenderer sr;

    Rigidbody2D rbEnemy;

    public float enemySpeed;
    public Transform target;
    public bool isChasing;

	void Awake () {
        pewPew = GetComponent<PewPew>();
        sr = GetComponent<SpriteRenderer>();
        pm = GameObject.Find("Bunny").GetComponent<PlayerMovement>();
        enemyNormal = sr.sprite;
        isChasing = true;
        target = pm.transform;

	
	}

	void Update () {

        if (isChasing == true && pm.playerHP > 0)
        {
            float step = enemySpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
         
        }




        if (isHit == true)
        {
            sr.sprite = enemyHit;
            //rbEnemy.MovePosition(transform.position + Vector3.up * hitForce * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(
            GetComponent<Rigidbody2D>().velocity.x, hitForce);

            recovery += Time.deltaTime;
            if (recovery > recoveryCDT) {
                recovery -= recoveryCDT;
                isHit = false;
                sr.sprite = enemyNormal;
            }

        }

        if (HP <= 0) {
            Destroy(gameObject);
        }

        if (attacking == true) {

        attack += Time.deltaTime;
        if (attack > attackCDT)
        {
            pm.playerIsHit = true;
            attack -= attackCDT;
            }
        else pm.playerIsHit = false;
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

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

    public Sprite enemyHit;
    Sprite enemyNormal;
    SpriteRenderer sr;

    Rigidbody2D rbEnemy;

	void Awake () {
        pewPew = GetComponent<PewPew>();
        sr = GetComponent<SpriteRenderer>();
        pm = GameObject.Find("Bunny").GetComponent<PlayerMovement>();
        enemyNormal = sr.sprite;

	
	}

	void Update () {

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
            print("attackHit");
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

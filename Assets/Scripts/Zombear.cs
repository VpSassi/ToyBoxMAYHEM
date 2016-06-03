using UnityEngine;
using System.Collections;

public class Zombear : MonoBehaviour
{

    public int HP = 3;
    PewPew pewPew;
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

    public Sprite enemyHit;
    Sprite enemyNormal;
    SpriteRenderer sr;
    AreaOfEffect aOER;
    AreaOfEffect aOEL;
    BoxCollider2D bcR;
    BoxCollider2D bcL;

    Rigidbody2D rbEnemy;

    public float enemySpeed;
    public Transform target;
    public bool isChasing;
    Vector3 facingDirection;

    public Animator zombear;

    void Awake()
    {
        pewPew = GetComponent<PewPew>();
        aOER = GetComponent<AreaOfEffect>();
        aOEL = GetComponent<AreaOfEffect>();
        sr = GetComponent<SpriteRenderer>();
        pm = GameObject.Find("Bunny").GetComponent<PlayerMovement>();
        enemyNormal = sr.sprite;
        isChasing = true;
        target = pm.transform;


    }

    void Update()
    {

        if (isChasing == true && pm.playerHP > 0)
        {
            float step = enemySpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target.position, step);

            //zombear.Play("");

        }

        facingDirection = (target.position - transform.position).normalized;
        if ( facingDirection.x > 0) {
            sr.flipX = true;
        }
        else {
            sr.flipX = false;
        }




        if (isHit == true)
        {
            sr.sprite = enemyHit;
            //rbEnemy.MovePosition(transform.position + Vector3.up * hitForce * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(
            GetComponent<Rigidbody2D>().velocity.x, hitForce);

            recovery += Time.deltaTime;
            if (recovery > recoveryCDT)
            {
                recovery -= recoveryCDT;
                isHit = false;
                sr.sprite = enemyNormal;
            }

        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        if (attacking == true)
        {

            attack += Time.deltaTime;
            if (attack > attackCDT)
            {
                pm.playerIsHit = true;
                attack -= attackCDT;
            }
            else pm.playerIsHit = false;
        }



    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            attacking = true;
        }

        if (c.tag == "AreaOfEffectRight")

        {
            sr.sprite = enemyHit;
            //rbEnemy.MovePosition(transform.position + Vector3.up * hitForce * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(
            GetComponent<Rigidbody2D>().velocity.x, hitForce);


        }

        if (c.tag == "AreaOfEffectLeft")

        {
            sr.sprite = enemyHit;
            //rbEnemy.MovePosition(transform.position + Vector3.up * hitForce * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = new Vector2(
            GetComponent<Rigidbody2D>().velocity.x, hitForce);

        }

        if (HP <= 0)
        {
           Destroy(gameObject);
        }

    }
    void OnTriggerExit2D(Collider2D c)
    {

        if (c.tag == "Player")
        {
            attacking = false;
        }

        if (c.tag == "AreaOfEffectRight")
        {
            recovery += Time.deltaTime;
            if (recovery > recoveryCDT)
            {
                recovery -= recoveryCDT;
                isHit = false;
                sr.sprite = enemyNormal;
            }

            if (c.tag == "AreaOfEffectLeft")
            {
                recovery += Time.deltaTime;
                if (recovery > recoveryCDT)
                {
                    recovery -= recoveryCDT;
                    isHit = false;
                    sr.sprite = enemyNormal;
                }
            }
        }
    }
}
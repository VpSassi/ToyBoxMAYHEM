using UnityEngine;
using System.Collections;

public class ZombearAnimations : MonoBehaviour {

	Zombear teddy;

	Animator zAnim;

	void Start () {
		zAnim = GetComponent<Animator>();
		teddy = GetComponent<Zombear>();
	}
	

	void Update () {
	
		if (teddy.HP <= 0) {
			zAnim.Play("ZB death");
		} else if (teddy.isHit == true) {
			zAnim.Play("ZB damaged");
		} else if (teddy.attacking == true) {
			zAnim.Play("Zombear attack");
		} else if (teddy.isChasing == true) {
			zAnim.Play("ZB walk");
		}


	}
}

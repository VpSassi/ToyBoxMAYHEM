using UnityEngine;
using System.Collections;

public class BallerinaChopperAnimation : MonoBehaviour {

	Zombear arr;

	Animator BCAnim;

	void Start () {
		BCAnim = GetComponent<Animator>();
		arr = GetComponent<Zombear>();
	}
	
	void Update () {

		if (arr.HP <= 0) {
			BCAnim.Play("RMdeath");
		} else if (arr.isHit == true) {
			BCAnim.Play("RM hurt");
		} else if (arr.isChasing == true) {
			BCAnim.Play("RM flying");
		}
	
	}
}

using UnityEngine;
using System.Collections;

public class playerAnimation : MonoBehaviour {

	PlayerMovement playMov;
	PewPew gun;
	AreaOfEffect penetratorNator;

	bool animTimer;
	float animTime;

	Animator pAnim;

	void Start () {
		pAnim = GetComponent<Animator>();
		playMov = GetComponent<PlayerMovement>();
		penetratorNator = GetComponent<AreaOfEffect>();
		gun = GetComponent<PewPew>();
	}

	void Update () {

		if (Input.GetKey(KeyCode.Z)) {
			pAnim.Play("Shoot animation");
		}else if (Input.GetKeyDown(KeyCode.C) || playMov.isDashing == true && playMov.onRope == false) {
			pAnim.Play("Dash animation");
		}else if (Input.GetKeyDown(KeyCode.X) && penetratorNator.penetrator == true 
			|| animTimer == true && penetratorNator.penetrator == true) {
			animTimer = true;
			pAnim.Play("PenetratorAnimation");
		}else if (Input.GetKeyDown(KeyCode.Space) || playMov.grounded == false) {
			pAnim.Play("Jump animatio");
		}else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
		  && playMov.onRope == false) {
			pAnim.Play("RabbitRunAnimation");
		}else {
			pAnim.Play("Idle");
		}
		
		//print(pAnim.GetCurrentAnimatorStateInfo(0).length);

		if (animTimer == true) {
			animTime += Time.deltaTime;
			if (animTime > 0.33f) {
				animTime = 0;
				animTimer = false;
			}

		}
	}

}

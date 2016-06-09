using UnityEngine;
using System.Collections;

public class Muzik : MonoBehaviour {

	PlayerMovement pMov;

	public AudioSource music;


	void Start () {

		pMov = GameObject.Find("Bunny").GetComponent<PlayerMovement>();
		music.Play();
	}
	

	void Update () {
	
		if (pMov.isDead == true) {
			music.Stop();
		}
	}
}

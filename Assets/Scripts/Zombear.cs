using UnityEngine;
using System.Collections;

public class Zombear : MonoBehaviour {

    public int HP = 3;
    PewPew pewPew;

	void Awake () {
        pewPew = GetComponent<PewPew>();
	
	}

	void Update () {



        if (HP <= 0) {
            print("Enemy Dead");
        }
	
	}
}
